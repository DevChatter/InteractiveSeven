using InteractiveSeven.Core;
using InteractiveSeven.Twitch.Model;
using System.Linq;

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

        public abstract void Execute(CommandData commandData);
    }
}