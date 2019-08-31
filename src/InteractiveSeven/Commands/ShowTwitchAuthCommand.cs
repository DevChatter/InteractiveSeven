using InteractiveSeven.Core.MvvmCommands;
using InteractiveSeven.Core.ViewModels;
using System;
using System.Diagnostics;

namespace InteractiveSeven.Commands
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
            ProcessStartInfo psi = new ProcessStartInfo
            {
                FileName = _twitchAuthViewModel.AuthRequestUrl,
                UseShellExecute = true
            };
            Process.Start(psi);
        }

        public event EventHandler CanExecuteChanged;
    }
}