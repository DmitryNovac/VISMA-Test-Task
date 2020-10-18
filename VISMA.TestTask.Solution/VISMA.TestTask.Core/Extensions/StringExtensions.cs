using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace VISMA.TestTask.Core.Extensions
{
    public static class StringExtensions
    {
        public static string ToCapitalize(this string str)
        {
            str = str.Trim().ToLower();

            str = Regex.Replace(str, "[ ]{1,}", " ");
            str = Regex.Replace(str, "[ ]*-[ ]*", "-");

            return string.Join(" ", str.Split(' ').ToList().Select(Capitalize));
        }

        private static string Capitalize(string str)
        {
            if (str.Contains('-'))
            {
                return string.Join("-", str.Split('-')
                    .ToList()
                    .Select(o => o.Substring(0, 1).ToUpper() + o.Substring(1).ToLower()));
            }

            return str.Substring(0, 1).ToUpper() + str.Substring(1).ToLower();
        }
    }
}
