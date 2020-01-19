namespace InteractiveSeven.Core.Diagnostics.Memory
{
    public interface IGilAccessor
    {
        void SetGil(int gil);
        uint GetGil();
        void AddGil(in uint amount);
        void RemoveGil(uint amount);
    }
}