﻿using System.Threading.Tasks;
using InteractiveSeven.Core.Chat;
using InteractiveSeven.Core.Events;

namespace InteractiveSeven.Core.Commands
{
    public class RefreshCommand : BaseCommand
    {
        private readonly IChatClient _chatClient;
        private readonly CooldownTracker _cooldownTracker;
        public RefreshCommand(IChatClient chatClient)
            : base(x => x.RefreshCommandWords, x => true)
        {
            _chatClient = chatClient;
            _cooldownTracker = new CooldownTracker(1);
        }

        public override GamePlayEffects GamePlayEffects => GamePlayEffects.DisplayOnly;

        public override async Task Execute(CommandData commandData)
        {
            if (IsAvailable(commandData))
            {
                DomainEvents.Raise(new RefreshEvent());
                _cooldownTracker.Run(commandData.User);
            }
            else
            {
                await _chatClient.SendMessage(commandData.Channel, "The !refresh command is on cooldown.");
            }
        }

        private bool IsAvailable(in CommandData data)
        {
            return data.User.IsBroadcaster || data.User.IsMod || _cooldownTracker.IsReady;
        }
    }
}
