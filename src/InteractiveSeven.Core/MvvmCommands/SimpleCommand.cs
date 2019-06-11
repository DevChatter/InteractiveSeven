using System;
using System.Windows.Input;

namespace InteractiveSeven.Core.MvvmCommands
{
    public class SimpleCommand : ICommand
    {
        private readonly Action<object> _executeAction;

        public SimpleCommand(Action<object> executeAction) => _executeAction = executeAction;

        public bool CanExecute(object parameter) => true;

        public void Execute(object parameter) => _executeAction?.Invoke(parameter);

        public event EventHandler CanExecuteChanged;
    }
}
