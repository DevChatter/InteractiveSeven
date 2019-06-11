using InteractiveSeven.Core.Events;
using InteractiveSeven.Twitch.Model;
using System.Linq;

namespace InteractiveSeven.Twitch.Commands
{
    public class I7Command : BaseCommand
    {
        public I7Command()
            : base(new[] { "i7", "iseven", "interactive" }, x => true) // TODO: Add a Setting to Allow this Command
        {
        }

        public override void Execute(CommandData commandData)
        {
            if (!commandData.IsBroadcaster && !commandData.IsMe && !commandData.IsMod) return;

            var action = commandData.Arguments.FirstOrDefault();

            switch (action)
            {
                case "help":
                    break;
                case "delete":
                case "remove":
                case "del":
                case "rem":
                    RemoveName(commandData);
                    break;
                default:
                    break;
            }
        }

        private void RemoveName(CommandData commandData)
        {
            var name = commandData.Arguments.ElementAtOrDefault(1);
            if (!string.IsNullOrWhiteSpace(name))
            {
                DomainEvents.Raise(new RemovingName(name));
            }
        }
    }
}