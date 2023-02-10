using System.Collections.Generic;
using System.Linq;
using InteractiveSeven.Core.Battle;
using InteractiveSeven.Core.Diagnostics.Memory;
using InteractiveSeven.Core.Emitters;
using InteractiveSeven.Core.FinalFantasy.Constants;
using InteractiveSeven.Core.FinalFantasy.Models;
using InteractiveSeven.Core.ViewModels;
using InteractiveSeven.Twitch.Model;
using InteractiveSeven.Twitch.Payments;
using TwitchLib.Client.Interfaces;

namespace InteractiveSeven.Twitch.Commands
{
    public class EsunaCommand : BaseCommand
    {
        private readonly ITwitchClient _twitchClient;
        private readonly PartyStatusViewModel _partyStatus;
        private readonly IStatusAccessor _statusAccessor;
        private readonly PaymentProcessor _paymentProcessor;
        private readonly IStatusHubEmitter _statusHubEmitter;

        public EsunaCommand(ITwitchClient twitchClient, PartyStatusViewModel partyStatus,
            IStatusAccessor statusAccessor, PaymentProcessor paymentProcessor,
            IStatusHubEmitter statusHubEmitter)
            : base(x => x.EsunaCommandWords,
                x => x.BattleSettings.AllowEsunaCommand && x.BattleSettings.AllowStatusEffects)
        {
            _twitchClient = twitchClient;
            _partyStatus = partyStatus;
            _statusAccessor = statusAccessor;
            _paymentProcessor = paymentProcessor;
            _statusHubEmitter = statusHubEmitter;
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
                Character character = GetTargetedCharacter(target);
                _statusAccessor.ClearNegativeStatuses(target);
                string message = $"Removed Negative Effects from {character.Name}.";
                _twitchClient.SendMessage(commandData.Channel, message);
                _statusHubEmitter.ShowEvent(message);
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
            return targets.Where(x => GetTargetedCharacter(x).Id != FF7Const.Empty).ToList();
        }

        private Character GetTargetedCharacter(Allies ally)
        {
            return _partyStatus.Party[ally.Index];
        }
    }
}