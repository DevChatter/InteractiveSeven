using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace InteractiveSeven.Core.Models
{
    public class ColorPaletteCollection
    {
        private readonly ILogger<ColorPaletteCollection> _logger;

        public ColorPaletteCollection(ILogger<ColorPaletteCollection> logger)
        {
            _logger = logger;
            try
            {
                All.AddRange(new[] { Default, Brendan, Tsuna, Strife });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error Initializing {nameof(ColorPaletteCollection)}", ex);
            }
        }

        public MenuColors ByName(string name) => All.SingleOrDefault(x => x.Names.Any(n => n.EqualsIns(name)))?.MenuColors;

        public List<ColorPalette> All { get; } = new List<ColorPalette>();

        public static ColorPalette Default = new ColorPalette(MenuColors.classic, "classic", "default", "original", "base");
        public static ColorPalette Brendan = new ColorPalette(MenuColors.brendan, "brendan", "brendoneus", "devchatter");
        public static ColorPalette Tsuna = new ColorPalette(MenuColors.tsuna, "tsuna", "tsunamods", "tsunamix");
        public static ColorPalette Strife = new ColorPalette(MenuColors.strife, "strife", "strife98");
    }
}
