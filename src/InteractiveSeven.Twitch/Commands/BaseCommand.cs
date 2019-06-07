using InteractiveSeven.Core;
using InteractiveSeven.Core.Settings;
using InteractiveSeven.Twitch.Model;
using System;
using System.Linq;

namespace InteractiveSeven.Twitch.Commands
{
    public abstract class BaseCommand : ITwitchCommand
    {
        private readonly string[] _commandWords;
        private readonly Func<ApplicationSettings, bool> _enableCheck;

        protected BaseCommand(string[] commandWords, Func<ApplicationSettings, bool> enableCheck)
        {
            _commandWords = commandWords;
            _enableCheck = enableCheck;
        }

        public virtual bool ShouldExecute(string commandWord)
        {
            return _enableCheck(ApplicationSettings.Instance)
                && _commandWords.Any(word => word.EqualsIns(commandWord));
        }

        public abstract void Execute(CommandData commandData);
    }
}