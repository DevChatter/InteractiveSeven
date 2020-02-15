using InteractiveSeven.Core;
using InteractiveSeven.Core.Data;
using InteractiveSeven.Core.Data.Items;
using InteractiveSeven.Core.Diagnostics.Memory;
using InteractiveSeven.Core.Emitters;
using InteractiveSeven.Core.Payments;
using InteractiveSeven.Core.Settings;
using InteractiveSeven.Twitch.Model;
using InteractiveSeven.Twitch.Payments;
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
        private readonly IStatusHubEmitter _statusHubEmitter;
        private readonly GameDatabase _gameDatabase;
        private readonly GilBank _gilBank;
        private readonly ITwitchClient _twitchClient;
        private readonly EquipmentData<T> _equipmentData;
        private readonly PaymentProcessor _paymentProcessor;

        protected EquipmentCommand(IEquipmentAccessor equipmentAccessor,
            IInventoryAccessor inventoryAccessor, IMateriaAccessor materiaAccessor,
            IStatusHubEmitter statusHubEmitter, GameDatabase gameDatabase,
            GilBank gilBank, ITwitchClient twitchClient, EquipmentData<T> equipmentData,
            Func<CommandSettings, string[]> commandWordsSelector, PaymentProcessor paymentProcessor)
            : base(commandWordsSelector, x => x.EquipmentSettings.Enabled)
        {
            _equipmentAccessor = equipmentAccessor;
            _inventoryAccessor = inventoryAccessor;
            _materiaAccessor = materiaAccessor;
            _statusHubEmitter = statusHubEmitter;
            _gameDatabase = gameDatabase;
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

            var candidates = Settings.EquipmentSettings.AllByName(equipmentArg, charName, typeof(T));

            if (candidates.Count == 0)
            {
                _twitchClient.SendMessage(commandData.Channel, "Error: No matching Equipment.");
                return;
            }

            if (candidates.Count() > 1)
            {
                string matches = string.Join(", ", candidates.Select(x => x.Name.NoSpaces()));
                _twitchClient.SendMessage(commandData.Channel, $"Error: Matches ({matches})");
                return;
            }

            var equippableSettings = candidates.Single();

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
            RemoveMateria(charName, equippableSettings.Item.EquipmentId);
            string message = $"Equipped {charName.DefaultName} with {equippableSettings.Name}.";
            _twitchClient.SendMessage(commandData.Channel, message);
            _statusHubEmitter.ShowEvent(message, commandData.User.Username);
        }

        private void RemoveMateria(CharNames charName, int equipmentId)
        {
            int keep = 0;
            if (typeof(T) == typeof(Weapon))
            {
                var weaponData = _gameDatabase.WeaponDatabase?.SingleOrDefault(x => x.Id == equipmentId);
                if (weaponData != null)
                {
                    keep = weaponData.LinkedSlots + weaponData.SingleSlots;
                }
                _materiaAccessor.RemoveWeaponMateria(charName, keep);
            }
            else if (typeof(T) == typeof(Armlet))
            {
                var armletData = _gameDatabase.ArmletDatabase?.SingleOrDefault(x => x.Id == equipmentId);
                if (armletData != null)
                {
                    keep = armletData.LinkedSlots + armletData.SingleSlots;
                }
                _materiaAccessor.RemoveArmletMateria(charName, keep);
            }
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