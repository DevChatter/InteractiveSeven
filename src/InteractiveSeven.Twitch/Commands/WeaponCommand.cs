using InteractiveSeven.Core.Data.Items;
using InteractiveSeven.Core.Memory;
using InteractiveSeven.Core.Models;
using TwitchLib.Client.Interfaces;

namespace InteractiveSeven.Twitch.Commands
{
    public class WeaponCommand : EquipmentCommand<Weapons>
    {
        public WeaponCommand(IEquipmentAccessor equipmentAccessor,
            IInventoryAccessor inventoryAccessor, IMateriaAccessor materiaAccessor,
            GilBank gilBank, ITwitchClient twitchClient, EquipmentData<Weapons> equipmentData)
            : base(equipmentAccessor, inventoryAccessor, materiaAccessor,
                gilBank, twitchClient, equipmentData, x => x.WeaponCommandWords)
        {
        }
    }
}