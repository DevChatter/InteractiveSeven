using System;
using System.Text.RegularExpressions;

namespace InteractiveSeven.Core
{
    public static class StringExtensions
    {
        private static readonly Regex WhiteSpaceRegex = new Regex(@"\s+");
        public static string NoSpaces(this string text) => WhiteSpaceRegex.Replace(text, "");

        public static bool StartsWithIns(this string a, string b)
        {
            return a?.StartsWith(b ?? "", StringComparison.OrdinalIgnoreCase) ?? true;
        }

        public static bool EqualsIns(this string a, string b)
        {
            return a.Equals(b, StringComparison.OrdinalIgnoreCase);
        }

        public static int SafeIntParse(this string text)
        {
            int.TryParse(text, out int result);
            return result;
        }

        public static string NoAt(this string text) => text.TrimStart('@');
    }
}