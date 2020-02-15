using InteractiveSeven.Core;
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

            string itemName = commandData.Arguments.FirstOrDefault();

            var candidates = Items.All.Where(x => x.IsMatchByName(itemName)).ToList(); // TODO: Make this lookup based on settings, not on the item.

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

            Items item = candidates.Single();
            _inventoryAccessor.AddItem(item.ItemId, 1, true);
            string message = $"Item {item.Name} Added";
            _twitchClient.SendMessage(commandData.Channel, message);
            _statusHubEmitter.ShowEvent(message);
        }

        private bool IsAllowedToUseCommand(in ChatUser user)
                => (Settings.ItemSettings.AllowMod && user.IsMod)
                   || user.IsMe || user.IsBroadcaster;

    }
}