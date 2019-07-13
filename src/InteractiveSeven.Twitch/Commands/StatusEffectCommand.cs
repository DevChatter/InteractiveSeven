using InteractiveSeven.Core.Battle;
using InteractiveSeven.Core.Memory;
using InteractiveSeven.Core.Settings;
using InteractiveSeven.Twitch.Model;
using InteractiveSeven.Twitch.Payments;
using System.Linq;
using TwitchLib.Client.Interfaces;

namespace InteractiveSeven.Twitch.Commands
{
    public class StatusEffectCommand : BaseCommand
    {
        private readonly ITwitchClient _twitchClient;
        private readonly IStatusAccessor _statusAccessor;
        private readonly PaymentProcessor _paymentProcessor;

        private static string[] AllWords(CommandSettings settings)
            => Settings.BattleSettings.AllStatusEffects
                .SelectMany(effect => effect.Words)
                .ToArray();


        public StatusEffectCommand(ITwitchClient twitchClient,
            IStatusAccessor statusAccessor, PaymentProcessor paymentProcessor)
            : base(AllWords, x => x.BattleSettings.AllowStatusEffects)
        {
            _twitchClient = twitchClient;
            _statusAccessor = statusAccessor;
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

            GilTransaction gilTransaction = _paymentProcessor.ProcessPayment(commandData,
                statusSettings.Cost,
                Settings.BattleSettings.AllowModOverride);

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