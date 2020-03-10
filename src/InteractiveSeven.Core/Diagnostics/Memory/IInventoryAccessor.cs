namespace InteractiveSeven.Core.Diagnostics.Memory
{
    public interface IInventoryAccessor
    {
        void AddItem(ushort itemId, ushort quantity, bool allowIncrement);
        void RemoveAllItems();
        bool HasItem(ushort itemId);
        bool DropItem(ushort itemId, ushort quantity);
    }
}