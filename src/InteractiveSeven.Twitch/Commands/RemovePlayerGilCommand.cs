using InteractiveSeven.Core;
using InteractiveSeven.Core.Diagnostics.Memory;
using InteractiveSeven.Core.Emitters;
using InteractiveSeven.Twitch.Model;
using InteractiveSeven.Twitch.Payments;
using System.Linq;
using TwitchLib.Client.Interfaces;

namespace InteractiveSeven.Twitch.Commands
{
    public class RemovePlayerGilCommand : BaseCommand
    {
        private readonly PaymentProcessor _paymentProcessor;
        private readonly ITwitchClient _twitchClient;
        private readonly IGilAccessor _gilAccessor;
        private readonly IStatusHubEmitter _statusHubEmitter;

        public RemovePlayerGilCommand(PaymentProcessor paymentProcessor, ITwitchClient twitchClient,
            IGilAccessor gilAccessor, IStatusHubEmitter statusHubEmitter)
            : base(x => x.RemovePlayerGilCommandWords,
                x => x.EquipmentSettings.PlayerGilSettings.RemoveGilEnabled)
        {
            _paymentProcessor = paymentProcessor;
            _twitchClient = twitchClient;
            _gilAccessor = gilAccessor;
            _statusHubEmitter = statusHubEmitter;
        }

        public override void Execute(in CommandData commandData)
        {
            int amount = commandData.Arguments.FirstOrDefault().SafeIntParse();

            if (amount <= 0)
            {
                _twitchClient.SendMessage(commandData.Channel,
                    $"How much of your gil do you want to take from the player, {commandData.User.Username}?");
                return;
            }

            uint gilToRemove = (uint)(amount * Settings.EquipmentSettings.PlayerGilSettings.RemoveMultiplier);
            uint currentGil = _gilAccessor.GetGil();
            if (gilToRemove > currentGil)
            {
                // TODO: Adjust their request to remove all gil.
                _twitchClient.SendMessage(commandData.Channel,
                    $"Player doesn't have {gilToRemove} gil.");
                return;
            }

            GilTransaction gilTransaction = _paymentProcessor.ProcessPayment(
                commandData, amount,
                Settings.EquipmentSettings.PlayerGilSettings.AllowModOverride);

            if (!gilTransaction.Paid)
            {
                _twitchClient.SendMessage(commandData.Channel,
                    $"You don't have {amount} gil, {commandData.User.Username}.");
                return;
            }

            _gilAccessor.RemoveGil(gilToRemove);
            string message = $"Removed {gilToRemove} gil from player.";
            _twitchClient.SendMessage(commandData.Channel, message);
            _statusHubEmitter.ShowEvent(message);
        }
    }
}