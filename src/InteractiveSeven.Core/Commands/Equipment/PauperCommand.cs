using System.Linq;
using System.Threading.Tasks;
using InteractiveSeven.Core.Chat;
using InteractiveSeven.Core.Data;
using InteractiveSeven.Core.Data.Items;
using InteractiveSeven.Core.Diagnostics.Memory;
using InteractiveSeven.Core.Emitters;
using InteractiveSeven.Core.Payments;
using InteractiveSeven.Core.ViewModels;

namespace InteractiveSeven.Core.Commands.Equipment
{
    public class PauperCommand : BaseCommand
    {
        private readonly IEquipmentAccessor _equipmentAccessor;
        private readonly IMateriaAccessor _materiaAccessor;
        private readonly IInventoryAccessor _inventoryAccessor;
        private readonly IGilAccessor _gilAccessor;
        private readonly IChatClient _chatClient;
        private readonly EquipmentData<Weapon> _weaponData;
        private readonly EquipmentData<Armlet> _armletData;
        private readonly PaymentProcessor _paymentProcessor;
        private readonly IStatusHubEmitter _statusHubEmitter;
        private readonly PartyStatusViewModel _partyStatusViewModel;

        public PauperCommand(IEquipmentAccessor equipmentAccessor,
            IMateriaAccessor materiaAccessor,
            IInventoryAccessor inventoryAccessor,
            IGilAccessor gilAccessor,
            IChatClient chatClient,
            EquipmentData<Weapon> weaponData,
            EquipmentData<Armlet> armletData,
            PaymentProcessor paymentProcessor,
            IStatusHubEmitter statusHubEmitter,
            PartyStatusViewModel partyStatusViewModel)
            : base(x => x.PauperCommandWords, x => x.EquipmentSettings.EnablePauperCommand)
        {
            _equipmentAccessor = equipmentAccessor;
            _materiaAccessor = materiaAccessor;
            _inventoryAccessor = inventoryAccessor;
            _gilAccessor = gilAccessor;
            _chatClient = chatClient;
            _weaponData = weaponData;
            _armletData = armletData;
            _paymentProcessor = paymentProcessor;
            _statusHubEmitter = statusHubEmitter;
            _partyStatusViewModel = partyStatusViewModel;
        }

        public override async Task Execute(CommandData commandData)
        {
            if (_partyStatusViewModel.Party.Any(x => x?.Id == CharNames.Sephiroth.Id))
            {
                await _chatClient.SendMessage(commandData.Channel,
                    "Cannot Change Equipment while Sephiroth is in the Party.");
                return;
            }

            GilTransaction gilTransaction = await _paymentProcessor.ProcessPayment(
                commandData, Settings.EquipmentSettings.PauperCommandCost,
                Settings.EquipmentSettings.AllowModOverride);

            if (!gilTransaction.Paid)
            {
                return;
            }

            foreach (var charName in CharNames.Core)
            {
                _materiaAccessor.RemoveWeaponMateria(charName);
                _materiaAccessor.RemoveArmletMateria(charName);
                var startWeapon = _weaponData.GetById(0, charName);
                _equipmentAccessor.SetCharacterEquipment(charName, startWeapon.EquipmentId, x => x.Weapon.Address);
                _equipmentAccessor.SetCharacterEquipment(charName, _armletData.GetById(0).EquipmentId, x => x.Armlet.Address);
                _equipmentAccessor.SetCharacterEquipment(charName, byte.MaxValue, x => x.Accessory.Address);
            }

            _materiaAccessor.RemoveAllMateria();

            _inventoryAccessor.RemoveAllItems();

            _gilAccessor.SetGil(2);

            await _chatClient.SendMessage(commandData.Channel,
                "All Weapons and Armor set to Default. " +
                "All Items, Accessories, Materia, and Gil have been removed. " +
                "Good luck.");
            await _statusHubEmitter.ShowEvent("You've been Paupered!", $"by {commandData.User.Username}",
                "ff7-gameover.mp3");
        }
    }
}
