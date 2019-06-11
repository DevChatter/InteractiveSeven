using System.Drawing;

namespace InteractiveSeven.Core.Models
{
    public class MenuColors
    {
        public Color TopLeft { get; set; }
        public Color BotLeft { get; set; }
        public Color TopRight { get; set; }
        public Color BotRight { get; set; }

        public static MenuColors classic = new MenuColors
        {
            TopLeft = Color.FromArgb(0, 88, 176),
            TopRight = Color.FromArgb(0, 0, 80),
            BotLeft = Color.FromArgb(0, 0, 128),
            BotRight = Color.FromArgb(0, 0, 32)
        };

        public static MenuColors tsuna = new MenuColors
        {
            TopLeft = Color.FromName("black"),
            TopRight = Color.FromName("gray"),
            BotLeft = Color.FromName("gray"),
            BotRight = Color.FromName("black"),
        };

        public static MenuColors brendan = new MenuColors
        {
            TopLeft = Color.FromName("DarkRed"),
            TopRight = Color.FromName("Black"),
            BotLeft = Color.FromName("Black"),
            BotRight = Color.FromName("DarkRed"),
        };

        public static MenuColors strife = new MenuColors
        {
            TopLeft = Color.FromName("DarkGreen"),
            TopRight = Color.FromName("black"),
            BotLeft = Color.FromName("black"),
            BotRight = Color.FromName("DarkGreen"),
        };
    }
}
