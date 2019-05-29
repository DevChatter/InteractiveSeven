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
            SaveSettingsCommand = new SimpleCommand(x => _settingsStore.SaveSettings());
        }

        public ICommand SaveSettingsCommand { get; }

        public ApplicationSettings Settings => ApplicationSettings.Instance;
    }
}
