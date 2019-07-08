using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
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
        public static byte[] AsBytesBgr(this Color color)
        {
            return new[] { color.B, color.G, color.R };
        }

        /// <summary>
        /// Return Color as bytes in RGB format, which is used for saving only.
        /// </summary>
        /// <param name="color"></param>
        /// <returns>bytes in order Red, Green, Blue</returns>
        public static byte[] AsBytesRgb(this Color color)
        {
            return new[] { color.R, color.G, color.B };
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

        public static bool IsNamedColor(this string text)
        {
            string adjustedText = text.ToLowerInvariant().Replace("grey", "gray");
            return ValidColors.Contains(adjustedText);
        }

        public static bool IsColor(this string text)
        {
            return text.IsHexFormat() || text.IsNamedColor();
        }

        private static readonly HashSet<string> ValidColors = new HashSet<string>
        {
            "aliceblue",
            "antiquewhite",
            "aqua",
            "aquamarine",
            "azure",
            "beige",
            "bisque",
            "black",
            "blanchedalmond",
            "blue",
            "blueviolet",
            "brown",
            "burlywood",
            "cadetblue",
            "chartreuse",
            "chocolate",
            "coral",
            "cornflowerblue",
            "cornsilk",
            "crimson",
            "cyan",
            "darkblue",
            "darkcyan",
            "darkgoldenrod",
            "darkgray",
            "darkgreen",
            "darkkhaki",
            "darkmagenta",
            "darkolivegreen",
            "darkorange",
            "darkorchid",
            "darkred",
            "darksalmon",
            "darkseagreen",
            "darkslateblue",
            "darkslategray",
            "darkturquoise",
            "darkviolet",
            "deeppink",
            "deepskyblue",
            "dimgray",
            "dodgerblue",
            "firebrick",
            "floralwhite",
            "forestgreen",
            "fuchsia",
            "gainsboro",
            "ghostwhite",
            "gold",
            "goldenrod",
            "gray",
            "green",
            "greenyellow",
            "honeydew",
            "hotpink",
            "indianred",
            "indigo",
            "ivory",
            "khaki",
            "lavender",
            "lavenderblush",
            "lawngreen",
            "lemonchiffon",
            "lightblue",
            "lightcoral",
            "lightcyan",
            "lightgoldenrodyellow",
            "lightgray",
            "lightgreen",
            "lightpink",
            "lightsalmon",
            "lightseagreen",
            "lightskyblue",
            "lightslategray",
            "lightsteelblue",
            "lightyellow",
            "lime",
            "limegreen",
            "linen",
            "magenta",
            "maroon",
            "mediumaquamarine",
            "mediumblue",
            "mediumorchid",
            "mediumpurple",
            "mediumseagreen",
            "mediumslateblue",
            "mediumspringgreen",
            "mediumturquoise",
            "mediumvioletred",
            "midnightblue",
            "mintcream",
            "mistyrose",
            "moccasin",
            "navajowhite",
            "navy",
            "oldlace",
            "olive",
            "olivedrab",
            "orange",
            "orangered",
            "orchid",
            "palegoldenrod",
            "palegreen",
            "paleturquoise",
            "palevioletred",
            "papayawhip",
            "peachpuff",
            "peru",
            "pink",
            "plum",
            "powderblue",
            "purple",
            "red",
            "rosybrown",
            "royalblue",
            "saddlebrown",
            "salmon",
            "sandybrown",
            "seagreen",
            "seashell",
            "sienna",
            "silver",
            "skyblue",
            "slateblue",
            "slategray",
            "snow",
            "springgreen",
            "steelblue",
            "tan",
            "teal",
            "thistle",
            "tomato",
            "turquoise",
            "violet",
            "wheat",
            "white",
            "whitesmoke",
            "yellow",
            "yellowgreen",
        };
    }
}