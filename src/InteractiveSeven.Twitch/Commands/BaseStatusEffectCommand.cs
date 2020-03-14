using InteractiveSeven.Core.Battle;
using InteractiveSeven.Core.Diagnostics.Memory;
using InteractiveSeven.Core.Emitters;
using InteractiveSeven.Core.FinalFantasy.MemModels;
using InteractiveSeven.Core.FinalFantasy.Models;
using InteractiveSeven.Core.Settings;
using InteractiveSeven.Core.ViewModels;
using InteractiveSeven.Twitch.Model;
using InteractiveSeven.Twitch.Payments;
using System;
using TwitchLib.Client.Interfaces;

namespace InteractiveSeven.Twitch.Commands
{
    public abstract class BaseStatusEffectCommand : BaseCommand
    {
        protected ITwitchClient _twitchClient;
        protected PartyStatusViewModel _partyStatus;
        protected IStatusAccessor _statusAccessor;
        protected PaymentProcessor _paymentProcessor;
        protected IStatusHubEmitter _statusHubEmitter;

        protected BaseStatusEffectCommand(ITwitchClient twitchClient, PartyStatusViewModel partyStatus,
            IStatusAccessor statusAccessor, PaymentProcessor paymentProcessor, IStatusHubEmitter statusHubEmitter,
            Func<CommandSettings, string[]> commandFunc)
            : base(commandFunc, x => x.BattleSettings.AllowStatusEffects)
        {
            _twitchClient = twitchClient;
            _partyStatus = partyStatus;
            _statusAccessor = statusAccessor;
            _paymentProcessor = paymentProcessor;
            _statusHubEmitter = statusHubEmitter;
        }

        protected Character GetTargetedAlly(Allies ally)
        {
            return _partyStatus.Party[ally.Index];
        }

        protected bool CouldNotAfford(in int targetCount, CommandData commandData, int eachCost)
        {
            GilTransaction gilTransaction = _paymentProcessor.ProcessPayment(
                commandData, eachCost * targetCount, Settings.BattleSettings.AllowModOverride);

            return !gilTransaction.Paid;
        }

    }
}