using System.Linq;
using InteractiveSeven.Core;
using InteractiveSeven.Core.Commands;
using InteractiveSeven.Core.Data.Items;
using InteractiveSeven.Core.Diagnostics.Memory;
using InteractiveSeven.Core.Emitters;
using InteractiveSeven.Core.Models;
using InteractiveSeven.Core.Payments;
using TwitchLib.Client.Interfaces;

namespace InteractiveSeven.Twitch.Commands
{
    public class ItemCommand : BaseCommand
    {
        private readonly ITwitchClient _twitchClient;
        private readonly IInventoryAccessor _inventoryAccessor;
        private readonly IStatusHubEmitter _statusHubEmitter;
        private readonly PaymentProcessor _paymentProcessor;

        public ItemCommand(ITwitchClient twitchClient, IInventoryAccessor inventoryAccessor,
            IStatusHubEmitter statusHubEmitter, PaymentProcessor paymentProcessor)
            : base(x => x.ItemCommandWords, x => x.ItemSettings.Enabled)
        {
            _twitchClient = twitchClient;
            _inventoryAccessor = inventoryAccessor;
            _statusHubEmitter = statusHubEmitter;
            _paymentProcessor = paymentProcessor;
        }

        public override void Execute(in CommandData commandData)
        {
            string itemName = commandData.Arguments.FirstOrDefault();

            var candidates = Settings.ItemSettings.AllByName(itemName);

            if (candidates.Count == 0)
            {
                _twitchClient.SendMessage(commandData.Channel, "Error: No matching Item.");
                return;
            }

            if (candidates.Count > 15)
            {
                _twitchClient.SendMessage(commandData.Channel, "Error: Too many matching items, be more specific.");
                return;
            }

            if (candidates.Count > 1)
            {
                string matches = string.Join(", ", candidates.Select(x => x.Name.NoSpaces()));
                _twitchClient.SendMessage(commandData.Channel, $"Error: matched ({matches})");
                return;
            }


            var itemSettings = candidates.Single();

            GilTransaction gilTransaction = _paymentProcessor.ProcessPayment(
                commandData, itemSettings.Cost, Settings.EquipmentSettings.AllowModOverride);

            if (!gilTransaction.Paid)
            {
                return;
            }

            Items item = itemSettings.Item;
            _inventoryAccessor.AddItem(item.ItemId, 1, true);
            string message = $"Item {item.Name} Added";
            _twitchClient.SendMessage(commandData.Channel, message);
            _statusHubEmitter.ShowEvent(message);
        }
    }
}
