using InteractiveSeven.Core.Data;

namespace InteractiveSeven.Core.Diagnostics.Memory
{
    public interface IMateriaAccessor
    {
        void AddMateria(byte materiaId, uint experience = 0);
        void RemoveAllMateria();
        void RemoveWeaponMateria(CharNames charNames, int keep = 0);
        void RemoveArmletMateria(CharNames charNames, int keep = 0);
        bool HasMateria(byte materiaId);
        bool DropMateria(byte materiaId);
    }
}