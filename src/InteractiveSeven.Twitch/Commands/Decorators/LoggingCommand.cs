using InteractiveSeven.Twitch.Model;
using Microsoft.Extensions.Logging;
using System;

namespace InteractiveSeven.Twitch.Commands.Decorators
{
    public class LoggingCommand<T> : ITwitchCommand where T : ITwitchCommand
    {
        private readonly T _internalCommand;
        private readonly ILogger<LoggingCommand<T>> _logger;

        public LoggingCommand(T internalCommand, ILogger<LoggingCommand<T>> logger)
        {
            _internalCommand = internalCommand;
            _logger = logger;
        }

        public void Execute(CommandData commandData)
        {
            try
            {
                _internalCommand.Execute(commandData);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled Command Exception");
                Console.WriteLine(ex);
            }
        }

        public bool ShouldExecute(string commandWord)
        {
            return _internalCommand.ShouldExecute(commandWord);
        }
    }
}