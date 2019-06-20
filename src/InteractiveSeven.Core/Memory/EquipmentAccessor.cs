using InteractiveSeven.Core.Data;
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

        public void SetCharacterWeapon(CharNames charName, int weapon)
        {
            CharMemLoc charMemLoc = CharMemLoc.ByName(charName);
            var bytes = new[] { (byte)weapon };
            _memoryAccessor.WriteMem(Settings.ProcessName, charMemLoc.Weapon.Address, bytes);
        }

        public void SetCharacterArmlet(CharNames charName, int armlet)
        {
            CharMemLoc charMemLoc = CharMemLoc.ByName(charName);
            var bytes = new[] { (byte)armlet };
            _memoryAccessor.WriteMem(Settings.ProcessName, charMemLoc.Armlet.Address, bytes);
        }
    }
}