using System;
using System.Net;

namespace ComProvis.AV.Integration.Exceptions
{
    [Serializable]
    public class TrendMicroApiException : Exception
    {
        public HttpStatusCode StatusCode { get; set; }
        public string StatusDescription { get; set; }

        public TrendMicroApiException(HttpStatusCode statusCode, string statusDescription,
                                string message, Exception innerException) : base(message, innerException)
        {
            StatusCode = StatusCode;
            StatusDescription = StatusDescription;
        }
    }
}
