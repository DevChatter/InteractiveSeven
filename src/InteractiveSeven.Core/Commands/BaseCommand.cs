﻿using System;
using System.Linq;
using System.Threading.Tasks;
using InteractiveSeven.Core.Chat;
using InteractiveSeven.Core.Settings;

namespace InteractiveSeven.Core.Commands
{
    public abstract class BaseCommand : IChatCommand
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

        public abstract Task Execute(CommandData commandData);
    }
}
