namespace InteractiveSeven
{
    public static class ColorExtensions
    {
        public static System.Drawing.Color ToOther(this System.Windows.Media.Color color)
        {
            return System.Drawing.Color.FromArgb(color.R, color.G, color.B);
        }
        public static System.Windows.Media.Color ToOther(this System.Drawing.Color color)
        {
            return System.Windows.Media.Color.FromRgb(color.R, color.G, color.B);
        }
    }
}
