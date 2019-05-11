using System.Linq;
using InteractiveSeven.Core;
using TwitchLib.Client.Models;

namespace InteractiveSeven.Twitch.Commands
{
    public abstract class BaseCommand : ITwitchCommand
    {
        private readonly string[] _commandWords;

        protected BaseCommand(string[] commandWords)
        {
            _commandWords = commandWords;
        }

        public virtual bool ShouldExecute(string commandWord)
            => _commandWords.Any(word => word.EqualsIns(commandWord));

        public abstract void Execute(ChatCommand chatCommand);
    }
}