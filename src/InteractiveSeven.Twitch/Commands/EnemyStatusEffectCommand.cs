using InteractiveSeven.Core.Battle;
using InteractiveSeven.Core.Diagnostics.Memory;
using InteractiveSeven.Core.Emitters;
using InteractiveSeven.Core.FinalFantasy.MemModels;
using InteractiveSeven.Core.Settings;
using InteractiveSeven.Core.ViewModels;
using InteractiveSeven.Twitch.Model;
using InteractiveSeven.Twitch.Payments;
using System.Collections.Generic;
using System.Linq;
using TwitchLib.Client.Interfaces;

namespace InteractiveSeven.Twitch.Commands
{
    public class EnemyStatusEffectCommand : BaseStatusEffectCommand
    {
        private readonly PartyStatusViewModel _statusViewModel;

        public EnemyStatusEffectCommand(ITwitchClient twitchClient, PartyStatusViewModel partyStatus,
            IStatusAccessor statusAccessor, PaymentProcessor paymentProcessor, PartyStatusViewModel statusViewModel,
            IStatusHubEmitter statusHubEmitter)
            : base(twitchClient, partyStatus, statusAccessor, paymentProcessor, statusHubEmitter, AllWords)
        {
            _statusViewModel = statusViewModel;
        }

        private static string[] AllWords(CommandSettings settings)
            => settings.EnemyStatusCommandWords;

        public override void Execute(in CommandData commandData)
        {
            var statusSettings = Settings.BattleSettings.ByWord(commandData.Arguments.FirstOrDefault());

            List<Enemies> enemies = Enemies.ByWord(commandData.Arguments.ElementAtOrDefault(1));
            var actors = _statusViewModel.Enemies.Where(x => x.Exists && x.Alive).Select((actor, index) => (actor, index)).ToList();

            List<(Enemies enemy, BattleActor actor)> targeted = enemies.Join(actors,
                x => x.Index, x => x.index, (e, b) => (e, b))
                .Select(x => (x.e, x.b.actor))
                .ToList();

            if (statusSettings == null || !enemies.Any())
            {
                _twitchClient.SendMessage(commandData.Channel, "Be sure to name a valid status and actor. Example: !cure psn top");
                return;
            }

            if (!statusSettings.Enabled)
            {
                _twitchClient.SendMessage(commandData.Channel, $"The {statusSettings.Name} status effect is disabled.");
                return;
            }

            var targets = CheckTargetValidity(targeted, statusSettings.Effect);

            if (CouldNotAfford(targets.valid.Count, commandData, statusSettings.Cost))
            {
                return;
            }

            foreach (var invalidTarget in targets.hasEffect)
            {
                // TODO: Get some identifier for the enemy to mention here.
                string message = $"{statusSettings.Name} already affects enemy {invalidTarget.enemy.Words.First()}.";
                _twitchClient.SendMessage(commandData.Channel, message);
            }

            foreach (var target in targets.valid)
            {
                // TODO: Get some identifier for the enemy to mention here.
                _statusAccessor.SetActorStatus(target.enemy, statusSettings.Effect);
                string message = $"Applied {statusSettings.Name} to a enemy {target.enemy.Words.First()}.";
                _twitchClient.SendMessage(commandData.Channel, message);
                _statusHubEmitter.ShowEvent(message);
            }
        }

        private (List<(Enemies enemy, BattleActor actor)> valid, List<(Enemies enemy, BattleActor actor)> hasEffect)
            CheckTargetValidity(List<(Enemies enemy, BattleActor actor)> targeted, StatusEffects statusEffects)
        {
            var valid = new List<(Enemies enemy, BattleActor actor)>();
            var hasEffect = new List<(Enemies enemy, BattleActor actor)>();

            foreach (var target in targeted)
            {
                if (target.actor.HasStatus(statusEffects))
                {
                    hasEffect.Add(target);
                }
                else
                {
                    valid.Add(target);
                }
            }
            return (valid, hasEffect);
        }
    }
}