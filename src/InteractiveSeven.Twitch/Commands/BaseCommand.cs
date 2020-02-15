using InteractiveSeven.Core;
using InteractiveSeven.Core.Settings;
using InteractiveSeven.Twitch.Model;
using System;
using System.Linq;

namespace InteractiveSeven.Twitch.Commands
{
    public abstract class BaseCommand : ITwitchCommand
    {
        private readonly Func<CommandSettings, string[]> _commandWordsSelector;
        private readonly Func<ApplicationSettings, bool> _enableCheck;

        protected static ApplicationSettings Settings => ApplicationSettings.Instance;

        protected BaseCommand(Func<CommandSettings, string[]> commandWordsSelector, Func<ApplicationSettings, bool> enableCheck)
        {
            _commandWordsSelector = commandWordsSelector;
            _enableCheck = enableCheck;
        }

        protected BaseCommand(string[] commandWords, Func<ApplicationSettings, bool> enableCheck)
        {
            _commandWordsSelector = x => commandWords;
            _enableCheck = enableCheck;
        }

        protected string DefaultCommandWord
            => _commandWordsSelector?.Invoke(Settings.CommandSettings)?.FirstOrDefault();

        public virtual GamePlayEffects GamePlayEffects => GamePlayEffects.MajorEffect;

        public virtual bool ShouldExecute(string commandWord)
        {
            return (Settings.GamePlayMode & GamePlayEffects) > 0
                && _enableCheck?.Invoke(Settings) == true
                && _commandWordsSelector?.Invoke(Settings.CommandSettings)?.Any(word => word.EqualsIns(commandWord)) == true;
        }

        public abstract void Execute(in CommandData commandData);
    }
}