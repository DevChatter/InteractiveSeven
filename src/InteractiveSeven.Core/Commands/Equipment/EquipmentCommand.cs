using System;
using System.Linq;
using System.Threading.Tasks;
using InteractiveSeven.Core.Chat;
using InteractiveSeven.Core.Data;
using InteractiveSeven.Core.Data.Items;
using InteractiveSeven.Core.Diagnostics.Memory;
using InteractiveSeven.Core.Emitters;
using InteractiveSeven.Core.Payments;
using InteractiveSeven.Core.Settings;
using InteractiveSeven.Core.ViewModels;

namespace InteractiveSeven.Core.Commands.Equipment
{
    public class EquipmentCommand<T> : BaseCommand where T : Data.Items.Equipment
    {
        private readonly IEquipmentAccessor _equipmentAccessor;
        private readonly IInventoryAccessor _inventoryAccessor;
        private readonly IMateriaAccessor _materiaAccessor;
        private readonly IStatusHubEmitter _statusHubEmitter;
        private readonly PartyStatusViewModel _partyStatusViewModel;
        private readonly GameDatabase _gameDatabase;
        private readonly GilBank _gilBank;
        private readonly EquipmentData<T> _equipmentData;
        private readonly PaymentProcessor _paymentProcessor;

        protected EquipmentCommand(IEquipmentAccessor equipmentAccessor,
            IInventoryAccessor inventoryAccessor,
            IMateriaAccessor materiaAccessor,
            IStatusHubEmitter statusHubEmitter,
            PartyStatusViewModel partyStatusViewModel,
            GameDatabase gameDatabase,
            GilBank gilBank,
            EquipmentData<T> equipmentData,
            Func<CommandSettings, string[]> commandWordsSelector,
            PaymentProcessor paymentProcessor)
            : base(commandWordsSelector, x => x.EquipmentSettings.Enabled)
        {
            _equipmentAccessor = equipmentAccessor;
            _inventoryAccessor = inventoryAccessor;
            _materiaAccessor = materiaAccessor;
            _statusHubEmitter = statusHubEmitter;
            _partyStatusViewModel = partyStatusViewModel;
            _gameDatabase = gameDatabase;
            _gilBank = gilBank;
            _equipmentData = equipmentData;
            _paymentProcessor = paymentProcessor;
        }

        public override async Task Execute(CommandData commandData, IChatClient chatClient)
        {
            (bool isValidName, CharNames charName) =
                CharNames.GetByName(commandData.Arguments.FirstOrDefault());

            if (isValidName && TryingToChangeSephirothOrYoungCloud(charName))
            {
                await chatClient.SendMessage(commandData.Channel,
                    "Cannot Change Equipment of Sephiroth or Young Cloud.");
                return;
            }

            if (!isValidName || commandData.Arguments.Count < 2)
            {
                await chatClient.SendMessage(commandData.Channel,
                    "Invalid Request - Specify equipment and character like this: !weapon cloud buster");
                return;
            }

            string equipmentArg =
                commandData.Arguments.Count == 2
                    ? commandData.Arguments.ElementAtOrDefault(1)
                    : string.Join(' ', commandData.Arguments.Skip(1));


            var candidates = Settings.EquipmentSettings.AllByName(equipmentArg, charName, typeof(T));

            if (candidates.Count == 0)
            {
                await chatClient.SendMessage(commandData.Channel, "Error: No matching Equipment.");
                return;
            }

            if (candidates.Count() > 1)
            {
                string matches = string.Join(", ", candidates.Select(x => x.Name.NoSpaces()));
                await chatClient.SendMessage(commandData.Channel, $"Error: Matches ({matches})");
                return;
            }

            var equippableSettings = candidates.Single();

            GilTransaction gilTransaction = await _paymentProcessor.ProcessPayment(
                commandData, equippableSettings.Cost, Settings.EquipmentSettings.AllowModOverride, chatClient);

            if (!gilTransaction.Paid)
            {
                return;
            }

            ushort existingEquipmentId = _equipmentAccessor.GetCharacterEquipment(charName, AddressSelector());
            if (equippableSettings.Item.EquipmentId == existingEquipmentId)
            {
                await chatClient.SendMessage(commandData.Channel,
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
            await chatClient.SendMessage(commandData.Channel, message);
            await _statusHubEmitter.ShowEvent(message, commandData.User.Username);
        }

        private bool TryingToChangeSephirothOrYoungCloud(CharNames charName)
        {
            byte[] ids =
            {
                CharNames.Vincent.Id,
                CharNames.CaitSith.Id,
                CharNames.Sephiroth.Id,
                CharNames.YoungCloud.Id
            };
            return ids.Contains(charName.Id)
                   && _partyStatusViewModel.Party.Any(x => x?.Id == CharNames.Sephiroth.Id || x?.Id == CharNames.YoungCloud.Id);
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
