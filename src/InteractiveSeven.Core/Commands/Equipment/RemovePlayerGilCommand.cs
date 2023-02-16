using System.Linq;
using InteractiveSeven.Core.Chat;
using InteractiveSeven.Core.Diagnostics.Memory;
using InteractiveSeven.Core.Emitters;
using InteractiveSeven.Core.Models;
using InteractiveSeven.Core.Payments;

namespace InteractiveSeven.Core.Commands.Equipment
{
    public class RemovePlayerGilCommand : BaseCommand
    {
        private readonly PaymentProcessor _paymentProcessor;
        private readonly IChatClient _chatClient;
        private readonly IGilAccessor _gilAccessor;
        private readonly IStatusHubEmitter _statusHubEmitter;

        public RemovePlayerGilCommand(PaymentProcessor paymentProcessor, IChatClient chatClient,
            IGilAccessor gilAccessor, IStatusHubEmitter statusHubEmitter)
            : base(x => x.RemovePlayerGilCommandWords,
                x => x.EquipmentSettings.PlayerGilSettings.RemoveGilEnabled)
        {
            _paymentProcessor = paymentProcessor;
            _chatClient = chatClient;
            _gilAccessor = gilAccessor;
            _statusHubEmitter = statusHubEmitter;
        }

        public override void Execute(in CommandData commandData)
        {
            int amount = commandData.Arguments.FirstOrDefault().SafeIntParse();

            if (amount <= 0)
            {
                _chatClient.SendMessage(commandData.Channel,
                    $"How much of your gil do you want to take from the player, {commandData.User.Username}?");
                return;
            }

            uint gilToRemove = (uint)(amount * Settings.EquipmentSettings.PlayerGilSettings.RemoveMultiplier);
            uint currentGil = _gilAccessor.GetGil();
            if (gilToRemove > currentGil)
            {
                // TODO: Adjust their request to remove all gil.
                _chatClient.SendMessage(commandData.Channel,
                    $"Player doesn't have {gilToRemove} gil.");
                return;
            }

            GilTransaction gilTransaction = _paymentProcessor.ProcessPayment(
                commandData, amount,
                Settings.EquipmentSettings.PlayerGilSettings.AllowModOverride);

            if (!gilTransaction.Paid)
            {
                _chatClient.SendMessage(commandData.Channel,
                    $"You don't have {amount} gil, {commandData.User.Username}.");
                return;
            }

            _gilAccessor.RemoveGil(gilToRemove);
            string message = $"Removed {gilToRemove} gil from player.";
            _chatClient.SendMessage(commandData.Channel, message);
            _statusHubEmitter.ShowEvent(message);
        }
    }
}
