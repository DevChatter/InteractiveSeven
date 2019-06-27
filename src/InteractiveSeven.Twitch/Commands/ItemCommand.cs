using InteractiveSeven.Core.Data.Items;
using InteractiveSeven.Core.Memory;
using InteractiveSeven.Core.Model;
using InteractiveSeven.Twitch.Model;
using System.Linq;
using TwitchLib.Client.Interfaces;

namespace InteractiveSeven.Twitch.Commands
{
    public class ItemCommand : BaseCommand
    {
        private readonly ITwitchClient _twitchClient;
        private readonly IInventoryAccessor _inventoryAccessor;

        public ItemCommand(ITwitchClient twitchClient, IInventoryAccessor inventoryAccessor)
            : base(x => new[] { "item" }, x => x.ItemSettings.Enabled)
        {
            _twitchClient = twitchClient;
            _inventoryAccessor = inventoryAccessor;
        }

        public override void Execute(CommandData commandData)
        {
            if (!IsAllowedToUseCommand(commandData.User)) return;

            string itemIdText = commandData.Arguments.FirstOrDefault();
            if (itemIdText != null && ushort.TryParse(itemIdText, out ushort itemId) && itemId < 319)
            {
                _inventoryAccessor.AddItem(itemId, 1, true);
                Items item = Items.All.SingleOrDefault(x => x.Id == itemId);
                string itemName = item == null ? "Unknown Item" : item.Name;
                _twitchClient.SendMessage(commandData.Channel,
                    $"Item {itemName} Added");
            }
        }

        private bool IsAllowedToUseCommand(ChatUser user)
            => (Settings.ItemSettings.AllowMod && user.IsMod)
               || user.IsMe || user.IsBroadcaster;

    }
}