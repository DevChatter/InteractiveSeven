using System;
using System.Threading.Tasks;
using InteractiveSeven.Core.Chat;
using Microsoft.Extensions.Logging;

namespace InteractiveSeven.Core.Commands.Decorators
{
    public class LoggingCommand<T> : IChatCommand where T : IChatCommand
    {
        private readonly T _internalCommand;
        private readonly ILogger<LoggingCommand<T>> _logger;

        public LoggingCommand(T internalCommand, ILogger<LoggingCommand<T>> logger)
        {
            _internalCommand = internalCommand;
            _logger = logger;
        }

        public virtual GamePlayEffects GamePlayEffects => _internalCommand.GamePlayEffects;

        public async Task Execute(CommandData commandData)
        {
            try
            {
                await _internalCommand.Execute(commandData);
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
