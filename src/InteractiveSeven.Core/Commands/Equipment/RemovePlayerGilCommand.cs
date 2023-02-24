using System.Linq;
using System.Threading.Tasks;
using InteractiveSeven.Core.Chat;
using InteractiveSeven.Core.Diagnostics.Memory;
using InteractiveSeven.Core.Emitters;
using InteractiveSeven.Core.Payments;

namespace InteractiveSeven.Core.Commands.Equipment
{
    public class RemovePlayerGilCommand : BaseCommand
    {
        private readonly PaymentProcessor _paymentProcessor;
        private readonly IGilAccessor _gilAccessor;
        private readonly IStatusHubEmitter _statusHubEmitter;

        public RemovePlayerGilCommand(PaymentProcessor paymentProcessor,
            IGilAccessor gilAccessor, IStatusHubEmitter statusHubEmitter)
            : base(x => x.RemovePlayerGilCommandWords,
                x => x.EquipmentSettings.PlayerGilSettings.RemoveGilEnabled)
        {
            _paymentProcessor = paymentProcessor;
            _gilAccessor = gilAccessor;
            _statusHubEmitter = statusHubEmitter;
        }

        public override async Task Execute(CommandData commandData, IChatClient chatClient)
        {
            int amount = commandData.Arguments.FirstOrDefault().SafeIntParse();

            if (amount <= 0)
            {
                await chatClient.SendMessage(commandData.Channel,
                    $"How much of your gil do you want to take from the player, {commandData.User.Username}?");
                return;
            }

            uint gilToRemove = (uint)(amount * Settings.EquipmentSettings.PlayerGilSettings.RemoveMultiplier);
            uint currentGil = _gilAccessor.GetGil();
            if (gilToRemove > currentGil)
            {
                // TODO: Adjust their request to remove all gil.
                await chatClient.SendMessage(commandData.Channel,
                    $"Player doesn't have {gilToRemove} gil.");
                return;
            }

            GilTransaction gilTransaction = await _paymentProcessor.ProcessPayment(
                commandData, amount,
                Settings.EquipmentSettings.PlayerGilSettings.AllowModOverride, chatClient);

            if (!gilTransaction.Paid)
            {
                await chatClient.SendMessage(commandData.Channel,
                    $"You don't have {amount} gil, {commandData.User.Username}.");
                return;
            }

            _gilAccessor.RemoveGil(gilToRemove);
            string message = $"Removed {gilToRemove} gil from player.";
            await chatClient.SendMessage(commandData.Channel, message);
            await _statusHubEmitter.ShowEvent(message);
        }
    }
}
