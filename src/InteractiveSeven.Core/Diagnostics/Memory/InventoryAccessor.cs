using System;
using System.Linq;
using InteractiveSeven.Core.Diagnostics.Memory.Model;
using InteractiveSeven.Core.Settings;

namespace InteractiveSeven.Core.Diagnostics.Memory
{
    public class InventoryAccessor : IInventoryAccessor
    {
        private readonly IMemoryAccessor _memory;
        private const ushort InvCapacity = 320;
        private const int ItemSize = 2;
        private static readonly IntPtr FirstAddress = new IntPtr(0xDC0234);
        private ApplicationSettings Settings => ApplicationSettings.Instance;

        public InventoryAccessor(IMemoryAccessor memory)
        {
            _memory = memory;
        }

        public void AddItem(ushort itemId, ushort quantity = 1, bool allowIncrement = true)
        {
            var scanResult = _memory.ScanMem(Settings.ProcessName,
                FirstAddress, ItemSize, InvCapacity, IsItem);

            if (scanResult.BaseAddrOffset == -1) // Have none, find empty
            {
                scanResult = _memory.ScanMem(Settings.ProcessName,
                    FirstAddress, ItemSize, InvCapacity, IsEmpty);
                WriteInventoryItem(new InventorySlot(itemId, quantity), scanResult.BaseAddrOffset);
            }
            else if (allowIncrement)
            {
                var item = new InventorySlot(scanResult.Bytes);
                item.Quantity += quantity;
                WriteInventoryItem(item, scanResult.BaseAddrOffset);
            }


            bool IsItem(byte[] bytes) => new InventorySlot(bytes).ItemId == itemId;
            bool IsEmpty(byte[] bytes) => bytes.All(b => b == byte.MaxValue);
            void WriteInventoryItem(InventorySlot inventorySlot, int addrOffset)
                => _memory.WriteMem(Settings.ProcessName, IntPtr.Add(FirstAddress, addrOffset), inventorySlot.AsBytes());
        }

        public void RemoveAllItems()
        {
            int inventoryTotalBytes = (InvCapacity * ItemSize);
            byte[] bytes = new byte[inventoryTotalBytes];
            for (int i = 0; i < inventoryTotalBytes; i++)
            {
                bytes[i] = byte.MaxValue;
            }
            _memory.WriteMem(Settings.ProcessName, FirstAddress, bytes);
        }

        public bool HasItem(ushort itemId)
        {
            var scanResult = _memory.ScanMem(Settings.ProcessName,
                FirstAddress, ItemSize, InvCapacity, IsTheItem);

            return scanResult.BaseAddrOffset > -1;

            bool IsTheItem(byte[] bytes) => new InventorySlot(bytes).ItemId == itemId;
        }

        public bool DropItem(ushort itemId, ushort quantity)
        {
            var scanResult = _memory.ScanMem(Settings.ProcessName,
                FirstAddress, ItemSize, InvCapacity, IsItem);

            if (scanResult.BaseAddrOffset == -1) return false; // Not found.

            var item = new InventorySlot(scanResult.Bytes);
            if (item.Quantity > quantity)
            {
                item.Quantity -= quantity;
                WriteInventoryItem(item, scanResult.BaseAddrOffset);
            }
            else
            {
                IntPtr address = IntPtr.Add(FirstAddress, scanResult.BaseAddrOffset);
                _memory.WriteMem(Settings.ProcessName, address, new[] { byte.MaxValue, byte.MaxValue });
            }

            return true;

            bool IsItem(byte[] bytes) => new InventorySlot(bytes).ItemId == itemId;
            void WriteInventoryItem(InventorySlot inventorySlot, int addrOffset)
                => _memory.WriteMem(Settings.ProcessName, IntPtr.Add(FirstAddress, addrOffset), inventorySlot.AsBytes());
        }
    }
}
