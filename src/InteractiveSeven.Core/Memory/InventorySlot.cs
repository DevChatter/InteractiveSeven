namespace InteractiveSeven.Core.Memory
{
    public class InventorySlot
    {
        private const ushort QuantityBits = 0b_0000_0000_0111_1111;
        public byte[] AsBytes
        {
            get
            {
                var bytes = new byte[2];
                bytes[0] = (byte) (ItemId > 255 ? ItemId - 255 : ItemId);
                bytes[1] = (byte) (Quantity * 2 + (ItemId > 255 ? 1 : 0));
                return bytes;
            }
        }

        public InventorySlot()
        {
            ItemId = ushort.MaxValue;
            Quantity = ushort.MaxValue;
        }

        public InventorySlot(byte[] bytes)
        {
            Quantity = (ushort)(bytes[1] / 2);
            ItemId = (ushort)(bytes[0] + (bytes[1] % 2 == 0 ? 0 : 255));
        }

        public ushort ItemId { get; set; }

        public ushort Quantity { get; set; }
    }
}