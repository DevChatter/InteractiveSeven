using System;
using System.Diagnostics;
using InteractiveSeven.Core.MvvmCommands;
using InteractiveSeven.Core.Settings;
using InteractiveSeven.Core.ViewModels;

namespace DevChatter.InteractiveGames.Core.Seven.Commands
{
    public class ShowTwitchAuthCommand : IShowTwitchAuthCommand
    {
        private readonly TwitchAuthViewModel _twitchAuthViewModel;

        public ShowTwitchAuthCommand(TwitchAuthViewModel twitchAuthViewModel)
        {
            _twitchAuthViewModel = twitchAuthViewModel;
        }

        public bool CanExecute(object parameter) => true;

        public void Execute(object parameter)
        {
            TwitchSettings.Instance.UpdatedFromTwitch = false;
            ProcessStartInfo psi = new ProcessStartInfo
            {
                FileName = _twitchAuthViewModel.AuthRequestUrl,
                UseShellExecute = true
            };
            Process.Start(psi);
        }

        public event EventHandler CanExecuteChanged = (sender, args) => { };
    }
}
