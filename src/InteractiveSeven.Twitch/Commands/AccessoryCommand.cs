using InteractiveSeven.Core.Data;
using InteractiveSeven.Core.Data.Items;
using InteractiveSeven.Core.Memory;
using InteractiveSeven.Core.Model;
using InteractiveSeven.Core.Models;
using InteractiveSeven.Twitch.Model;
using System.Linq;
using TwitchLib.Client.Interfaces;

namespace InteractiveSeven.Twitch.Commands
{
    public class AccessoryCommand : BaseCommand
    {
        private readonly IEquipmentAccessor _equipmentAccessor;
        private readonly IInventoryAccessor _inventoryAccessor;
        private readonly GilBank _gilBank;
        private readonly ITwitchClient _twitchClient;

        public AccessoryCommand(IEquipmentAccessor equipmentAccessor, IInventoryAccessor inventoryAccessor,
            GilBank gilBank, ITwitchClient twitchClient)
            : base(x => x.AccessoryCommandWords, x => x.EquipmentSettings.Enabled)
        {
            _equipmentAccessor = equipmentAccessor;
            _inventoryAccessor = inventoryAccessor;
            _gilBank = gilBank;
            _twitchClient = twitchClient;
        }

        public override void Execute(CommandData commandData)
        {
            (bool isValidName, CharNames charName) =
                CharNames.GetByName(commandData.Arguments.FirstOrDefault());
            var accessoryText = commandData.Arguments.ElementAtOrDefault(1);

            if (!isValidName
                || !int.TryParse(accessoryText ?? "", out int accessoryId)
                || !Accessories.IsValid(accessoryId))
            {
                _twitchClient.SendMessage(commandData.Channel,
                    "Invalid Request - Specify character and accessory number like this !accessory cloud 15");
                return;
            }

            (int balance, int withdrawn) = (0, 0);
            if (!CanOverrideBitRestriction(commandData.User))
            {
                const int cost = 100; // TODO: Configurable Costs
                (balance, withdrawn) = _gilBank.Withdraw(commandData.User, cost, true);
                if (withdrawn < cost)
                {
                    _twitchClient.SendMessage(commandData.Channel,
                        $"Insufficient gil. You only have {balance} gil and needed {cost}");
                    return;
                }
            }

            Accessories accessory = Accessories.GetAccessory(accessoryId);
            int existingAccessoryId = _equipmentAccessor.GetCharacterAccessory(charName);
            if (accessory.Value == existingAccessoryId)
            {
                _twitchClient.SendMessage(commandData.Channel,
                    $"Sorry, {charName.DefaultName} already has {accessory.Name} equipped.");
                if (withdrawn > 0) // return the gil, since we did nothing
                {
                    _gilBank.Deposit(commandData.User, withdrawn);
                }
                return;
            }

            _equipmentAccessor.SetCharacterAccessory(charName, (byte)accessory.Value);
            Accessories removedAccessory = Accessories.GetAccessory(existingAccessoryId);
            if (removedAccessory != null && Settings.EquipmentSettings.KeepPreviousEquipment)
            {
                _inventoryAccessor.AddItem(removedAccessory.ItemId, 1, true);
            }
            _twitchClient.SendMessage(commandData.Channel,
                $"Equipped {charName.DefaultName} with a {accessory.Name}.");
        }

        private bool CanOverrideBitRestriction(ChatUser user)
            => (Settings.EquipmentSettings.AllowModOverride && user.IsMod)
               || user.IsMe || user.IsBroadcaster;
    }
}