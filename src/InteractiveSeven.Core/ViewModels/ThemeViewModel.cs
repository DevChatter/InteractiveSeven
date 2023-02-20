using System.Collections.Generic;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using InteractiveSeven.Core.Services;

namespace InteractiveSeven.Core.ViewModels
{
    public partial class ThemeViewModel : ObservableObject
    {
        private readonly IThemeChanger _themeChanger;

        [ObservableProperty]
        private KeyValuePair<string, string> _selectedAccentColor;

        [RelayCommand]
        public void ChangeTheme(string themeName) => _themeChanger.ChangeTheme(themeName);

        [RelayCommand]
        public void ChangeBaseColor(string baseColor) => _themeChanger.ChangeBaseColor(baseColor);

        [RelayCommand]
        public void ChangeAccentColor(string accentColor) => _themeChanger.ChangeAccentColor(accentColor);

        public List<KeyValuePair<string, string>> Colors { get; set; }

        public ThemeViewModel(IThemeChanger themeChanger)
        {
            _themeChanger = themeChanger;

            Colors = themeChanger.GetAccentColors()
                .Select(x => new KeyValuePair<string, string>(x, x))
                .ToList();

            this.PropertyChanged += (_, args) =>
            {
                if (args.PropertyName == nameof(SelectedAccentColor))
                {
                    themeChanger.ChangeAccentColor(SelectedAccentColor.Key);
                }
            };
        }
    }
}
