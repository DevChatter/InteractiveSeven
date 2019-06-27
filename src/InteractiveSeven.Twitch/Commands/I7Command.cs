using InteractiveSeven.Core.Events;
using InteractiveSeven.Twitch.Model;
using System.Linq;

namespace InteractiveSeven.Twitch.Commands
{
    public class I7Command : BaseCommand
    {
        public I7Command()
            : base(x => x.I7CommandWords, x => true) // TODO: Add a Setting to Allow this Command
        {
        }

        public override void Execute(CommandData commandData)
        {
            if (!commandData.User.IsBroadcaster && !commandData.User.IsMe && !commandData.User.IsMod) return;

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
            }
        }

        private void RemoveName(CommandData commandData)
        {
            if (Settings.NameBiddingSettings.AllowModeration)
            {
                var name = commandData.Arguments.ElementAtOrDefault(1);
                if (!string.IsNullOrWhiteSpace(name))
                {
                    DomainEvents.Raise(new RemovingName(name));
                }
            }
        }
    }
}