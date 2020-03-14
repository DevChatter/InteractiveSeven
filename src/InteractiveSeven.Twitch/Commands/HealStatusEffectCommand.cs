using InteractiveSeven.Core.Battle;
using InteractiveSeven.Core.Diagnostics.Memory;
using InteractiveSeven.Core.Emitters;
using InteractiveSeven.Core.FinalFantasy.Models;
using InteractiveSeven.Core.Settings;
using InteractiveSeven.Core.ViewModels;
using InteractiveSeven.Twitch.Model;
using InteractiveSeven.Twitch.Payments;
using System.Collections.Generic;
using System.Linq;
using Tseng.Constants;
using TwitchLib.Client.Interfaces;

namespace InteractiveSeven.Twitch.Commands
{
    public class HealStatusEffectCommand : BaseStatusEffectCommand
    {
        public HealStatusEffectCommand(ITwitchClient twitchClient, PartyStatusViewModel partyStatus,
            IStatusAccessor statusAccessor, PaymentProcessor paymentProcessor,
            IStatusHubEmitter statusHubEmitter)
            : base(twitchClient, partyStatus, statusAccessor, paymentProcessor, statusHubEmitter, AllWords)
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
                _twitchClient.SendMessage(commandData.Channel, "Be sure to name a valid status and actor. Example: !cure psn top");
                return;
            }

            if (!statusSettings.Enabled)
            {
                _twitchClient.SendMessage(commandData.Channel, $"The {statusSettings.Name} status effect is disabled.");
                return;
            }

            var targets = CheckTargetValidity(targeted, _partyStatus.Party, statusSettings.Effect);

            if (CouldNotAfford(targets.valid.Count, commandData, statusSettings.CureCost))
            {
                return;
            }

            foreach (Allies invalidTarget in targets.safeFrom)
            {
                Character character = GetTargetedAlly(invalidTarget);
                string message = $"{character.Name} is immune to {statusSettings.Name}.";
                _twitchClient.SendMessage(commandData.Channel, message);
            }

            foreach (Allies invalidTarget in targets.unaffected)
            {
                Character character = GetTargetedAlly(invalidTarget);
                string message = $"{character.Name} is not affected by {statusSettings.Name}.";
                _twitchClient.SendMessage(commandData.Channel, message);
            }

            foreach (Allies target in targets.valid)
            {
                Character character = GetTargetedAlly(target);
                _statusAccessor.RemoveActorStatus(target, statusSettings.Effect);
                string message = $"Removed {statusSettings.Name} from {character.Name}.";
                _twitchClient.SendMessage(commandData.Channel, message);
                _statusHubEmitter.ShowEvent(message);
            }
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
