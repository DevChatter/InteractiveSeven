﻿using InteractiveSeven.Core.MvvmCommands;
using InteractiveSeven.Core.Services;
using InteractiveSeven.Core.Settings;
using System.Windows.Input;

namespace InteractiveSeven.Core.ViewModels
{
    public class SettingsViewModel
    {
        private readonly ISettingsStore _settingsStore;
        private readonly IDialogService _dialogService;

        public SettingsViewModel(ISettingsStore settingsStore, IDialogService dialogService,
            IShowTwitchAuthCommand showTwitchAuthCommand)
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
            OpenTwitchAuthWindow = showTwitchAuthCommand;
        }

        public ICommand SaveSettingsCommand { get; }
        public ICommand LoadSettingsCommand { get; }
        public ICommand OpenTwitchAuthWindow { get; }


        public ApplicationSettings Settings => ApplicationSettings.Instance;
        public TwitchSettings TwitchSettings => TwitchSettings.Instance;
    }
}
