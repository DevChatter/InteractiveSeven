using InteractiveSeven.Core.Data.Items;
using InteractiveSeven.Core.Memory;
using InteractiveSeven.Core.Models;
using TwitchLib.Client.Interfaces;

namespace InteractiveSeven.Twitch.Commands
{
    public class AccessoryCommand : EquipmentCommand<Accessories>
    {
        public AccessoryCommand(IEquipmentAccessor equipmentAccessor,
            IInventoryAccessor inventoryAccessor, IMateriaAccessor materiaAccessor,
            GilBank gilBank, ITwitchClient twitchClient, EquipmentData<Accessories> equipmentData)
            : base(equipmentAccessor, inventoryAccessor, materiaAccessor,
                gilBank, twitchClient, equipmentData, x => x.AccessoryCommandWords)
        {
        }
    }
}