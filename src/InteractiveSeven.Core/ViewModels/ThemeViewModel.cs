using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using InteractiveSeven.Core.MvvmCommands;
using InteractiveSeven.Core.Services;

namespace InteractiveSeven.Core.ViewModels
{
    public class ThemeViewModel : INotifyPropertyChanged
    {
        private KeyValuePair<string, string> _selectedAccentColor;

        public KeyValuePair<string, string> SelectedAccentColor
        {
            get => _selectedAccentColor;
            set
            {
                _selectedAccentColor = value;
                OnPropertyChanged(nameof(SelectedAccentColor));
            }
        }

        public ICommand ChangeTheme { get; }
        public ICommand ChangeBaseColor { get; }
        public ICommand ChangeAccentColor { get; }

        public List<KeyValuePair<string, string>> Colors { get; set; }

        public ThemeViewModel(IThemeChanger themeChanger)
        {
            ChangeTheme = new StringCommand(themeChanger.ChangeTheme);

            ChangeBaseColor = new StringCommand(themeChanger.ChangeBaseColor);

            ChangeAccentColor = new StringCommand(themeChanger.ChangeAccentColor);

            //SelectedAccentColor.Subscribe()

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

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
