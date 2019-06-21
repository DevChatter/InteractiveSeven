namespace InteractiveSeven.Core.Memory
{
    public interface IInventoryAccessor
    {
        void AddItem(ushort itemId, ushort quantity, bool allowIncrement);
    }
}