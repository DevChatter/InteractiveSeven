using InteractiveSeven.Core.MvvmCommands;
using System;
using Microsoft.Extensions.DependencyInjection;

namespace InteractiveSeven.Commands
{
    public class ShowTwitchAuthCommand : IShowTwitchAuthCommand
    {
        private readonly IServiceProvider _serviceProvider;

        public ShowTwitchAuthCommand(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public bool CanExecute(object parameter) => true;

        public void Execute(object parameter)
        {
            var authWindow = _serviceProvider.GetService<TwitchAuthWindow>();
            authWindow.Show();
        }

        public event EventHandler CanExecuteChanged;
    }
}