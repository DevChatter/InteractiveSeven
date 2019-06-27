using InteractiveSeven.Core.Data;
using InteractiveSeven.Core.Data.Items;
using InteractiveSeven.Core.Memory;
using InteractiveSeven.Core.Model;
using InteractiveSeven.Core.Models;
using InteractiveSeven.Core.Settings;
using InteractiveSeven.Twitch.Model;
using System;
using System.Linq;
using TwitchLib.Client.Interfaces;

namespace InteractiveSeven.Twitch.Commands
{
    public class EquipmentCommand<T> : BaseCommand where T : Equipment
    {
        private readonly IEquipmentAccessor _equipmentAccessor;
        private readonly IInventoryAccessor _inventoryAccessor;
        private readonly IMateriaAccessor _materiaAccessor;
        private readonly GilBank _gilBank;
        private readonly ITwitchClient _twitchClient;
        private readonly EquipmentData<T> _equipmentData;

        protected EquipmentCommand(IEquipmentAccessor equipmentAccessor,
            IInventoryAccessor inventoryAccessor, IMateriaAccessor materiaAccessor,
            GilBank gilBank, ITwitchClient twitchClient, EquipmentData<T> equipmentData,
            Func<CommandSettings, string[]> commandWordsSelector)
            : base(commandWordsSelector, x => x.EquipmentSettings.Enabled)
        {
            _equipmentAccessor = equipmentAccessor;
            _inventoryAccessor = inventoryAccessor;
            _materiaAccessor = materiaAccessor;
            _gilBank = gilBank;
            _twitchClient = twitchClient;
            _equipmentData = equipmentData;
        }

        public override void Execute(CommandData commandData)
        {
            (bool isValidName, CharNames charName) =
                CharNames.GetByName(commandData.Arguments.FirstOrDefault());
            var equipmentArg = commandData.Arguments.ElementAtOrDefault(1);

            if (!isValidName
                || !ushort.TryParse(equipmentArg ?? "", out ushort id)
                || _equipmentData.GetById(id, charName) == null)
            {
                _twitchClient.SendMessage(commandData.Channel,
                    "Invalid Request - Specify equipment and character like this: !weapon cloud 15");
                return;
            }

            int withdrawn = 0;
            if (!CanOverrideBitRestriction(commandData.User))
            {
                const int cost = 100; // TODO: Configurable Costs
                int balance;
                (balance, withdrawn) = _gilBank.Withdraw(commandData.User, cost, true);
                if (withdrawn < cost)
                {
                    _twitchClient.SendMessage(commandData.Channel,
                        $"Insufficient gil. You only have {balance} gil and needed {cost}");
                    return;
                }
            }

            var equipment = _equipmentData.GetById(id, charName);
            ushort existingEquipmentId = _equipmentAccessor.GetCharacterEquipment(charName, AddressSelector());
            if (equipment.EquipmentId == existingEquipmentId)
            {
                _twitchClient.SendMessage(commandData.Channel,
                    $"Sorry, {charName.DefaultName} already has {equipment.Name} equipped.");
                if (withdrawn > 0) // return the gil, since we did nothing
                {
                    _gilBank.Deposit(commandData.User, withdrawn);
                }
                return;
            }

            _equipmentAccessor.SetCharacterEquipment(charName, equipment.EquipmentId, AddressSelector());
            if (Settings.EquipmentSettings.KeepPreviousEquipment)
            {
                var removedEquip = _equipmentData.GetByEquipId(existingEquipmentId, charName);
                if (removedEquip != null)
                {
                    _inventoryAccessor.AddItem(removedEquip.ItemId, 1, true);
                }
            }
            _materiaAccessor.RemoveWeaponMateria(charName);
            _twitchClient.SendMessage(commandData.Channel,
                $"Equipped {charName.DefaultName} with a {equipment.Name}.");
        }

        private static Func<CharMemLoc, IntPtr> AddressSelector()
        {
            if (typeof(T) == typeof(Weapon))
            {
                return x => x.Weapon.Address;
            }
            if (typeof(T) == typeof(Accessory))
            {
                return x => x.Accessory.Address;
            }
            if (typeof(T) == typeof(Armlet))
            {
                return x => x.Armlet.Address;
            }
            throw new NotImplementedException();
        }

        private bool CanOverrideBitRestriction(ChatUser user)
            => (Settings.EquipmentSettings.AllowModOverride && user.IsMod)
               || user.IsMe || user.IsBroadcaster;
    }
}