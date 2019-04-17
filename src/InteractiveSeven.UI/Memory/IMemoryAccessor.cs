using System;

namespace InteractiveSeven.UI.Memory
{
    public interface IMemoryAccessor
    {
        void ReadMem(string processName, IntPtr address, byte[] buffer);
        void WriteMem(string processName, IntPtr address, byte[] bytes);
    }
}