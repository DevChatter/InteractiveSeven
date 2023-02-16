using System.Linq;
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
        private readonly IChatClient _chatClient;
        private readonly IGpAccessor _gpAccessor;
        private readonly IStatusHubEmitter _statusHubEmitter;
        private PlayerGpSettings GpSettings => Settings.EquipmentSettings.PlayerGpSettings;

        public GivePlayerGpCommand(PaymentProcessor paymentProcessor, IChatClient chatClient,
            IGpAccessor gpAccessor, IStatusHubEmitter statusHubEmitter)
            : base(x => x.GivePlayerGpCommandWords,
                x => x.EquipmentSettings.PlayerGpSettings.GiveGpEnabled)
        {
            _paymentProcessor = paymentProcessor;
            _chatClient = chatClient;
            _gpAccessor = gpAccessor;
            _statusHubEmitter = statusHubEmitter;
        }

        public override void Execute(in CommandData commandData)
        {
            ushort amount = commandData.Arguments.FirstOrDefault().SafeUshortParse();

            if (amount <= 0)
            {
                _chatClient.SendMessage(commandData.Channel,
                    $"How much gp do you want to give to the player, {commandData.User.Username}?");
                return;
            }

            ushort currentGp = _gpAccessor.GetGp();
            if (FF7Const.MaxGp - currentGp > amount)
            {
                _chatClient.SendMessage(commandData.Channel,
                    $"Max GP is {FF7Const.MaxGp:N0}. Current GP: {currentGp:N0}.");
                return;
            }

            int gilCost = amount * GpSettings.GiveMultiplier;
            GilTransaction gilTransaction = _paymentProcessor.ProcessPayment(
                commandData, gilCost, GpSettings.AllowModOverride);

            if (!gilTransaction.Paid)
            {
                _chatClient.SendMessage(commandData.Channel,
                    $"You don't have {gilCost} gil, {commandData.User.Username}.");
                return;
            }

            _gpAccessor.AddGp(amount);
            string message = $"Added {amount} GP for player.";
            _chatClient.SendMessage(commandData.Channel, message);
            _statusHubEmitter.ShowEvent(message);
        }
    }
}
