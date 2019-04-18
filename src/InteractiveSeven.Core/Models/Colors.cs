using InteractiveSeven.Core.Exceptions;
using System.Collections.Generic;

namespace InteractiveSeven.Core.Models
{
    public class Colors
    {
        private static readonly Dictionary<string, Colors> _all = new Dictionary<string, Colors>();

        public string Name { get; }
        public byte Red { get; }
        public byte Blue { get; }
        public byte Green { get; }

        private Colors(string name, byte red, byte green, byte blue)
        {
            Name = name;
            Red = red;
            Green = green;
            Blue = blue;

            Colors._all.Add(name, this);
        }

        public static IReadOnlyCollection<Colors> All => _all.Values;

        public static bool IsValid(string name)
        {
            name = name.ToLower();
            return _all.ContainsKey(name);
        }

        public static Colors ByName(string name)
        {
            name = name.ToLower();
            if (_all.TryGetValue(name, out Colors color))
            {
                return color;
            }
            throw new InvalidColorException(name);
        }

        public static Colors aliceblue = new Colors("aliceblue", 240, 248, 255);
        public static Colors antiquewhite = new Colors("antiquewhite", 250, 235, 215);
        public static Colors aqua = new Colors("aqua", 0, 255, 255);
        public static Colors aquamarine = new Colors("aquamarine", 127, 255, 212);
        public static Colors azure = new Colors("azure", 240, 255, 255);
        public static Colors beige = new Colors("beige", 245, 245, 220);
        public static Colors bisque = new Colors("bisque", 255, 228, 196);
        public static Colors black = new Colors("black", 0, 0, 0);
        public static Colors blanchedalmond = new Colors("blanchedalmond", 255, 235, 205);
        public static Colors blue = new Colors("blue", 0, 0, 255);
        public static Colors blueviolet = new Colors("blueviolet", 138, 43, 226);
        public static Colors brown = new Colors("brown", 165, 42, 42);
        public static Colors burlywood = new Colors("burlywood", 222, 184, 135);
        public static Colors cadetblue = new Colors("cadetblue", 95, 158, 160);
        public static Colors chartreuse = new Colors("chartreuse", 127, 255, 0);
        public static Colors chocolate = new Colors("chocolate", 210, 105, 30);
        public static Colors coral = new Colors("coral", 255, 127, 80);
        public static Colors cornflowerblue = new Colors("cornflowerblue", 100, 149, 237);
        public static Colors cornsilk = new Colors("cornsilk", 255, 248, 220);
        public static Colors crimson = new Colors("crimson", 220, 20, 60);
        public static Colors cyan = new Colors("cyan", 0, 255, 255);
        public static Colors darkblue = new Colors("darkblue", 0, 0, 139);
        public static Colors darkcyan = new Colors("darkcyan", 0, 139, 139);
        public static Colors darkgoldenrod = new Colors("darkgoldenrod", 184, 134, 11);
        public static Colors darkgray = new Colors("darkgray", 169, 169, 169);
        public static Colors darkgreen = new Colors("darkgreen", 0, 100, 0);
        public static Colors darkkhaki = new Colors("darkkhaki", 189, 183, 107);
        public static Colors darkmagenta = new Colors("darkmagenta", 139, 0, 139);
        public static Colors darkolivegreen = new Colors("darkolivegreen", 85, 107, 47);
        public static Colors darkorange = new Colors("darkorange", 255, 140, 0);
        public static Colors darkorchid = new Colors("darkorchid", 153, 50, 204);
        public static Colors darkred = new Colors("darkred", 139, 0, 0);
        public static Colors darksalmon = new Colors("darksalmon", 233, 150, 122);
        public static Colors darkseagreen = new Colors("darkseagreen", 143, 188, 143);
        public static Colors darkslateblue = new Colors("darkslateblue", 72, 61, 139);
        public static Colors darkslategray = new Colors("darkslategray", 47, 79, 79);
        public static Colors darkturquoise = new Colors("darkturquoise", 0, 206, 209);
        public static Colors darkviolet = new Colors("darkviolet", 148, 0, 211);
        public static Colors deeppink = new Colors("deeppink", 255, 20, 147);
        public static Colors deepskyblue = new Colors("deepskyblue", 0, 191, 255);
        public static Colors dimgray = new Colors("dimgray", 105, 105, 105);
        public static Colors dodgerblue = new Colors("dodgerblue", 30, 144, 255);
        public static Colors firebrick = new Colors("firebrick", 178, 34, 34);
        public static Colors floralwhite = new Colors("floralwhite", 255, 250, 240);
        public static Colors forestgreen = new Colors("forestgreen", 34, 139, 34);
        public static Colors fuchsia = new Colors("fuchsia", 255, 0, 255);
        public static Colors gainsboro = new Colors("gainsboro", 220, 220, 220);
        public static Colors ghostwhite = new Colors("ghostwhite", 248, 248, 255);
        public static Colors gold = new Colors("gold", 255, 215, 0);
        public static Colors goldenrod = new Colors("goldenrod", 218, 165, 32);
        public static Colors gray = new Colors("gray", 128, 128, 128);
        public static Colors green = new Colors("green", 0, 128, 0);
        public static Colors greenyellow = new Colors("greenyellow", 173, 255, 47);
        public static Colors honeydew = new Colors("honeydew", 240, 255, 240);
        public static Colors hotpink = new Colors("hotpink", 255, 105, 180);
        public static Colors indianred = new Colors("indianred", 205, 92, 92);
        public static Colors indigo = new Colors("indigo", 75, 0, 130);
        public static Colors ivory = new Colors("ivory", 255, 255, 240);
        public static Colors khaki = new Colors("khaki", 240, 230, 140);
        public static Colors lavender = new Colors("lavender", 230, 230, 250);
        public static Colors lavenderblush = new Colors("lavenderblush", 255, 240, 245);
        public static Colors lawngreen = new Colors("lawngreen", 124, 252, 0);
        public static Colors lemonchiffon = new Colors("lemonchiffon", 255, 250, 205);
        public static Colors lightblue = new Colors("lightblue", 173, 216, 230);
        public static Colors lightcoral = new Colors("lightcoral", 240, 128, 128);
        public static Colors lightcyan = new Colors("lightcyan", 224, 255, 255);
        public static Colors lightgoldenrodyellow = new Colors("lightgoldenrodyellow", 250, 250, 210);
        public static Colors lightgreen = new Colors("lightgreen", 144, 238, 144);
        public static Colors lightgrey = new Colors("lightgrey", 211, 211, 211);
        public static Colors lightpink = new Colors("lightpink", 255, 182, 193);
        public static Colors lightsalmon = new Colors("lightsalmon", 255, 160, 122);
        public static Colors lightseagreen = new Colors("lightseagreen", 32, 178, 170);
        public static Colors lightskyblue = new Colors("lightskyblue", 135, 206, 250);
        public static Colors lightslategray = new Colors("lightslategray", 119, 136, 153);
        public static Colors lightsteelblue = new Colors("lightsteelblue", 176, 196, 222);
        public static Colors lightyellow = new Colors("lightyellow", 255, 255, 224);
        public static Colors lime = new Colors("lime", 0, 255, 0);
        public static Colors limegreen = new Colors("limegreen", 50, 205, 50);
        public static Colors linen = new Colors("linen", 250, 240, 230);
        public static Colors magenta = new Colors("magenta", 255, 0, 255);
        public static Colors maroon = new Colors("maroon", 128, 0, 0);
        public static Colors mediumaquamarine = new Colors("mediumaquamarine", 102, 205, 170);
        public static Colors mediumblue = new Colors("mediumblue", 0, 0, 205);
        public static Colors mediumorchid = new Colors("mediumorchid", 186, 85, 211);
        public static Colors mediumpurple = new Colors("mediumpurple", 147, 112, 219);
        public static Colors mediumseagreen = new Colors("mediumseagreen", 60, 179, 113);
        public static Colors mediumslateblue = new Colors("mediumslateblue", 123, 104, 238);
        public static Colors mediumspringgreen = new Colors("mediumspringgreen", 0, 250, 154);
        public static Colors mediumturquoise = new Colors("mediumturquoise", 72, 209, 204);
        public static Colors mediumvioletred = new Colors("mediumvioletred", 199, 21, 133);
        public static Colors midnightblue = new Colors("midnightblue", 25, 25, 112);
        public static Colors mintcream = new Colors("mintcream", 245, 255, 250);
        public static Colors mistyrose = new Colors("mistyrose", 255, 228, 225);
        public static Colors moccasin = new Colors("moccasin", 255, 228, 181);
        public static Colors navajowhite = new Colors("navajowhite", 255, 222, 173);
        public static Colors navy = new Colors("navy", 0, 0, 128);
        public static Colors oldlace = new Colors("oldlace", 253, 245, 230);
        public static Colors olive = new Colors("olive", 128, 128, 0);
        public static Colors olivedrab = new Colors("olivedrab", 107, 142, 35);
        public static Colors orange = new Colors("orange", 255, 165, 0);
        public static Colors orangered = new Colors("orangered", 255, 69, 0);
        public static Colors orchid = new Colors("orchid", 218, 112, 214);
        public static Colors palegoldenrod = new Colors("palegoldenrod", 238, 232, 170);
        public static Colors palegreen = new Colors("palegreen", 152, 251, 152);
        public static Colors paleturquoise = new Colors("paleturquoise", 175, 238, 238);
        public static Colors palevioletred = new Colors("palevioletred", 219, 112, 147);
        public static Colors papayawhip = new Colors("papayawhip", 255, 239, 213);
        public static Colors peachpuff = new Colors("peachpuff", 255, 218, 185);
        public static Colors peru = new Colors("peru", 205, 133, 63);
        public static Colors pink = new Colors("pink", 255, 192, 203);
        public static Colors plum = new Colors("plum", 221, 160, 221);
        public static Colors powderblue = new Colors("powderblue", 176, 224, 230);
        public static Colors purple = new Colors("purple", 128, 0, 128);
        public static Colors red = new Colors("red", 255, 0, 0);
        public static Colors rosybrown = new Colors("rosybrown", 188, 143, 143);
        public static Colors royalblue = new Colors("royalblue", 65, 105, 225);
        public static Colors saddlebrown = new Colors("saddlebrown", 139, 69, 19);
        public static Colors salmon = new Colors("salmon", 250, 128, 114);
        public static Colors sandybrown = new Colors("sandybrown", 250, 164, 96);
        public static Colors seagreen = new Colors("seagreen", 46, 139, 87);
        public static Colors seashell = new Colors("seashell", 255, 245, 238);
        public static Colors sienna = new Colors("sienna", 160, 82, 45);
        public static Colors silver = new Colors("silver", 192, 192, 192);
        public static Colors skyblue = new Colors("skyblue", 135, 206, 235);
        public static Colors slateblue = new Colors("slateblue", 106, 90, 205);
        public static Colors slategray = new Colors("slategray", 112, 128, 144);
        public static Colors snow = new Colors("snow", 255, 250, 250);
        public static Colors springgreen = new Colors("springgreen", 0, 255, 127);
        public static Colors steelblue = new Colors("steelblue", 70, 130, 180);
        public static Colors tan = new Colors("tan", 210, 180, 140);
        public static Colors teal = new Colors("teal", 0, 128, 128);
        public static Colors thistle = new Colors("thistle", 216, 191, 216);
        public static Colors tomato = new Colors("tomato", 255, 99, 71);
        public static Colors turquoise = new Colors("turquoise", 64, 224, 208);
        public static Colors violet = new Colors("violet", 238, 130, 238);
        public static Colors wheat = new Colors("wheat", 245, 222, 179);
        public static Colors white = new Colors("white", 255, 255, 255);
        public static Colors whitesmoke = new Colors("whitesmoke", 245, 245, 245);
        public static Colors yellow = new Colors("yellow", 255, 255, 0);
        public static Colors yellowgreen = new Colors("yellowgreen", 154, 205, 50);

    }
}