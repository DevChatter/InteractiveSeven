using System.Threading.Tasks;
using InteractiveSeven.Core.Chat;
using InteractiveSeven.Core.Events;

namespace InteractiveSeven.Core.Commands
{
    public class RefreshCommand : BaseCommand
    {
        private readonly CooldownTracker _cooldownTracker;
        public RefreshCommand()
            : base(x => x.RefreshCommandWords, x => true)
        {
            _cooldownTracker = new CooldownTracker(1);
        }

        public override GamePlayEffects GamePlayEffects => GamePlayEffects.DisplayOnly;

        public override async Task Execute(CommandData commandData, IChatClient chatClient)
        {
            if (IsAvailable(commandData))
            {
                DomainEvents.Raise(new RefreshEvent());
                _cooldownTracker.Run(commandData.User);
            }
            else
            {
                await chatClient.SendMessage(commandData.Channel, "The !refresh command is on cooldown.");
            }
        }

        private bool IsAvailable(in CommandData data)
        {
            return data.User.IsBroadcaster || data.User.IsMod || _cooldownTracker.IsReady;
        }
    }
}
