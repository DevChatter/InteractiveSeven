using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace InteractiveSeven.Core.Models
{
    public class ColorPaletteCollection
    {
        public ColorPaletteCollection(ILogger<ColorPaletteCollection> logger)
        {
            try
            {
                All.AddRange(new[] { Default, Brendan, Tsuna, Strife });
            }
            catch (Exception ex)
            {
                logger.LogError($"Error Initializing {nameof(ColorPaletteCollection)}", ex);
            }
        }

        public MenuColors ByName(string name) => All.SingleOrDefault(x => x.Names.Any(n => n.EqualsIns(name)))?.MenuColors;

        public List<ColorPalette> All { get; } = new List<ColorPalette>();

        public static ColorPalette Default = new ColorPalette(MenuColors.Classic, "classic", "default", "original", "base");
        public static ColorPalette Brendan = new ColorPalette(MenuColors.Brendan, "brendan", "brendoneus", "devchatter");
        public static ColorPalette Tsuna = new ColorPalette(MenuColors.Tsuna, "tsuna", "tsunamods", "tsunamix");
        public static ColorPalette Strife = new ColorPalette(MenuColors.Strife, "strife", "strife98");
    }
}
