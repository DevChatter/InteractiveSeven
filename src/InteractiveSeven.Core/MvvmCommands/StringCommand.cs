using System;

namespace InteractiveSeven.Core.MvvmCommands
{
    public class StringCommand : BaseCommand<string>
    {
        public StringCommand(Action<string> executeAction) : base(executeAction)
        {
        }
    }
}
