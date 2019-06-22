using InteractiveSeven.Core.Data;

namespace InteractiveSeven.Core.Memory
{
    public interface IEquipmentAccessor
    {
        void SetCharacterWeapon(CharNames charName, byte weapon);
        void SetCharacterArmlet(CharNames charName, int armlet);
        void SetCharacterAccessory(CharNames charName, int accessory);
        byte GetCharacterWeapon(CharNames charName);
        byte GetCharacterArmlet(CharNames charName);
        byte GetCharacterAccessory(CharNames charName);
    }
}