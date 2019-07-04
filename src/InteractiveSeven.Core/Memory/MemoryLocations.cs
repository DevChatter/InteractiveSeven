using System;
using System.Net;

namespace InteractiveSeven.Core.Memory
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

        public static readonly MemLoc MenuColorAll = new MemLoc(0x91EFC8, 16); // 4 bytes each in order blue, green, red, 128
        public static readonly MemLoc MenuTopLeft = new MemLoc(0x91EFC8, 3); // order blue, green, red
        public static readonly MemLoc MenuBotLeft = new MemLoc(0x91EFCC, 3); // order blue, green, red
        public static readonly MemLoc MenuTopRight = new MemLoc(0x91EFD0, 3); // order blue, green, red
        public static readonly MemLoc MenuBotRight = new MemLoc(0x91EFD4, 3); // order blue, green, red

        public static readonly MemLoc MenuColorAllSave = new MemLoc(0x91EFD8, 3); // order red, green, blue
        public static readonly MemLoc MenuTopLeftSave = new MemLoc(0x91EFD8, 3); // order red, green, blue
        public static readonly MemLoc MenuBotLeftSave = new MemLoc(0x91EFDB, 3); // order red, green, blue
        public static readonly MemLoc MenuTopRightSave = new MemLoc(0x91EFDE, 3); // order red, green, blue
        public static readonly MemLoc MenuBotRightSave = new MemLoc(0x91EFE1, 3); // order red, green, blue

        public static readonly MemLoc Gil = new MemLoc(0xDC08B4, 4); // 4 bytes
        public static readonly MemLoc GameMoment = new MemLoc(0xDC08DC, 2);
    }
}
