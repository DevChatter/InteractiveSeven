using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InteractiveSeven.Core.Chat;
using InteractiveSeven.Core.Diagnostics.Memory;
using InteractiveSeven.Core.Emitters;
using InteractiveSeven.Core.FinalFantasy.Constants;
using InteractiveSeven.Core.FinalFantasy.Models;
using InteractiveSeven.Core.Payments;
using InteractiveSeven.Core.Settings;
using InteractiveSeven.Core.ViewModels;

namespace InteractiveSeven.Core.Commands.Battle
{
    public class StatusEffectCommand : BaseStatusEffectCommand
    {
        public StatusEffectCommand(IChatClient chatClient, PartyStatusViewModel partyStatus,
            IStatusAccessor statusAccessor, PaymentProcessor paymentProcessor,
            IStatusHubEmitter statusHubEmitter)
            : base(chatClient, partyStatus, statusAccessor, paymentProcessor, statusHubEmitter, AllWords)
        {
        }

        private static string[] AllWords(CommandSettings settings)
            => Settings.BattleSettings.AllStatusEffects
                .SelectMany(effect => effect.Words)
                .ToArray();

        public override async Task Execute(CommandData commandData)
        {
            var statusSettings = Settings.BattleSettings.ByWord(commandData.CommandText);
            List<Allies> targeted = Allies.ByWord(commandData.Arguments.FirstOrDefault());
            if (statusSettings == null || !targeted.Any())
            {
                await _chatClient.SendMessage(commandData.Channel,
                    "Be sure to name a valid status and actor. Example: !psn top");
                return;
            }

            if (!statusSettings.Enabled)
            {
                await _chatClient.SendMessage(commandData.Channel,
                    $"The {statusSettings.Name} status effect is disabled.");
                return;
            }

            var targets = CheckTargetValidity(targeted, _partyStatus.Party, statusSettings.Effect);

            if (await CouldNotAfford(targets.valid.Count, statusSettings, commandData))
            {
                return;
            }

            foreach (Allies invalidTarget in targets.safeFrom)
            {
                Character character = GetTargetedCharacter(invalidTarget);
                string message = $"Can't apply {statusSettings.Name} to {character.Name}.";
                await _chatClient.SendMessage(commandData.Channel, message);
            }

            foreach (Allies invalidTarget in targets.hasEffect)
            {
                Character character = GetTargetedCharacter(invalidTarget);
                string message = $"{statusSettings.Name} already affects {character.Name}.";
                await _chatClient.SendMessage(commandData.Channel, message);
            }

            foreach (Allies target in targets.valid)
            {
                Character character = GetTargetedCharacter(target);
                _statusAccessor.SetActorStatus(target, statusSettings.Effect);
                string message = $"Applied {statusSettings.Name} to {character.Name}.";
                await _chatClient.SendMessage(commandData.Channel, message);
                await _statusHubEmitter.ShowEvent(message);
            }
        }

        protected async Task<bool> CouldNotAfford(int targetCount, StatusEffectSettings statusSettings,
            CommandData commandData)
        {
            GilTransaction gilTransaction = await _paymentProcessor.ProcessPayment(
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
