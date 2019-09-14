using System;

namespace Interactive.Core.Memory
{
    public class MemLoc
    {
        public IntPtr Address { get; set; }
        public int NumBytes { get; set; }
        public MemLoc(int address, int numBytes = 1)
        {
            Address = new IntPtr(address);
            NumBytes = numBytes;
        }

        public MemLoc(IntPtr address, int numBytes = 1)
        {
            Address = address;
            NumBytes = numBytes;
        }
    }
}
