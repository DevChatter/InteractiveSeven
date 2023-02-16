using System.Collections.Generic;
using System.Linq;
using InteractiveSeven.Core.Battle;
using InteractiveSeven.Core.Chat;
using InteractiveSeven.Core.Diagnostics.Memory;
using InteractiveSeven.Core.Emitters;
using InteractiveSeven.Core.FinalFantasy.Constants;
using InteractiveSeven.Core.FinalFantasy.Models;
using InteractiveSeven.Core.Models;
using InteractiveSeven.Core.Payments;
using InteractiveSeven.Core.Settings;
using InteractiveSeven.Core.ViewModels;

namespace InteractiveSeven.Core.Commands.Battle
{
    public class HealStatusEffectCommand : BaseStatusEffectCommand
    {
        public HealStatusEffectCommand(IChatClient chatClient, PartyStatusViewModel partyStatus,
            IStatusAccessor statusAccessor, PaymentProcessor paymentProcessor,
            IStatusHubEmitter statusHubEmitter)
            : base(chatClient, partyStatus, statusAccessor, paymentProcessor, statusHubEmitter, AllWords)
        {
        }

        private static string[] AllWords(CommandSettings settings)
            => settings.HealCommandWords;

        public override void Execute(in CommandData commandData)
        {
            var statusSettings = Settings.BattleSettings.ByWord(commandData.Arguments.FirstOrDefault());
            List<Allies> targeted = Allies.ByWord(commandData.Arguments.ElementAtOrDefault(1));
            if (statusSettings == null || !targeted.Any())
            {
                _chatClient.SendMessage(commandData.Channel, "Be sure to name a valid status and actor. Example: !cure psn top");
                return;
            }

            if (!statusSettings.Enabled)
            {
                _chatClient.SendMessage(commandData.Channel, $"The {statusSettings.Name} status effect is disabled.");
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
                string message = $"{character.Name} is immune to {statusSettings.Name}.";
                _chatClient.SendMessage(commandData.Channel, message);
            }

            foreach (Allies invalidTarget in targets.unaffected)
            {
                Character character = GetTargetedCharacter(invalidTarget);
                string message = $"{character.Name} is not affected by {statusSettings.Name}.";
                _chatClient.SendMessage(commandData.Channel, message);
            }

            foreach (Allies target in targets.valid)
            {
                Character character = GetTargetedCharacter(target);
                _statusAccessor.RemoveActorStatus(target, statusSettings.Effect);
                string message = $"Removed {statusSettings.Name} from {character.Name}.";
                _chatClient.SendMessage(commandData.Channel, message);
                _statusHubEmitter.ShowEvent(message);
            }
        }

        protected bool CouldNotAfford(in int targetCount, StatusEffectSettings statusSettings,
            CommandData commandData)
        {
            GilTransaction gilTransaction = _paymentProcessor.ProcessPayment(
                commandData, statusSettings.CureCost * targetCount, Settings.BattleSettings.AllowModOverride);

            return !gilTransaction.Paid;
        }

        protected (List<Allies> valid, List<Allies> safeFrom, List<Allies> unaffected)
            CheckTargetValidity(IEnumerable<Allies> targets, Character[] charRecords, StatusEffects effect)
        {
            var valid = new List<Allies>();
            var safeFrom = new List<Allies>();
            var unaffected = new List<Allies>();

            foreach (Allies target in targets.Where(x => _partyStatus.Party[x.Index].Id != FF7Const.Empty))
            {
                Character characterRecord = charRecords[target.Index];

                if (characterRecord.Accessory?.ProtectsFrom(effect) ?? false)
                {
                    safeFrom.Add(target);
                }
                else if ((characterRecord.StatusEffectsValue & effect) == 0)
                {
                    unaffected.Add(target);
                }
                else
                {
                    valid.Add(target);
                }
            }

            return (valid, safeFrom, unaffected);
        }
    }
}
