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

        public MenuColors ByName(string name) => PaletteByName(name)?.MenuColors;

        private ColorPalette PaletteByName(string name)
        {
            return All.SingleOrDefault(x => x.Names.Any(n => n.EqualsIns(name)));
        }

        public void RemovePalette(string name)
        {
            All.RemoveAll(x => x.Names.Any(n => n.EqualsIns(name)));
        }

        public void AddPalette(ColorPalette colorPalette)
        {
            All.Add(colorPalette);
            ExistingNames.UnionWith(colorPalette.Names);
        }

        public bool EditPalette(MenuColors menuColors, string paletteName)
        {
            ColorPalette palette = PaletteByName(paletteName);
            if (palette == null)
            {
                return false;
            }

            palette.MenuColors = menuColors;

            return true;
        }

        public void Load(List<ColorPalette> colorPalettes)
        {
            foreach (ColorPalette colorPalette in colorPalettes)
            {
                if (ExistingNames.Overlaps(colorPalette.Names))
                {
                    string[] validNames = colorPalette.Names.Except(ExistingNames).ToArray();
                    if (validNames.Any())
                    {
                        AddPalette(new ColorPalette(colorPalette.MenuColors, validNames));
                    }
                }
                else
                {
                    AddPalette(colorPalette);
                }
            }
        }

        private HashSet<string> _existingNames;
        private HashSet<string> ExistingNames => _existingNames ??= All.SelectMany(x => x.Names).ToHashSet();

        public List<ColorPalette> All { get; } = new List<ColorPalette>();

        public static ColorPalette Default = new ColorPalette(MenuColors.Classic, "Classic", "Default", "Original", "Base");
        public static ColorPalette Brendan = new ColorPalette(MenuColors.Brendan, "Brendan", "Brendoneus", "DevChatter");
        public static ColorPalette Tsuna = new ColorPalette(MenuColors.Tsuna, "Tsuna", "TsunaMods", "TsunaMix");
        public static ColorPalette Strife = new ColorPalette(MenuColors.Strife, "Strife", "Strife98");
    }
}
