using System;

namespace InteractiveSeven.Core.Memory
{
    public interface IMemoryAccessor
    {
        void ReadMem(string processName, IntPtr address, byte[] buffer);
        void WriteMem(string processName, IntPtr address, byte[] bytes);
        ScanResult ScanMem(string processName, IntPtr startAddr,
            ushort itemSize, uint capacity, Func<byte[], bool> isMatch);
    }
}