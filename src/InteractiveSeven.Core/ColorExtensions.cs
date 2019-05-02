using System;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

namespace InteractiveSeven.Core
{
    public static class ColorExtensions
    {
        /// <summary>
        /// Create from direct memory read in FF7.
        /// Note: Order is BGR.
        /// </summary>
        /// <param name="bytes">Corner of 3 bytes in order Blue, Green, Red.</param>
        public static Color ToColor(this byte[] bytes)
        {
            return Color.FromArgb(bytes[2], bytes[1], bytes[0]);
        }

        /// <summary>
        /// Return Color as bytes formatted for use in FF7.
        /// Note: Order is BGR.
        /// </summary>
        /// <param name="color"></param>
        /// <returns>bytes in order Blue, Green, Red</returns>
        public static byte[] AsBytes(this Color color)
        {
            return new[] { color.B, color.G, color.R };
        }

        /// <summary>
        /// Converts a named or hex color in HTML style into a .net Color.
        /// Examples: #000 #FFF #0F0
        /// #A042CC #414142
        /// LightBlue, Red, Orange
        /// </summary>
        /// <param name="htmlColor">Color defined as would be used in HTML.</param>
        /// <returns></returns>
        public static Color ToColor(this string htmlColor)
        {
            if (string.IsNullOrEmpty(htmlColor)) return Color.Empty;

            if (htmlColor.IsHexFormat())
            {
                if (htmlColor.Length == 7)
                {
                    return Color.FromArgb(
                        htmlColor.Substring(1, 2).HexToInt(),
                        htmlColor.Substring(3, 2).HexToInt(),
                        htmlColor.Substring(5, 2).HexToInt());
                }

                string r = char.ToString(htmlColor[1]);
                string g = char.ToString(htmlColor[2]);
                string b = char.ToString(htmlColor[3]);

                return Color.FromArgb(
                    (r + r).HexToInt(),
                    (g + g).HexToInt(),
                    (b + b).HexToInt());
            }

            htmlColor = htmlColor.ToLower(CultureInfo.InvariantCulture);

            // Help Europeans
            if (htmlColor.Contains("grey"))
            {
                htmlColor = htmlColor.Replace("grey", "gray");
            }

            return Color.FromName(htmlColor);
        }

        private static readonly Regex HexRegex = new Regex("^#(?:[0-9a-fA-F]{3}){1,2}$");
        public static bool IsHexFormat(this string htmlColor)
        {
            return HexRegex.IsMatch(htmlColor);
        }
    }
}