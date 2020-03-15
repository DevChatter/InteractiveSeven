using InteractiveSeven.Core.Battle;
using InteractiveSeven.Core.Diagnostics.Memory;
using InteractiveSeven.Core.Emitters;
using InteractiveSeven.Core.FinalFantasy.Constants;
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
    public class StatusEffectCommand : BaseStatusEffectCommand
    {
        public StatusEffectCommand(ITwitchClient twitchClient, PartyStatusViewModel partyStatus,
            IStatusAccessor statusAccessor, PaymentProcessor paymentProcessor,
            IStatusHubEmitter statusHubEmitter)
            : base(twitchClient, partyStatus, statusAccessor, paymentProcessor, statusHubEmitter, AllWords)
        {
        }

        private static string[] AllWords(CommandSettings settings)
            => Settings.BattleSettings.AllStatusEffects
                .SelectMany(effect => effect.Words)
                .ToArray();

        public override void Execute(in CommandData commandData)
        {
            var statusSettings = Settings.BattleSettings.ByWord(commandData.CommandText);
            List<Allies> targeted = Allies.ByWord(commandData.Arguments.FirstOrDefault());
            if (statusSettings == null || !targeted.Any())
            {
                _twitchClient.SendMessage(commandData.Channel,
                    "Be sure to name a valid status and actor. Example: !psn top");
                return;
            }

            if (!statusSettings.Enabled)
            {
                _twitchClient.SendMessage(commandData.Channel,
                    $"The {statusSettings.Name} status effect is disabled.");
                return;
            }

            var targets = CheckTargetValidity(targeted, _partyStatus.Party, statusSettings.Effect);

            if (CouldNotAfford(targets.valid.Count, statusSettings, commandData))
            {
                return;
            }

            foreach (Allies invalidTarget in targets.safeFrom)
            {
                Character character = GetTargetedCharacter(invalidTarget);
                string message = $"Can't apply {statusSettings.Name} to {character.Name}.";
                _twitchClient.SendMessage(commandData.Channel, message);
            }

            foreach (Allies invalidTarget in targets.hasEffect)
            {
                Character character = GetTargetedCharacter(invalidTarget);
                string message = $"{statusSettings.Name} already affects {character.Name}.";
                _twitchClient.SendMessage(commandData.Channel, message);
            }

            foreach (Allies target in targets.valid)
            {
                Character character = GetTargetedCharacter(target);
                _statusAccessor.SetActorStatus(target, statusSettings.Effect);
                string message = $"Applied {statusSettings.Name} to {character.Name}.";
                _twitchClient.SendMessage(commandData.Channel, message);
                _statusHubEmitter.ShowEvent(message);
            }
        }

        protected bool CouldNotAfford(in int targetCount, StatusEffectSettings statusSettings,
            CommandData commandData)
        {
            GilTransaction gilTransaction = _paymentProcessor.ProcessPayment(
                commandData, statusSettings.Cost * targetCount, Settings.BattleSettings.AllowModOverride);

            return !gilTransaction.Paid;
        }

        protected (List<Allies> valid, List<Allies> safeFrom, List<Allies> hasEffect) CheckTargetValidity(
            IEnumerable<Allies> targets, Character[] charRecords, StatusEffects effect)
        {
            var valid = new List<Allies>();
            var safeFrom = new List<Allies>();
            var hasEffect = new List<Allies>();

            foreach (Allies target in targets
                .Where(x => _partyStatus?.Party?[x.Index]?.Id != FF7Const.Empty))
            {
                Character characterRecord = charRecords[target.Index];
                if (characterRecord == null) continue;

                if (characterRecord.Accessory?.ProtectsFrom(effect) ?? false)
                {
                    safeFrom.Add(target);
                }
                else if (IsInPyramid(characterRecord))
                {
                    safeFrom.Add(target);
                }
                else if (characterRecord.CurrentHp == 0)
                {
                    safeFrom.Add(target);
                }
                else if (characterRecord.HasStatus(effect))
                {
                    hasEffect.Add(target);
                }
                else
                {
                    valid.Add(target);
                }
            }

            return (valid, safeFrom, hasEffect);
        }

        private static bool IsInPyramid(Character characterRecord)
        {
            return characterRecord.HasStatus(StatusEffects.Imprisoned);
        }
    }
}
