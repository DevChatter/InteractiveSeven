using InteractiveSeven.Core.Events;
using InteractiveSeven.Twitch.Model;
using System;
using TwitchLib.Client.Interfaces;

namespace InteractiveSeven.Twitch.Commands
{
    public class RefreshCommand : BaseCommand
    {
        private static readonly TimeSpan _cooldownTime = TimeSpan.FromMinutes(1);
        private readonly ITwitchClient _twitchClient;
        private DateTime _nextAvailable = DateTime.UtcNow;
        public RefreshCommand(ITwitchClient twitchClient)
            : base(new[] { "refresh" }, x => true)
        {
            _twitchClient = twitchClient;
        }

        public override void Execute(CommandData commandData)
        {
            if (IsAvailable(commandData))
            {
                DomainEvents.Raise(new RefreshEvent());
                _nextAvailable = DateTime.UtcNow.Add(_cooldownTime);
            }
            else
            {
                _twitchClient.SendMessage(commandData.Channel, "The !refresh command is on cooldown.");
            }
        }

        private bool IsAvailable(CommandData data)
        {
            return data.User.IsBroadcaster || data.User.IsMod || DateTime.UtcNow > _nextAvailable;
        }
    }
}