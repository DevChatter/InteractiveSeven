using System.Linq;
using InteractiveSeven.Core.Data.Items;
using InteractiveSeven.Core.Memory;
using InteractiveSeven.Twitch.Model;
using TwitchLib.Client.Interfaces;

namespace InteractiveSeven.Twitch.Commands
{
    public class ItemCommand : BaseCommand
    {
        private readonly ITwitchClient _twitchClient;
        private readonly IInventoryAccessor _inventoryAccessor;

        public ItemCommand(ITwitchClient twitchClient, IInventoryAccessor inventoryAccessor)
            : base(x => new[] {"item"}, x => true)
        {
            _twitchClient = twitchClient;
            _inventoryAccessor = inventoryAccessor;
        }

        public override void Execute(CommandData commandData)
        {
            string itemIdText = commandData.Arguments.FirstOrDefault();
            if (itemIdText != null && ushort.TryParse(itemIdText, out ushort itemId) && itemId < 319)
            {
                _inventoryAccessor.AddItem(itemId, 1, true);
                Items item = Items.All.SingleOrDefault(x => x.Value == itemId);
                string itemName = item == null ? "Unknown Item" : item.Name;
                _twitchClient.SendMessage(commandData.Channel,
                    $"Item {itemName} Added");
            }
        }
    }
}