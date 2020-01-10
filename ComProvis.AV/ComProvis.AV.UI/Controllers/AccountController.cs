using System;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;
using System.Web;
using ComponentPro.Saml;
using ComponentPro.Saml2;
using ComProvis.AV.Services;
using ComProvis.AV.UI.Models;
using ComProvis.AV.UI.Providers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace ComProvis.AV.UI.Controllers
{
    [Route("api/[controller]")]
    public class AccountController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly IComponentProSamlProvider _certificateProvider;
        private readonly IDataService _dataService;

        public AccountController(IConfiguration configuration, IComponentProSamlProvider certificateProvider, IDataService dataService)
        {
            _certificateProvider = certificateProvider;
            _configuration = configuration;
            _dataService = dataService;
        }

        [AllowAnonymous]
        [Route("/login")]
        [HttpGet]
        public async Task<IActionResult> Login()
        {
            var BaseUrl = _configuration["UiHomeAfterLogin"];
            var cookieContent = HttpContext.Request.Cookies["User"].ToString();
            if (string.IsNullOrEmpty(cookieContent))
            {
                return BadRequest("UserNotLogged");
            }

            var value = JsonConvert.DeserializeObject<UserData>(cookieContent);

            var user = await _dataService.UserService.ExternalGetAsync(value.ssoID);

            if (user?.Id == null)
            {
                return BadRequest("UserNotRegistered");
            }

            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var jwtTokenProvider = new JwtTokenProvider();
            var token = jwtTokenProvider.GenerateJwtToken(user.ExternalId.ToString(), "ADMIN");

            var roles = new string[] { "CLIENT" };

            var userModel = new AuthenticatedUserInfoModel
            {
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserName = value.ssoID,
                Token = jwtSecurityTokenHandler.WriteToken(token),
                Roles = roles,
                Expires = token.ValidTo,
                Issued = token.ValidFrom
            };

            return Redirect(BaseUrl+"?ssoId="+userModel.UserName + "&token=" + userModel.Token+"&culture=hr-HR");
        }

        [HttpPost]
        [Route("assert")]
        [AllowAnonymous]
        public void Assert(string binding)
        {
            try
            {

                RecieveResponse(out Response samlResponse, out string relayState, binding);

                if (samlResponse == null) return;

                if (samlResponse.IsSuccess())
                {
                    _certificateProvider.ProcessSuccessResponse(samlResponse, relayState, HttpContext);
                }
                else
                {
                    _certificateProvider.ProcessErrorResponse(samlResponse, HttpContext);
                }
            }
            catch (Exception)
            {

            }
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("/trylogin")]
        public ActionResult TryLogin()
        {
            var authnRequest = BuildAuthenticationRequest();

            var spResourceUrl = HttpContext.Request.Host.Value + "/login";
            var relayState = Guid.NewGuid().ToString();

            authnRequest.NameIdPolicy.SpNameQualifier = spResourceUrl;

            authnRequest.RequestedAuthnContext = new RequestedAuthnContext
            {
                Comparison = SamlAuthenticationContextComparison.Exact
            };
            authnRequest.RequestedAuthnContext.AuthenticationContexts.Add(new AuthnContextClassRef("urn:oasis:names:tc:SAML:2.0:ac:classes:PasswordProtectedTransport"));

            var idpUrl = $"{_configuration["MarketplaceSamlRoute"]}?{"binding"}={HttpUtility.UrlEncode("urn:oasis:names:tc:SAML:2.0:bindings:HTTP-Redirect")}";

            SamlSettings.CacheProvider.Insert(relayState, spResourceUrl, new TimeSpan(0, 0, 1));

            //X509Certificate2 x509Certificate = _certificateProvider.GetCertificate();
            //authnRequest.Redirect(HttpContext.Response, idpUrl, relayState, x509Certificate.PrivateKey);

            authnRequest.Redirect(HttpContext.Response, idpUrl, relayState, null);

            return new EmptyResult();

        }

        [HttpGet]
        [Route("/logout")]
        public ActionResult Logout()
        {
            HttpContext.Session.Remove("User");
            return Redirect(_configuration["MarketplaceUrl"]);
        }

        private AuthnRequest BuildAuthenticationRequest()
        {
            var issuerUrl = HttpContext.Request.Host.Value;
            var assertionConsumerServiceUrl = string.Format("{0}?{1}={2}", _configuration["AssertionConsumerServiceUrl"], "binding", HttpUtility.UrlEncode("urn:oasis:names:tc:SAML:2.0:bindings:HTTP-POST"));
            var url = $"{_configuration["MarketplaceSamlRoute"]}?{"binding"}={HttpUtility.UrlEncode("urn:oasis:names:tc:SAML:2.0:bindings:HTTP-Redirect")}";

            var authnRequest = new AuthnRequest
            {
                ForceAuthn = false,
                Issuer = new Issuer(),
                Destination = _configuration["MarketplaceSamlRoute"],
                NameIdPolicy = new NameIdPolicy(null, null, true),
                ProtocolBinding = SamlBindingUri.HttpRedirect,
                AssertionConsumerServiceUrl = assertionConsumerServiceUrl
            };

            return authnRequest;
        }

        private void RecieveResponse(out ComponentPro.Saml2.Response samlResponse, out string relayState, string binding)
        {
            var bindingType = binding;

            if (bindingType == null)
            {
                if (HttpContext.Session.GetString("User") != null)
                {
                    HttpContext.Session.Set("User", null);
                }
                else
                {
                    HttpContext.Response.Redirect("trylogin");
                }
            }

            switch (bindingType)
            {
                case SamlBindingUri.HttpPost:
                    samlResponse = ComponentPro.Saml2.Response.Create(HttpContext.Request);
                    relayState = samlResponse.RelayState;
                    break;

                default:
                    samlResponse = null;
                    relayState = null;
                    return;
            }

            var x509Certificate = _certificateProvider.GetCertificate();
            if (!samlResponse.Validate(x509Certificate))
            {
                throw new ApplicationException("The SAML response signature failed to verify.");
            }
        }
    }
}
