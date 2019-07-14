using InteractiveSeven.Core.Data;
using InteractiveSeven.Core.Data.Items;
using InteractiveSeven.Core.Memory;
using InteractiveSeven.Core.Model;
using InteractiveSeven.Core.Payments;
using InteractiveSeven.Core.Settings;
using InteractiveSeven.Twitch.Model;
using System;
using System.Linq;
using InteractiveSeven.Twitch.Payments;
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
        private readonly PaymentProcessor _paymentProcessor;

        protected EquipmentCommand(IEquipmentAccessor equipmentAccessor,
            IInventoryAccessor inventoryAccessor, IMateriaAccessor materiaAccessor,
            GilBank gilBank, ITwitchClient twitchClient, EquipmentData<T> equipmentData,
            Func<CommandSettings, string[]> commandWordsSelector, PaymentProcessor paymentProcessor)
            : base(commandWordsSelector, x => x.EquipmentSettings.Enabled)
        {
            _equipmentAccessor = equipmentAccessor;
            _inventoryAccessor = inventoryAccessor;
            _materiaAccessor = materiaAccessor;
            _gilBank = gilBank;
            _twitchClient = twitchClient;
            _equipmentData = equipmentData;
            _paymentProcessor = paymentProcessor;
        }

        public override void Execute(in CommandData commandData)
        {
            (bool isValidName, CharNames charName) =
                CharNames.GetByName(commandData.Arguments.FirstOrDefault());
            var equipmentArg = commandData.Arguments.ElementAtOrDefault(1);
            if (!isValidName)
            {
                _twitchClient.SendMessage(commandData.Channel,
                    "Invalid Request - Specify equipment and character like this: !weapon cloud 15");
                return;
            }

            var equippableSettings = Settings.EquipmentSettings.GetByValue(equipmentArg, charName, typeof(T));

            if (equippableSettings == null)
            {
                _twitchClient.SendMessage(commandData.Channel,
                    "Invalid Request - Specify equipment and character like this: !weapon cloud 15");
                return;
            }


            GilTransaction gilTransaction = _paymentProcessor.ProcessPayment(
                commandData, equippableSettings.Cost, Settings.EquipmentSettings.AllowModOverride);

            if (!gilTransaction.Paid)
            {
                return;
            }

            ushort existingEquipmentId = _equipmentAccessor.GetCharacterEquipment(charName, AddressSelector());
            if (equippableSettings.Item.EquipmentId == existingEquipmentId)
            {
                _twitchClient.SendMessage(commandData.Channel,
                    $"Sorry, {charName.DefaultName} already has {equippableSettings.Name} equipped.");
                if (gilTransaction.AmountPaid > 0) // return the gil, since we did nothing
                {
                    _gilBank.Deposit(commandData.User, gilTransaction.AmountPaid);
                }
                return;
            }

            _equipmentAccessor.SetCharacterEquipment(charName, equippableSettings.Item.EquipmentId, AddressSelector());
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
                $"Equipped {charName.DefaultName} with a {equippableSettings.Name}.");
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
    }
}