using InteractiveSeven.Core.Data;

namespace InteractiveSeven.Core.Memory
{
    public interface IEquipmentAccessor
    {
        void SetCharacterWeapon(CharNames charName, int weapon);
    }
}