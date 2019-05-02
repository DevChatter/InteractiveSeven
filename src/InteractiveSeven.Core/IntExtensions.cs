using System;

namespace InteractiveSeven.Core
{
    public static class IntExtensions
    {
        public static int HexToInt(this string src) => Convert.ToInt32(src, 16);
    }
}