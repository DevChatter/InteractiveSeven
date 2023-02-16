using System.Linq;
using InteractiveSeven.Core.Diagnostics.Memory;
using InteractiveSeven.Core.Emitters;
using InteractiveSeven.Core.FinalFantasy.Constants;
using InteractiveSeven.Core.Models;
using InteractiveSeven.Core.Payments;
using InteractiveSeven.Core.Settings;
using TwitchLib.Client.Interfaces;

namespace InteractiveSeven.Core.Commands.Equipment
{
    public class GivePlayerGpCommand : BaseCommand
    {
        private readonly PaymentProcessor _paymentProcessor;
        private readonly ITwitchClient _twitchClient;
        private readonly IGpAccessor _gpAccessor;
        private readonly IStatusHubEmitter _statusHubEmitter;
        private PlayerGpSettings GpSettings => Settings.EquipmentSettings.PlayerGpSettings;

        public GivePlayerGpCommand(PaymentProcessor paymentProcessor, ITwitchClient twitchClient,
            IGpAccessor gpAccessor, IStatusHubEmitter statusHubEmitter)
            : base(x => x.GivePlayerGpCommandWords,
                x => x.EquipmentSettings.PlayerGpSettings.GiveGpEnabled)
        {
            _paymentProcessor = paymentProcessor;
            _twitchClient = twitchClient;
            _gpAccessor = gpAccessor;
            _statusHubEmitter = statusHubEmitter;
        }

        public override void Execute(in CommandData commandData)
        {
            ushort amount = commandData.Arguments.FirstOrDefault().SafeUshortParse();

            if (amount <= 0)
            {
                _twitchClient.SendMessage(commandData.Channel,
                    $"How much gp do you want to give to the player, {commandData.User.Username}?");
                return;
            }

            ushort currentGp = _gpAccessor.GetGp();
            if (FF7Const.MaxGp - currentGp > amount)
            {
                _twitchClient.SendMessage(commandData.Channel,
                    $"Max GP is {FF7Const.MaxGp:N0}. Current GP: {currentGp:N0}.");
                return;
            }

            int gilCost = amount * GpSettings.GiveMultiplier;
            GilTransaction gilTransaction = _paymentProcessor.ProcessPayment(
                commandData, gilCost, GpSettings.AllowModOverride);

            if (!gilTransaction.Paid)
            {
                _twitchClient.SendMessage(commandData.Channel,
                    $"You don't have {gilCost} gil, {commandData.User.Username}.");
                return;
            }

            _gpAccessor.AddGp(amount);
            string message = $"Added {amount} GP for player.";
            _twitchClient.SendMessage(commandData.Channel, message);
            _statusHubEmitter.ShowEvent(message);
        }
    }
}
