namespace InteractiveSeven.Core.Diagnostics.Memory
{
    public interface IGpAccessor
    {
        ushort GetGp();
        void AddGp(in ushort amount);
        void RemoveGp(ushort amount);
    }
}