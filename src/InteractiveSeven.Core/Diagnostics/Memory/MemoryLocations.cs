using System;

namespace InteractiveSeven.Core.Diagnostics.Memory
{
    public class MemLoc
    {
        public IntPtr Address { get; set; }
        public int NumBytes { get; set; }
        internal MemLoc(int address, int numBytes = 1)
        {
            Address = new IntPtr(address);
            NumBytes = numBytes;
        }

        internal MemLoc(IntPtr address, int numBytes = 1)
        {
            Address = address;
            NumBytes = numBytes;
        }
    }
}
