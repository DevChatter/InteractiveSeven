using InteractiveSeven.Core.Battle;
using InteractiveSeven.Core.Diagnostics.Memory;
using InteractiveSeven.Core.FinalFantasy.Models;
using InteractiveSeven.Core.Settings;
using InteractiveSeven.Core.ViewModels;
using InteractiveSeven.Twitch.Model;
using InteractiveSeven.Twitch.Payments;
using System.Collections.Generic;
using System.Linq;
using TwitchLib.Client.Interfaces;

namespace InteractiveSeven.Twitch.Commands
{
    public class StatusEffectCommand : BaseCommand
    {
        private readonly ITwitchClient _twitchClient;
        private readonly PartyStatusViewModel _partyStatus;
        private readonly IStatusAccessor _statusAccessor;
        private readonly PaymentProcessor _paymentProcessor;

        private static string[] AllWords(CommandSettings settings)
            => Settings.BattleSettings.AllStatusEffects
                .SelectMany(effect => effect.Words)
                .ToArray();

        public StatusEffectCommand(ITwitchClient twitchClient, PartyStatusViewModel partyStatus,
            IStatusAccessor statusAccessor, PaymentProcessor paymentProcessor)
            : base(AllWords, x => x.BattleSettings.AllowStatusEffects)
        {
            _twitchClient = twitchClient;
            _partyStatus = partyStatus;
            _statusAccessor = statusAccessor;
            _paymentProcessor = paymentProcessor;
        }

        public override void Execute(in CommandData commandData)
        {
            var statusSettings = Settings.BattleSettings.ByWord(commandData.CommandText);
            List<Allies> targets = Allies.ByWord(commandData.Arguments.FirstOrDefault());
            if (statusSettings == null || !targets.Any())
            {
                _twitchClient.SendMessage(commandData.Channel, "Be sure to name a valid status and actor. Example: !psn top");
                return;
            }

            if (!statusSettings.Enabled)
            {
                _twitchClient.SendMessage(commandData.Channel, $"The {commandData.CommandText} status effect is disabled.");
                return;
            }

            var existingTargets = targets.Where(x => _partyStatus.Party[x.Index].Id != 255);

            var (validTargets, invalidTargets) = CheckTargetValidity(
                existingTargets,
                _partyStatus.Party,
                statusSettings.Effect);

            if (CouldNotAfford(validTargets.Count, statusSettings, commandData))
            {
                return;
            }

            foreach (Allies invalidTarget in invalidTargets)
            {
                string message = $"Can't apply {statusSettings.Name} to {invalidTarget.Words.First()}.";
                _twitchClient.SendMessage(commandData.Channel, message);
            }

            foreach (Allies target in validTargets)
            {
                _statusAccessor.SetActorStatus(target, statusSettings.Effect);

                _twitchClient.SendMessage(commandData.Channel,
                    $"Applied {commandData.CommandText} to {target.Words.First()}.");
            }
        }

        private bool CouldNotAfford(in int targetCount, StatusEffectSettings statusSettings, 
            CommandData commandData)
        {
            GilTransaction gilTransaction = _paymentProcessor.ProcessPayment(
                commandData, statusSettings.Cost * targetCount, Settings.BattleSettings.AllowModOverride);

            return !gilTransaction.Paid;
        }

        private (List<Allies> valid, List<Allies> invalid) CheckTargetValidity(
            IEnumerable<Allies> targets, Character[] charRecords, StatusEffects effect)
        {
            var valid = new List<Allies>();
            var invalid = new List<Allies>();

            foreach (Allies target in targets)
            {
                Character characterRecord = charRecords[target.Index];

                if (characterRecord.Accessory?.ProtectsFrom(effect) ?? false)
                {
                    invalid.Add(target);
                }
                else
                {
                    valid.Add(target);
                }
            }

            return (valid, invalid);
        }
    }
}
