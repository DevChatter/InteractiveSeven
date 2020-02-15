namespace InteractiveSeven.Core.Models
{

    public class ColorPalette
    {
        public MenuColors MenuColors { get; internal set; }
        public string[] Names { get; }

        public ColorPalette(MenuColors menuColors, params string[] names)
        {
            MenuColors = menuColors;
            Names = names;
        }
    }
}
