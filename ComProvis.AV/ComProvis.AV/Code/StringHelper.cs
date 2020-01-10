using System;
using System.Text;
using System.Text.RegularExpressions;

namespace ComProvis.AV.Code
{
    public class StringHelper
    {
        #region CleanString
        public static string CleanString(string input)
        {
            var result = Regex.Replace(input, @"[ ]{2,}", " ").Trim();
            return Regex.Replace(result, @"[^a-zA-Z]", "");
        }

        public static string CleanEmailString(string input)
        {
            var result = Regex.Replace(input, @"[ ]{2,}", " ").Trim();
            result = Regex.Replace(result, @"[.]{2,}", ".").Trim();
            if (result.StartsWith(".")) result = result.Substring(1, result.Length - 1);
            if (result.EndsWith(".")) result = result.Substring(0, result.Length - 1);
            return Regex.Replace(result, @"[^a-zA-Z0-9.\-_]", "");
        }

        public static string CleanHRChars(string str)
        {
            return str.
                    Replace("š", "s").
                    Replace("đ", "d").
                    Replace("ž", "z").
                    Replace("č", "c").
                    Replace("ć", "c").
                    Replace("Š", "S").
                    Replace("Đ", "D").
                    Replace("Ž", "Z").
                    Replace("Č", "C").
                    Replace("Ć", "C");
        }
        #endregion

        #region RandomizerInCaseOfEmptyString
        public static string RandomString(int size)
        {
            var builder = new StringBuilder();
            var random = new Random();
            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }
            return builder.ToString().ToLower();
        }
        #endregion

        #region MatchCollection
        public static string MatchCollectionToString(MatchCollection collection)
        {
            var sb = new StringBuilder();
            foreach (Match m in collection)
            {
                sb.Append(m.Value);
            }
            if (sb.ToString() == string.Empty) sb.Append("0");
            return sb.ToString();
        }
        #endregion
    }
}
