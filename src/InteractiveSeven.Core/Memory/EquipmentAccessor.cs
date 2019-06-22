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

        public void SetCharacterWeapon(CharNames charName, byte weapon)
        {
            CharMemLoc charMemLoc = CharMemLoc.ByName(charName);
            var bytes = new[] { weapon };
            _memoryAccessor.WriteMem(Settings.ProcessName, charMemLoc.Weapon.Address, bytes);
        }

        public void SetCharacterArmlet(CharNames charName, int armlet)
        {
            CharMemLoc charMemLoc = CharMemLoc.ByName(charName);
            var bytes = new[] { (byte)armlet };
            _memoryAccessor.WriteMem(Settings.ProcessName, charMemLoc.Armlet.Address, bytes);
        }

        public void SetCharacterAccessory(CharNames charName, int accessory)
        {
            CharMemLoc charMemLoc = CharMemLoc.ByName(charName);
            var bytes = new[] { (byte)accessory };
            _memoryAccessor.WriteMem(Settings.ProcessName, charMemLoc.Accessory.Address, bytes);
        }

        public byte GetCharacterWeapon(CharNames charName)
        {
            CharMemLoc charMemLoc = CharMemLoc.ByName(charName);
            var bytes = new byte[1];
            _memoryAccessor.ReadMem(Settings.ProcessName, charMemLoc.Weapon.Address, bytes);
            return bytes[0];
        }

        public byte GetCharacterArmlet(CharNames charName)
        {
            CharMemLoc charMemLoc = CharMemLoc.ByName(charName);
            var bytes = new byte[1];
            _memoryAccessor.ReadMem(Settings.ProcessName, charMemLoc.Armlet.Address, bytes);
            return bytes[0];
        }

        public byte GetCharacterAccessory(CharNames charName)
        {
            CharMemLoc charMemLoc = CharMemLoc.ByName(charName);
            var bytes = new byte[1];
            _memoryAccessor.ReadMem(Settings.ProcessName, charMemLoc.Accessory.Address, bytes);
            return bytes[0];
        }
    }
}