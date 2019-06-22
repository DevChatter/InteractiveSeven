using InteractiveSeven.Core.Memory.Model;
using InteractiveSeven.Core.Settings;
using System;
using System.Linq;

namespace InteractiveSeven.Core.Memory
{
    public class MateriaAccessor : IMateriaAccessor
    {
        private readonly IMemoryAccessor _memory;
        private const ushort InvCapacity = 200;
        private const int ItemSize = 4;
        private static readonly IntPtr FirstAddress = new IntPtr(0xDC04B4);
        private ApplicationSettings Settings => ApplicationSettings.Instance;

        public MateriaAccessor(IMemoryAccessor memory)
        {
            _memory = memory;
        }

        public void AddMateria(byte materiaId, ushort experience = ushort.MinValue)
        {
            var scanResult = _memory.ScanMem(Settings.ProcessName,
                FirstAddress, ItemSize, InvCapacity, IsEmpty);

            if (scanResult.BaseAddrOffset == -1) return;

            IntPtr address = IntPtr.Add(FirstAddress, scanResult.BaseAddrOffset);
            var materiaSlot = new MateriaSlot(materiaId, experience);
            _memory.WriteMem(Settings.ProcessName, address, materiaSlot.AsBytes());

            bool IsEmpty(byte[] bytes) => bytes.All(b => b == byte.MaxValue);
        }
    }
}