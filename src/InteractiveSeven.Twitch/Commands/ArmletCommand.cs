using InteractiveSeven.Core.Data.Items;
using InteractiveSeven.Core.Memory;
using InteractiveSeven.Core.Models;
using TwitchLib.Client.Interfaces;

namespace InteractiveSeven.Twitch.Commands
{
    public class ArmletCommand : EquipmentCommand<Armlet>
    {
        public ArmletCommand(IEquipmentAccessor equipmentAccessor,
            IInventoryAccessor inventoryAccessor, IMateriaAccessor materiaAccessor,
            GilBank gilBank, ITwitchClient twitchClient, EquipmentData<Armlet> equipmentData)
            : base(equipmentAccessor, inventoryAccessor, materiaAccessor,
                gilBank, twitchClient, equipmentData, x => x.ArmletCommandWords)
        {
        }
    }
}