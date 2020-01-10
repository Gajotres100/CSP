using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Reflection;

namespace ComProvis.AV.UI.MVC.Helper
{
    public static class CookieContainerExtension
    {
        public static IEnumerable<Cookie> GetAllCookies(this CookieContainer c)
        {
            var k = (Hashtable)c.GetType().GetField("m_domainTable", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(c);
            foreach (DictionaryEntry element in k)
            {
                var l = (SortedList)element.Value.GetType().GetField("m_list", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(element.Value);
                foreach (var e in l)
                {
                    var cl = (CookieCollection)((DictionaryEntry)e).Value;
                    foreach (Cookie fc in cl)
                    {
                        yield return fc;
                    }
                }
            }
        }
    }
}
