using InteractiveSeven.Core.Events;
using InteractiveSeven.Twitch.Model;

namespace InteractiveSeven.Twitch.Commands
{
    public class RefreshCommand : BaseCommand
    {
        public RefreshCommand()
            : base(new[] { "refresh" }, x => true)
        {
        }

        public override void Execute(CommandData commandData)
        {
            if (IsAvailable())
            {
                DomainEvents.Raise(new RefreshEvent());
            }
        }

        private bool IsAvailable()
        {
            return true;
        }
    }
}