using System;

namespace InteractiveSeven.Core.MvvmCommands
{
    public class SimpleCommand : BaseCommand<object>
    {
        public SimpleCommand(Action<object> executeAction) : base(executeAction)
        {
        }
    }
}
