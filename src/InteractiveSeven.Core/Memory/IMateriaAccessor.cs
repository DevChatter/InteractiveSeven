namespace InteractiveSeven.Core.Memory
{
    public interface IMateriaAccessor
    {
        void AddMateria(byte materiaId, uint experience = 0);
    }
}