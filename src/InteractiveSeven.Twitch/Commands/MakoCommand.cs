using InteractiveSeven.Core.Events;
using InteractiveSeven.Twitch.Model;

namespace InteractiveSeven.Twitch.Commands
{
    public class MakoCommand : BaseCommand
    {
        public MakoCommand() 
            : base(x => x.MakoCommandWords, x => x.MenuSettings.EnableMakoCommand)
        {
        }

        public override void Execute(in CommandData commandData)
        {
            if (!commandData.User.IsBroadcaster)
            {
                return;
            }

            //TODO: Add a cost and allow others' access


            DomainEvents.Raise(new MakoModeStarted());
        }
    }
}