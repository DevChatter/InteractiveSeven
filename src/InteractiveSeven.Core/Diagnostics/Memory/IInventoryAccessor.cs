namespace InteractiveSeven.Core.Diagnostics.Memory
{
    public interface IInventoryAccessor
    {
        void AddItem(ushort itemId, ushort quantity, bool allowIncrement);
        void RemoveAllItems();
    }
}