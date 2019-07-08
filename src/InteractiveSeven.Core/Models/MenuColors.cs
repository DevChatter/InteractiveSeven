using System;
using System.Drawing;

namespace InteractiveSeven.Core.Models
{
    public class MenuColors
    {
        private static readonly Random Rand = new Random();

        public Color TopLeft { get; set; }
        public Color BotLeft { get; set; }
        public Color TopRight { get; set; }
        public Color BotRight { get; set; }

        public byte[] GetDisplayBytes()
        {
            return new byte[]{
                TopLeft.B,
                TopLeft.G,
                TopLeft.R,
                128,
                BotLeft.B,
                BotLeft.G,
                BotLeft.R,
                128,
                TopRight.B,
                TopRight.G,
                TopRight.R,
                128,
                BotRight.B,
                BotRight.G,
                BotRight.R,
                128
            };
        }

        public byte[] GetSaveBytes()
        {
            return new[] {
                TopLeft.R,
                TopLeft.G,
                TopLeft.B,
                BotLeft.R,
                BotLeft.G,
                BotLeft.B,
                TopRight.R,
                TopRight.G,
                TopRight.B,
                BotRight.R,
                BotRight.G,
                BotRight.B
            };

        }

        public static MenuColors Classic = new MenuColors
        {
            TopLeft = Color.FromArgb(0, 88, 176),
            TopRight = Color.FromArgb(0, 0, 80),
            BotLeft = Color.FromArgb(0, 0, 128),
            BotRight = Color.FromArgb(0, 0, 32)
        };

        public static MenuColors Tsuna = new MenuColors
        {
            TopLeft = Color.FromName("black"),
            TopRight = Color.FromName("gray"),
            BotLeft = Color.FromName("gray"),
            BotRight = Color.FromName("black"),
        };

        public static MenuColors Brendan = new MenuColors
        {
            TopLeft = Color.FromName("DarkRed"),
            TopRight = Color.FromName("Black"),
            BotLeft = Color.FromName("Black"),
            BotRight = Color.FromName("DarkRed"),
        };

        public static MenuColors Strife = new MenuColors
        {
            TopLeft = Color.FromName("DarkGreen"),
            TopRight = Color.FromName("black"),
            BotLeft = Color.FromName("black"),
            BotRight = Color.FromName("DarkGreen"),
        };

        public static MenuColors RandomPalette()
        {
            return new MenuColors
            {
                TopLeft = GetRandomColor(),
                TopRight = GetRandomColor(),
                BotLeft = GetRandomColor(),
                BotRight = GetRandomColor()
            };
            Color GetRandomColor()
            {
                byte[] b = new byte[3];
                Rand.NextBytes(b);
                return Color.FromArgb(b[0], b[1], b[2]);
            }
        }
    }
}
