using InteractiveSeven.Core.Events;
using InteractiveSeven.Twitch.Commands.Components;
using InteractiveSeven.Twitch.Model;
using TwitchLib.Client.Interfaces;

namespace InteractiveSeven.Twitch.Commands
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

        public override void Execute(CommandData commandData)
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

        private bool IsAvailable(CommandData data)
        {
            return data.User.IsBroadcaster || data.User.IsMod || _cooldownTracker.IsReady;
        }
    }
}