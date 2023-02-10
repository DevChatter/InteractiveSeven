using System;
using InteractiveSeven.Core.Data;
using InteractiveSeven.Core.Settings;

namespace InteractiveSeven.Core.Diagnostics.Memory
{
    public class EquipmentAccessor : IEquipmentAccessor
    {
        private readonly IMemoryAccessor _memoryAccessor;

        private ApplicationSettings Settings => ApplicationSettings.Instance;

        public EquipmentAccessor(IMemoryAccessor memoryAccessor)
        {
            _memoryAccessor = memoryAccessor;
        }

        public byte GetCharacterEquipment(CharNames charName, Func<CharMemLoc, IntPtr> addressSelector)
        {
            CharMemLoc charMemLoc = CharMemLoc.ByName(charName);
            var bytes = new byte[1];
            _memoryAccessor.ReadMem(Settings.ProcessName, addressSelector(charMemLoc), bytes);
            return bytes[0];
        }

        public void SetCharacterEquipment(CharNames charName, byte equipmentEquipmentId,
            Func<CharMemLoc, IntPtr> addressSelector)
        {
            CharMemLoc charMemLoc = CharMemLoc.ByName(charName);
            var bytes = new[] { equipmentEquipmentId };
            _memoryAccessor.WriteMem(Settings.ProcessName, addressSelector(charMemLoc), bytes);
        }
    }
}