using InteractiveSeven.Core.Diagnostics.Memory;
using InteractiveSeven.Core.Settings;
using InteractiveSeven.Core.ViewModels;
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

        protected BaseStatusEffectCommand(ITwitchClient twitchClient, PartyStatusViewModel partyStatus,
            IStatusAccessor statusAccessor, PaymentProcessor paymentProcessor,
            Func<CommandSettings, string[]> commandFunc)
            : base(commandFunc, x => x.BattleSettings.AllowStatusEffects)
        {
            _twitchClient = twitchClient;
            _partyStatus = partyStatus;
            _statusAccessor = statusAccessor;
            _paymentProcessor = paymentProcessor;
        }
    }
}