using System.Windows;
using ControlzEx.Theming;
using InteractiveSeven.Core.Services;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Documents;

namespace InteractiveSeven.Services
{
    public class ThemeChanger : IThemeChanger
    {
        public void ChangeTheme(string themeName)
        {
            var theme = ThemeManager.Current.GetTheme(themeName);
            ThemeManager.Current.ChangeTheme(Application.Current, theme);
        }

        public void ChangeBaseColor(string baseColor)
        {
            ThemeManager.Current.ChangeThemeBaseColor(Application.Current, baseColor);
            Application.Current?.MainWindow?.Activate();
        }

        public void ChangeAccentColor(string accentColor)
        {
            ThemeManager.Current.ChangeThemeColorScheme(Application.Current, accentColor);
            Application.Current?.MainWindow?.Activate();
        }

        public List<string> GetAccentColors()
        {
            return ThemeManager.Current.ColorSchemes.ToList();
        }
    }
}
