using InteractiveSeven.Core.Battle;
using InteractiveSeven.Core.Diagnostics.Memory;
using InteractiveSeven.Core.ViewModels;
using InteractiveSeven.Twitch.Model;
using InteractiveSeven.Twitch.Payments;
using System.Collections.Generic;
using System.Linq;
using Tseng.Constants;
using TwitchLib.Client.Interfaces;

namespace InteractiveSeven.Twitch.Commands
{
    public class EsunaCommand : BaseCommand
    {
        private readonly ITwitchClient _twitchClient;
        private readonly PartyStatusViewModel _partyStatus;
        private readonly IStatusAccessor _statusAccessor;
        private readonly PaymentProcessor _paymentProcessor;

        public EsunaCommand(ITwitchClient twitchClient, PartyStatusViewModel partyStatus,
            IStatusAccessor statusAccessor, PaymentProcessor paymentProcessor)
            : base(x => x.EsunaCommandWords, x => x.BattleSettings.AllowEsunaCommand)
        {
            _twitchClient = twitchClient;
            _partyStatus = partyStatus;
            _statusAccessor = statusAccessor;
            _paymentProcessor = paymentProcessor;
        }

        public override void Execute(in CommandData commandData)
        {
            List<Allies> targeted = Allies.ByWord(commandData.Arguments.FirstOrDefault());
            if (!targeted.Any())
            {
                _twitchClient.SendMessage(commandData.Channel, "Be sure to name a valid actor. Example: !esuna top");
                return;
            }

            var validTargets = CheckTargetValidity(targeted);

            if (CouldNotAfford(validTargets.Count, commandData))
            {
                return;
            }

            foreach (Allies target in validTargets)
            {
                _statusAccessor.ClearNegativeStatuses(target);
                _twitchClient.SendMessage(commandData.Channel,
                    $"Removed Negative Effects from {target.Words.First()}.");
            }
        }

        protected bool CouldNotAfford(in int targetCount, CommandData commandData)
        {
            GilTransaction gilTransaction = _paymentProcessor.ProcessPayment(
                commandData, Settings.BattleSettings.EsunaCost * targetCount,
                Settings.BattleSettings.AllowModOverride);

            return !gilTransaction.Paid;
        }

        protected List<Allies> CheckTargetValidity(IEnumerable<Allies> targets)
        {
            return targets.Where(x => _partyStatus.Party[x.Index].Id != FF7Const.Empty).ToList();
        }
    }
}