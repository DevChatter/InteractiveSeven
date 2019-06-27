using InteractiveSeven.Core.MvvmCommands;
using InteractiveSeven.Core.Settings;
using System.Windows.Input;

namespace InteractiveSeven.Core.ViewModels
{
    public class SettingsViewModel
    {
        private readonly ISettingsStore _settingsStore;
        private readonly IDialogService _dialogService;

        public SettingsViewModel(ISettingsStore settingsStore, IDialogService dialogService)
        {
            _settingsStore = settingsStore;
            _dialogService = dialogService;
            SaveSettingsCommand = new SimpleCommand(x =>
            {
                if (_dialogService.ConfirmDialog("Saving will overwrite your previously saved settings. OK?"))
                {
                    _settingsStore.SaveSettings();
                }
            });
            LoadSettingsCommand = new SimpleCommand(x =>
            {
                if (_dialogService.ConfirmDialog("Loading will replace your current settings. OK?"))
                {
                    _settingsStore.LoadSettings();
                }
            });
        }

        public ICommand SaveSettingsCommand { get; }
        public ICommand LoadSettingsCommand { get; }

        public ApplicationSettings Settings => ApplicationSettings.Instance;
    }
}
