using System.Linq;
using System.Threading.Tasks;
using InteractiveSeven.Core.Chat;
using InteractiveSeven.Core.Events;

namespace InteractiveSeven.Core.Commands.Admin
{
    public class I7Command : BaseCommand
    {
        public I7Command()
            : base(x => x.I7CommandWords, x => x.EnableModCommand)
        {
        }

        public override GamePlayEffects GamePlayEffects => GamePlayEffects.DisplayOnly;

        public override Task Execute(CommandData commandData)
        {
            if (commandData.User is { IsBroadcaster: false, IsMe: false, IsMod: false }) return Task.CompletedTask;

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

            return Task.CompletedTask;
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
