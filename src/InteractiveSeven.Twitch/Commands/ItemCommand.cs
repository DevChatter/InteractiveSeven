using InteractiveSeven.Core.Data.Items;
using InteractiveSeven.Core.Diagnostics.Memory;
using InteractiveSeven.Core.Emitters;
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
        private readonly IStatusHubEmitter _statusHubEmitter;

        public ItemCommand(ITwitchClient twitchClient, IInventoryAccessor inventoryAccessor,
            IStatusHubEmitter statusHubEmitter)
            : base(x => x.ItemCommandWords, x => x.ItemSettings.Enabled)
        {
            _twitchClient = twitchClient;
            _inventoryAccessor = inventoryAccessor;
            _statusHubEmitter = statusHubEmitter;
        }

        public override void Execute(in CommandData commandData)
        {
            if (!IsAllowedToUseCommand(commandData.User)) return;

            string itemIdText = commandData.Arguments.FirstOrDefault();
            if (itemIdText != null && ushort.TryParse(itemIdText, out ushort itemId) && itemId < 319)
            {
                Items item = Items.All.SingleOrDefault(x => x.ItemId == itemId); // TODO: Make this lookup based on settings, not on the item.

                if (item == null)
                {
                    return;
                }

                _inventoryAccessor.AddItem(item.ItemId, 1, true);
                string message = $"Item {item.Name} Added";
                _twitchClient.SendMessage(commandData.Channel, message);
                _statusHubEmitter.ShowEvent(message);
            }
        }

        private bool IsAllowedToUseCommand(in ChatUser user)
            => (Settings.ItemSettings.AllowMod && user.IsMod)
               || user.IsMe || user.IsBroadcaster;

    }
}