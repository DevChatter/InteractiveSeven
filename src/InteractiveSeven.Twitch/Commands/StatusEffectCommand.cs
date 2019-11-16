using InteractiveSeven.Core.Battle;
using InteractiveSeven.Core.Data;
using InteractiveSeven.Core.Diagnostics.Memory;
using InteractiveSeven.Core.Settings;
using InteractiveSeven.Twitch.Model;
using InteractiveSeven.Twitch.Payments;
using System.Linq;
using System.Security.AccessControl;
using InteractiveSeven.Core.FinalFantasy.Models;
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
            var actor = Allies.ByWord(commandData.Arguments.FirstOrDefault());
            if (statusSettings == null || actor == null)
            {
                _twitchClient.SendMessage(commandData.Channel, "Be sure to name a valid status and actor. Example: !psn top");
                return;
            }

            if (!statusSettings.Enabled)
            {
                _twitchClient.SendMessage(commandData.Channel, $"The {commandData.CommandText} status effect is disabled.");
                return;
            }

            FF7BattleMap ff7BattleMap = _battleInfoAccessor.GetBattleMap();

            FF7SaveMap gameInfo = _gameInfoAccessor.GetGameInfoMap();

            CharacterRecord characterRecord = gameInfo.LiveParty[actor.Index];
            if (characterRecord.Id == 255)
            {
                // No character here.
                return;
            }

            var accessory = _gameDatabase.AccessoryDatabase
                .SingleOrDefault(x => x.Id == characterRecord.Accessory);

            if (accessory != null && accessory.ProtectsFrom(statusSettings.Effect))
            {
                string message = $"Can't apply {statusSettings.Name} to {actor.Words.First()}.";
                _twitchClient.SendMessage(commandData.Channel, message);
                return;
            }

            GilTransaction gilTransaction = _paymentProcessor.ProcessPayment(
                commandData, statusSettings.Cost, Settings.BattleSettings.AllowModOverride);

            if (!gilTransaction.Paid)
            {
                return;
            }

            _statusAccessor.SetActorStatus(actor, statusSettings.Effect);

            _twitchClient.SendMessage(commandData.Channel,
                $"Applied {commandData.CommandText} to {actor.Words.First()}.");
        }
    }
}