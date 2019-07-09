using InteractiveSeven.Core.Events;
using InteractiveSeven.Twitch.Model;

namespace InteractiveSeven.Twitch.Commands
{
    public class MakoCommand : BaseCommand
    {
        public MakoCommand() 
            : base(x => new []{"Mako"}, x => true)
        {
        }

        public override void Execute(in CommandData commandData)
        {
            //TODO: Restrict Access and/or add a cost


            DomainEvents.Raise(new MakoModeStarted());
        }
    }
}