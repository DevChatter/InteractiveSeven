using System.Linq;
using System.Threading.Tasks;
using InteractiveSeven.Core.Chat;
using InteractiveSeven.Core.Diagnostics.Memory;
using InteractiveSeven.Core.Emitters;
using InteractiveSeven.Core.FinalFantasy.Constants;
using InteractiveSeven.Core.Payments;
using InteractiveSeven.Core.Settings;

namespace InteractiveSeven.Core.Commands.Equipment
{
    public class GivePlayerGpCommand : BaseCommand
    {
        private readonly PaymentProcessor _paymentProcessor;
        private readonly IGpAccessor _gpAccessor;
        private readonly IStatusHubEmitter _statusHubEmitter;
        private PlayerGpSettings GpSettings => Settings.EquipmentSettings.PlayerGpSettings;

        public GivePlayerGpCommand(PaymentProcessor paymentProcessor,
            IGpAccessor gpAccessor, IStatusHubEmitter statusHubEmitter)
            : base(x => x.GivePlayerGpCommandWords,
                x => x.EquipmentSettings.PlayerGpSettings.GiveGpEnabled)
        {
            _paymentProcessor = paymentProcessor;
            _gpAccessor = gpAccessor;
            _statusHubEmitter = statusHubEmitter;
        }

        public override async Task Execute(CommandData commandData, IChatClient chatClient)
        {
            ushort amount = commandData.Arguments.FirstOrDefault().SafeUshortParse();

            if (amount <= 0)
            {
                await chatClient.SendMessage(commandData.Channel,
                    $"How much gp do you want to give to the player, {commandData.User.Username}?");
                return;
            }

            ushort currentGp = _gpAccessor.GetGp();
            if (FF7Const.MaxGp - currentGp > amount)
            {
                await chatClient.SendMessage(commandData.Channel,
                    $"Max GP is {FF7Const.MaxGp:N0}. Current GP: {currentGp:N0}.");
                return;
            }

            int gilCost = amount * GpSettings.GiveMultiplier;
            GilTransaction gilTransaction = await _paymentProcessor.ProcessPayment(
                commandData, gilCost, GpSettings.AllowModOverride, chatClient);

            if (!gilTransaction.Paid)
            {
                await chatClient.SendMessage(commandData.Channel,
                    $"You don't have {gilCost} gil, {commandData.User.Username}.");
                return;
            }

            _gpAccessor.AddGp(amount);
            string message = $"Added {amount} GP for player.";
            await chatClient.SendMessage(commandData.Channel, message);
            await _statusHubEmitter.ShowEvent(message);
        }
    }
}
