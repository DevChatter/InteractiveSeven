using System.Collections.Generic;
using InteractiveSeven.Core.Battle;
using InteractiveSeven.Core.Data;
using InteractiveSeven.Core.Diagnostics.Memory;
using InteractiveSeven.Core.Settings;
using InteractiveSeven.Twitch.Model;
using InteractiveSeven.Twitch.Payments;
using System.Linq;
using Tseng.GameData;
using TwitchLib.Client.Interfaces;

namespace InteractiveSeven.Twitch.Commands
{
    public class StatusEffectCommand : BaseCommand
    {
        private readonly ITwitchClient _twitchClient;
        private readonly IEquipmentAccessor _equipmentAccessor;
        private readonly IBattleInfoAccessor _battleInfoAccessor;
        private readonly IGameInfoAccessor _gameInfoAccessor;
        private readonly IStatusAccessor _statusAccessor;
        private readonly GameDatabase _gameDatabase;
        private readonly PaymentProcessor _paymentProcessor;

        private static string[] AllWords(CommandSettings settings)
            => Settings.BattleSettings.AllStatusEffects
                .SelectMany(effect => effect.Words)
                .ToArray();

        public StatusEffectCommand(ITwitchClient twitchClient, IEquipmentAccessor equipmentAccessor,
            IBattleInfoAccessor battleInfoAccessor, IGameInfoAccessor gameInfoAccessor,
            IStatusAccessor statusAccessor, GameDatabase gameDatabase,
            PaymentProcessor paymentProcessor)
            : base(AllWords, x => x.BattleSettings.AllowStatusEffects)
        {
            _twitchClient = twitchClient;
            _equipmentAccessor = equipmentAccessor;
            _battleInfoAccessor = battleInfoAccessor;
            _gameInfoAccessor = gameInfoAccessor;
            _statusAccessor = statusAccessor;
            _gameDatabase = gameDatabase;
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

            FF7SaveMap gameInfo = _gameInfoAccessor.GetGameInfoMap();

            var existingTargets = targets.Where(x => gameInfo.LiveParty[x.Index].Id != 255).ToList();

            var (validTargets, invalidTargets) = CheckTargetValidity(
                existingTargets,
                gameInfo.LiveParty,
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

        private bool CouldNotAfford(in int targetCount,
            StatusEffectSettings statusSettings,
            CommandData commandData)
        {
            GilTransaction gilTransaction = _paymentProcessor.ProcessPayment(
                commandData, statusSettings.Cost * targetCount, Settings.BattleSettings.AllowModOverride);

            return !gilTransaction.Paid;
        }

        private (List<Allies> valid, List<Allies> invalid)
            CheckTargetValidity(List<Allies> targets, CharacterRecord[] charRecords, StatusEffects effect)
        {
            var valid = new List<Allies>();
            var invalid = new List<Allies>();

            foreach (Allies target in targets)
            {
                CharacterRecord characterRecord = charRecords[target.Index];

                var accessory = _gameDatabase.AccessoryDatabase
                    .SingleOrDefault(x => x.Id == characterRecord.Accessory);

                if (accessory != null && accessory.ProtectsFrom(effect))
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