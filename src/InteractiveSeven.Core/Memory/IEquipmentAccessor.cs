using InteractiveSeven.Core.Data;
using System;

namespace InteractiveSeven.Core.Memory
{
    public interface IEquipmentAccessor
    {
        byte GetCharacterEquipment(CharNames charName, Func<CharMemLoc, IntPtr> addressSelector);

        void SetCharacterEquipment(CharNames charName, byte equipmentEquipmentId,
            Func<CharMemLoc, IntPtr> addressSelector);
    }
}