using System;

namespace InteractiveSeven.Core.Memory
{
    public class InventorySlot
    {
        private const ushort QuantityBits = 0b_0000_0000_0111_1111;
        public byte[] AsBytes => BitConverter.GetBytes(Value);
        public ushort Value { get; set; }

        public InventorySlot()
        {
            Value = 0b_1111_1111_1111_1111;
        }

        public InventorySlot(ushort fullValue)
        {
            Value = fullValue;
        }

        public ushort ItemId
        {
            get => (ushort)(Value >> 7);
            set => Value = (ushort) ((ushort) (Value & QuantityBits) + (value << 7));
        }

        public ushort Quantity
        {
            get => (ushort)(Value & QuantityBits);
            set => Value = (ushort)((ushort)(Value & ~QuantityBits) + (value & QuantityBits));
        }
    }
}