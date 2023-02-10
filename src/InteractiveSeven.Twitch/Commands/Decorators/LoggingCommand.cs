using System;
using InteractiveSeven.Core;
using InteractiveSeven.Twitch.Model;
using Microsoft.Extensions.Logging;

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

        public virtual GamePlayEffects GamePlayEffects => _internalCommand.GamePlayEffects;

        public void Execute(in CommandData commandData)
        {
            try
            {
                _internalCommand.Execute(commandData);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled Command Exception");
            }
        }

        public bool ShouldExecute(string commandWord)
        {
            return _internalCommand.ShouldExecute(commandWord);
        }
    }
}