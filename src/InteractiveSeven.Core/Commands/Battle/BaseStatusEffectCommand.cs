using System;
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
        protected readonly PartyStatusViewModel _partyStatus;
        protected readonly IStatusAccessor _statusAccessor;
        protected readonly PaymentProcessor _paymentProcessor;
        protected readonly IStatusHubEmitter _statusHubEmitter;

        protected BaseStatusEffectCommand(PartyStatusViewModel partyStatus,
            IStatusAccessor statusAccessor, PaymentProcessor paymentProcessor, IStatusHubEmitter statusHubEmitter,
            Func<CommandSettings, string[]> commandFunc)
            : base(commandFunc, x => x.BattleSettings.AllowStatusEffects)
        {
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
