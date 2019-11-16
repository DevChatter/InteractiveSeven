using System;

namespace InteractiveSeven.Core
{
    public static class StringExtensions
    {
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