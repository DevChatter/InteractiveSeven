using System;

namespace InteractiveSeven.Core
{
    public static class StringExtensions
    {
        public static bool EqualsIns(this string a, string b)
        {
            return a.Equals(b, StringComparison.OrdinalIgnoreCase);
        }
    }
}