using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;
using System;

namespace ComProvis.AV.UI.Extensions
{
    public static class CookieExtensions
    {
        public static void SetCookie<TEntity>(this HttpResponse httpResponse, string key, TEntity value)
        {
            httpResponse.Cookies.Append(key, JsonConvert.SerializeObject(value));
        }

        public static void AppendWithoutEscapes(this IHeaderDictionary headers, string key, string value, CookieOptions options)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            var setCookieHeaderValue = new SetCookieHeaderValue("")
            {
                Domain = options.Domain,
                Path = options.Path,
                Expires = options.Expires,
                Secure = options.Secure,
                SameSite = (Microsoft.Net.Http.Headers.SameSiteMode)options.SameSite,
                HttpOnly = options.HttpOnly
            };

            var cookie = key + "=" + value;
            var cookieValue = cookie + setCookieHeaderValue.ToString().Substring(1);

            headers[HeaderNames.SetCookie] = StringValues.Concat(headers[HeaderNames.SetCookie], cookieValue);
        }

        public static TEntity GetCookie<TEntity>(this HttpRequest httpRequest, string key)
        {
            var value = httpRequest.Cookies[key];
            return value == null ? default(TEntity) :
                                  JsonConvert.DeserializeObject<TEntity>(value);
        }
        public static void RemoveCookie(this HttpResponse httpResponse, string key)
        {
            httpResponse.Cookies.Delete(key);
        }

    }
}
