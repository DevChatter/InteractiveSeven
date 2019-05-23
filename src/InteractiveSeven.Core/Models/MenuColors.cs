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
            TopRight = Color.FromName("silver"),
            BotLeft = Color.FromName("silver"),
            BotRight = Color.FromName("black"),
        };

        public static MenuColors brendan = new MenuColors
        {
            TopLeft = Color.FromName("black"),
            TopRight = Color.FromName("red"),
            BotLeft = Color.FromName("red"),
            BotRight = Color.FromName("black"),
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
