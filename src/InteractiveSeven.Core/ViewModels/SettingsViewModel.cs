using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using InteractiveSeven.Core.MvvmCommands;
using InteractiveSeven.Core.Services;
using InteractiveSeven.Core.Settings;

namespace InteractiveSeven.Core.ViewModels
{
    public partial class SettingsViewModel
    {
        private readonly ISettingsStore _settingsStore;
        private readonly IDialogService _dialogService;

        public SettingsViewModel(ISettingsStore settingsStore, IDialogService dialogService,
            IShowTwitchAuthCommand showTwitchAuthCommand)
        {
            _settingsStore = settingsStore;
            _dialogService = dialogService;
            OpenTwitchAuthWindow = showTwitchAuthCommand;
        }

        [RelayCommand]
        public void SaveSettings()
        {
            if (_dialogService.ConfirmDialog("Saving will overwrite your previously saved settings. OK?"))
            {
                _settingsStore.SaveSettings();
            }
        }

        [RelayCommand]
        public void LoadSettings()
        {
            if (_dialogService.ConfirmDialog("Loading will replace your current settings. OK?"))
            {
                _settingsStore.LoadSettings();
            }
        }

        public ICommand OpenTwitchAuthWindow { get; }


        public ApplicationSettings Settings => ApplicationSettings.Instance;
        public TwitchSettings TwitchSettings => TwitchSettings.Instance;
    }
}
