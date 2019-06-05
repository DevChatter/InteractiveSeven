using InteractiveSeven.Twitch.Model;
using System;

namespace InteractiveSeven.Twitch.Commands.Decorators
{
    public class LoggingCommand<T> : ITwitchCommand where T : ITwitchCommand
    {
        private readonly T _internalCommand;

        public LoggingCommand(T internalCommand)
        {
            _internalCommand = internalCommand;
        }

        public void Execute(CommandData commandData)
        {
            try
            {
                _internalCommand.Execute(commandData);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public bool ShouldExecute(string commandWord)
        {
            return _internalCommand.ShouldExecute(commandWord);
        }
    }
}