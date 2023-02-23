using System.Linq;
using System.Threading.Tasks;
using InteractiveSeven.Core.Chat;
using InteractiveSeven.Core.Diagnostics.Memory;
using InteractiveSeven.Core.Emitters;
using InteractiveSeven.Core.Payments;
using InteractiveSeven.Core.Settings;

namespace InteractiveSeven.Core.Commands.Equipment
{
    public class RemovePlayerGpCommand : BaseCommand
    {
        private readonly PaymentProcessor _paymentProcessor;
        private readonly IChatClient _chatClient;
        private readonly IGpAccessor _gpAccessor;
        private readonly IStatusHubEmitter _statusHubEmitter;
        private PlayerGpSettings GpSettings => Settings.EquipmentSettings.PlayerGpSettings;

        public RemovePlayerGpCommand(PaymentProcessor paymentProcessor, IChatClient chatClient,
            IGpAccessor gpAccessor, IStatusHubEmitter statusHubEmitter)
            : base(x => x.RemovePlayerGpCommandWords,
                x => x.EquipmentSettings.PlayerGpSettings.RemoveGpEnabled)
        {
            _paymentProcessor = paymentProcessor;
            _chatClient = chatClient;
            _gpAccessor = gpAccessor;
            _statusHubEmitter = statusHubEmitter;
        }

        public override async Task Execute(CommandData commandData)
        {
            ushort amount = commandData.Arguments.FirstOrDefault().SafeUshortParse();

            if (amount <= 0)
            {
                await _chatClient.SendMessage(commandData.Channel,
                    $"How much gp do you want to take from the player, {commandData.User.Username}?");
                return;
            }

            ushort currentGp = _gpAccessor.GetGp();
            if (amount > currentGp)
            {
                // TODO: Adjust their request to remove all gp.
                await _chatClient.SendMessage(commandData.Channel,
                    $"Player has {currentGp:N0} GP. Can't remove {amount:N0} GP.");
                return;
            }

            int gilCost = amount * GpSettings.RemoveMultiplier;
            GilTransaction gilTransaction = await _paymentProcessor.ProcessPayment(
                commandData, gilCost, GpSettings.AllowModOverride);

            if (!gilTransaction.Paid)
            {
                await _chatClient.SendMessage(commandData.Channel,
                    $"You don't have {gilCost} gil, {commandData.User.Username}.");
                return;
            }

            _gpAccessor.RemoveGp(amount);
            string message = $"Removed {amount} gp from player.";
            await _chatClient.SendMessage(commandData.Channel, message);
            await _statusHubEmitter.ShowEvent(message);
        }
    }
}
