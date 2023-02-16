using InteractiveSeven.Core.Events;
using InteractiveSeven.Core.Models;
using TwitchLib.Client.Interfaces;

namespace InteractiveSeven.Core.Commands
{
    public class RefreshCommand : BaseCommand
    {
        private readonly ITwitchClient _twitchClient;
        private readonly CooldownTracker _cooldownTracker;
        public RefreshCommand(ITwitchClient twitchClient)
            : base(x => x.RefreshCommandWords, x => true)
        {
            _twitchClient = twitchClient;
            _cooldownTracker = new CooldownTracker(1);
        }

        public override GamePlayEffects GamePlayEffects => GamePlayEffects.DisplayOnly;

        public override void Execute(in CommandData commandData)
        {
            if (IsAvailable(commandData))
            {
                DomainEvents.Raise(new RefreshEvent());
                _cooldownTracker.Run(commandData.User);
            }
            else
            {
                _twitchClient.SendMessage(commandData.Channel, "The !refresh command is on cooldown.");
            }
        }

        private bool IsAvailable(in CommandData data)
        {
            return data.User.IsBroadcaster || data.User.IsMod || _cooldownTracker.IsReady;
        }
    }
}
