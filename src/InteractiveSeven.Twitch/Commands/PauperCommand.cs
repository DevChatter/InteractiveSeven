using System.Linq;
using InteractiveSeven.Core.Data;
using InteractiveSeven.Core.Data.Items;
using InteractiveSeven.Core.Memory;
using InteractiveSeven.Twitch.Model;
using TwitchLib.Client.Interfaces;

namespace InteractiveSeven.Twitch.Commands
{
    public class PauperCommand : BaseCommand
    {
        private readonly IEquipmentAccessor _equipmentAccessor;
        private readonly IMateriaAccessor _materiaAccessor;
        private readonly IInventoryAccessor _inventoryAccessor;
        private readonly IGilAccessor _gilAccessor;
        private readonly ITwitchClient _twitchClient;

        public PauperCommand(IEquipmentAccessor equipmentAccessor,
            IMateriaAccessor materiaAccessor,
            IInventoryAccessor inventoryAccessor,
            IGilAccessor gilAccessor,
            ITwitchClient twitchClient)
            : base(x => new []{ "pauper" }, x => true)
        {
            _equipmentAccessor = equipmentAccessor;
            _materiaAccessor = materiaAccessor;
            _inventoryAccessor = inventoryAccessor;
            _gilAccessor = gilAccessor;
            _twitchClient = twitchClient;
        }

        public override void Execute(CommandData commandData)
        {
            if (!commandData.User.IsMe && !commandData.User.IsBroadcaster) return;

            foreach (var charName in CharNames.All)
            {
                _materiaAccessor.RemoveWeaponMateria(charName);
                _materiaAccessor.RemoveArmletMateria(charName);
                var charWeaponSet = Weapons.AllWeapons[charName.Id];
                _equipmentAccessor.SetCharacterWeapon(charName, charWeaponSet.Min(x => x.Value));
                _equipmentAccessor.SetCharacterArmlet(charName, Armlets.All.Min(x => x.Value));
                _equipmentAccessor.SetCharacterAccessory(charName, byte.MaxValue);
            }

            _materiaAccessor.RemoveAllMateria();

            _inventoryAccessor.RemoveAllItems();

            _gilAccessor.SetGil(2);

            _twitchClient.SendMessage(commandData.Channel,
                "All Weapons and Armor set to Default. All Items, Accessories, Materia, and Gil have been removed. Good luck.");
        }
    }
}