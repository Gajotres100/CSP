using ComponentPro.Saml2;
using ComProvis.AV.UI.Models;
using Microsoft.AspNetCore.Http;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Extensions.Configuration;
using ComProvis.AV.UI.Extensions;
using Microsoft.AspNetCore.Hosting;

namespace ComProvis.AV.UI.Providers
{
    public class ComponentProSamlProvider : IComponentProSamlProvider
    {
        private readonly IConfiguration _configuration;
        private readonly IHostingEnvironment _hostingEnvironment;

        public ComponentProSamlProvider(IConfiguration configuration, IHostingEnvironment hostingEnvironment)
        {
            _configuration = configuration;
            _hostingEnvironment = hostingEnvironment;
        }

        public X509Certificate2 GetCertificate()
        {
            var cert = new X509Certificate2(_hostingEnvironment.ContentRootPath + _configuration["CertFileLocation"]);
            return cert;
        }

        public void ProcessSuccessResponse(Response samlResponse, string relayState, HttpContext httpContext)
        {
            var assertion = (Assertion)samlResponse.Assertions[0];
            var userName = assertion.Subject.NameId.NameIdentifier;

            var userData = new UserData();

            foreach (Attribute decryptedAttribute in assertion.AttributeStatements[0].Attributes)
            {
                if (decryptedAttribute.Name.ToString() == "Email")
                    userData.email = decryptedAttribute.Values[0].ToString();
                else if (decryptedAttribute.Name.ToString() == "ssoID")
                    userData.ssoID = decryptedAttribute.Values[0].ToString();
            }

            httpContext.Response.SetCookie("User", userData);

            httpContext.Response.Redirect(_configuration["UiHomePage"], false);
        }

        public void ProcessErrorResponse(Response samlResponse, HttpContext httpContext)
        {
            string errorMessage = null;

            if ((samlResponse.Status.StatusMessage != null))
            {
                errorMessage = samlResponse.Status.StatusMessage.Message;
            }

            var redirectUrl = string.Format(_configuration["UiHomePage"]);

            httpContext.Response.Redirect(redirectUrl, false);
        }
    }
}
