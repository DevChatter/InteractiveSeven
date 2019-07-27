using InteractiveSeven.Core.Data;

namespace InteractiveSeven.Core.Diagnostics.Memory
{
    public interface IMateriaAccessor
    {
        void AddMateria(byte materiaId, uint experience = 0);
        void RemoveAllMateria();
        void RemoveWeaponMateria(CharNames charNames);
        void RemoveArmletMateria(CharNames charNames);
    }
}