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
        private readonly ITwitchAuth _twitchAuth;

        public SettingsViewModel(ISettingsStore settingsStore,
            IDialogService dialogService,
            ITwitchAuth twitchAuth)
        {
            _settingsStore = settingsStore;
            _dialogService = dialogService;
            _twitchAuth = twitchAuth;
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

        [RelayCommand]
        public void OpenTwitchAuthWindow()
        {
            _twitchAuth.Show();
        }

        public ApplicationSettings Settings => ApplicationSettings.Instance;
        public TwitchSettings TwitchSettings => TwitchSettings.Instance;
    }
}
