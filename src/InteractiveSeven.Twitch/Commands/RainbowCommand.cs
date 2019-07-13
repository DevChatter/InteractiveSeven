using InteractiveSeven.Core.Events;
using InteractiveSeven.Twitch.Model;

namespace InteractiveSeven.Twitch.Commands
{
    public class RainbowCommand : BaseCommand
    {
        public RainbowCommand()
            : base(x => x.RainbowCommandWords, x => x.MenuSettings.EnableRainbowCommand)
        {
        }

        public override void Execute(in CommandData commandData)
        {
            if (!commandData.User.IsBroadcaster)
            {
                return;
            }

            DomainEvents.Raise(new RainbowModeStarted());
        }
    }
}