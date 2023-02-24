using System.Linq;
using System.Threading.Tasks;
using InteractiveSeven.Core.Chat;
using InteractiveSeven.Core.Data.Items;
using InteractiveSeven.Core.Diagnostics.Memory;
using InteractiveSeven.Core.Emitters;
using InteractiveSeven.Core.Payments;

namespace InteractiveSeven.Core.Commands.Equipment
{
    public class ItemCommand : BaseCommand
    {
        private readonly IInventoryAccessor _inventoryAccessor;
        private readonly IStatusHubEmitter _statusHubEmitter;
        private readonly PaymentProcessor _paymentProcessor;

        public ItemCommand(IInventoryAccessor inventoryAccessor,
            IStatusHubEmitter statusHubEmitter, PaymentProcessor paymentProcessor)
            : base(x => x.ItemCommandWords, x => x.ItemSettings.Enabled)
        {
            _inventoryAccessor = inventoryAccessor;
            _statusHubEmitter = statusHubEmitter;
            _paymentProcessor = paymentProcessor;
        }

        public override async Task Execute(CommandData commandData, IChatClient chatClient)
        {
            string itemName = commandData.Arguments.Count == 1
                ? commandData.Arguments.FirstOrDefault()
                : string.Join(' ', commandData.Arguments);

            var candidates = Settings.ItemSettings.AllByName(itemName);

            if (candidates.Count == 0)
            {
                await chatClient.SendMessage(commandData.Channel, "Error: No matching Item.");
                return;
            }

            if (candidates.Count > 15)
            {
                await chatClient.SendMessage(commandData.Channel, "Error: Too many matching items, be more specific.");
                return;
            }

            if (candidates.Count > 1)
            {
                string matches = string.Join(", ", candidates.Select(x => x.Name.NoSpaces()));
                await chatClient.SendMessage(commandData.Channel, $"Error: matched ({matches})");
                return;
            }


            var itemSettings = candidates.Single();

            GilTransaction gilTransaction = await _paymentProcessor.ProcessPayment(
                commandData, itemSettings.Cost, Settings.EquipmentSettings.AllowModOverride, chatClient);

            if (!gilTransaction.Paid)
            {
                return;
            }

            Items item = itemSettings.Item;
            _inventoryAccessor.AddItem(item.ItemId, 1, true);
            string message = $"Item {item.Name} Added";
            await chatClient.SendMessage(commandData.Channel, message);
            await _statusHubEmitter.ShowEvent(message);
        }
    }
}
