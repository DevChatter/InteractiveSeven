using InteractiveSeven.Core.Settings;
using InteractiveSeven.MvvmCommands;
using System.Windows.Input;

namespace InteractiveSeven.ViewModels
{
    public class SettingsViewModel
    {
        private readonly ISettingsStore _settingsStore;
        public SettingsViewModel(ISettingsStore settingsStore)
        {
            _settingsStore = settingsStore;
            SaveSettingsCommand = new SimpleCommand(SaveSettings);
            LoadSettingsCommand = new SimpleCommand(LoadSettings);
        }

        private void SaveSettings(object obj)
        {
            _settingsStore.SaveSettings();
        }

        private void LoadSettings(object obj)
        {
            _settingsStore.LoadSettings();
        }

        public ICommand SaveSettingsCommand { get; }
        public ICommand LoadSettingsCommand { get; }

        public ApplicationSettings Settings => ApplicationSettings.Instance;
    }
}
