using System.Security.Cryptography.X509Certificates;
using ComponentPro.Saml2;
using Microsoft.AspNetCore.Http;

namespace ComProvis.AV.UI.Providers
{
    public interface IComponentProSamlProvider
    {
        X509Certificate2 GetCertificate();
        void ProcessErrorResponse(Response samlResponse, HttpContext httpContext);
        void ProcessSuccessResponse(Response samlResponse, string relayState, HttpContext httpContext);
    }
}