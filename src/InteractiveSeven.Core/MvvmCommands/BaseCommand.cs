using System;
using System.Windows.Input;

namespace InteractiveSeven.Core.MvvmCommands
{
    public class BaseCommand<T> : ICommand
    {
        private readonly Action<T> _executeAction;

        public BaseCommand(Action<T> executeAction) => _executeAction = executeAction;

        public bool CanExecute(object parameter) => parameter is T;

        public void Execute(object parameter) => _executeAction?.Invoke((T)parameter);

        public event EventHandler CanExecuteChanged;
    }
}
