namespace InteractiveSeven.Core.Memory.Model
{
    public class InventorySlot
    {
        public ushort ItemId { get; set; }
        public ushort Quantity { get; set; }

        public InventorySlot(ushort itemId, ushort quantity)
        {
            ItemId = itemId;
            Quantity = quantity;
        }

        public InventorySlot(byte[] bytes)
        {
            Quantity = (ushort)(bytes[1] / 2);
            ItemId = (ushort)(bytes[0] + (bytes[1] % 2 == 0 ? 0 : 256));
        }

        public byte[] AsBytes
        {
            get
            {
                var bytes = new byte[2];
                bytes[0] = (byte) (ItemId > 255 ? ItemId - 256 : ItemId);
                bytes[1] = (byte) (Quantity * 2 + (ItemId > 255 ? 1 : 0));
                return bytes;
            }
        }
    }
}