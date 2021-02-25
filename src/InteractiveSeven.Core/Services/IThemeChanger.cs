using System.Collections.Generic;

namespace InteractiveSeven.Core.Services
{
    public interface IThemeChanger
    {
        void ChangeTheme(string themeName);
        void ChangeBaseColor(string baseColor);
        void ChangeAccentColor(string accentColor);
        List<string> GetAccentColors();
    }
}
