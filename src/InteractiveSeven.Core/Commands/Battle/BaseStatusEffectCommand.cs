using System;
using InteractiveSeven.Core.Battle;
using InteractiveSeven.Core.Chat;
using InteractiveSeven.Core.Diagnostics.Memory;
using InteractiveSeven.Core.Emitters;
using InteractiveSeven.Core.FinalFantasy.Models;
using InteractiveSeven.Core.Payments;
using InteractiveSeven.Core.Settings;
using InteractiveSeven.Core.ViewModels;

namespace InteractiveSeven.Core.Commands.Battle
{
    public abstract class BaseStatusEffectCommand : BaseCommand
    {
        protected IChatClient _chatClient;
        protected PartyStatusViewModel _partyStatus;
        protected IStatusAccessor _statusAccessor;
        protected PaymentProcessor _paymentProcessor;
        protected IStatusHubEmitter _statusHubEmitter;

        protected BaseStatusEffectCommand(IChatClient chatClient, PartyStatusViewModel partyStatus,
            IStatusAccessor statusAccessor, PaymentProcessor paymentProcessor, IStatusHubEmitter statusHubEmitter,
            Func<CommandSettings, string[]> commandFunc)
            : base(commandFunc, x => x.BattleSettings.AllowStatusEffects)
        {
            _chatClient = chatClient;
            _partyStatus = partyStatus;
            _statusAccessor = statusAccessor;
            _paymentProcessor = paymentProcessor;
            _statusHubEmitter = statusHubEmitter;
        }


        protected Character GetTargetedCharacter(Allies ally)
        {
            return _partyStatus.Party[ally.Index];
        }

    }
}
