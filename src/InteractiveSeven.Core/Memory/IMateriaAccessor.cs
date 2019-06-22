namespace InteractiveSeven.Core.Memory
{
    public interface IMateriaAccessor
    {
        void AddMateria(byte materiaId, ushort experience = ushort.MinValue);
    }
}