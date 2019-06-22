using System;
using System.Linq;
using InteractiveSeven.Core.Memory.Model;
using InteractiveSeven.Core.Settings;

namespace InteractiveSeven.Core.Memory
{
    public class InventoryAccessor : IInventoryAccessor
    {
        private readonly IMemoryAccessor _memory;
        private const ushort InvCapacity = 319;
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
    }
}