using System;
using InteractiveSeven.Core.Settings;

namespace InteractiveSeven.Core.Memory
{
    public class EquipmentAccessor : IEquipmentAccessor
    {
        private readonly IMemoryAccessor _memoryAccessor;

        private ApplicationSettings Settings => ApplicationSettings.Instance;

        public EquipmentAccessor(IMemoryAccessor memoryAccessor)
        {
            _memoryAccessor = memoryAccessor;
        }

        public void SetCharacterWeapon(string characterName, int weapon)
        {
            CharMemLoc charMemLoc = CharMemLoc.ByName(characterName);
            var bytes = new[] { (byte)weapon };
            _memoryAccessor.WriteMem(Settings.ProcessName, charMemLoc.Weapon.Address, bytes);
        }
    }
}