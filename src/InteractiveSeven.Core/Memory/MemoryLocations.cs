using System;

namespace InteractiveSeven.Core.Memory
{
    public class MemLoc
    {
        public IntPtr Address { get; set; }
        public int NumBytes { get; set; }
        private MemLoc(int address, int numBytes = 1)
        {
            Address = new IntPtr(address);
            NumBytes = numBytes;
        }

        public static readonly MemLoc MenuTopLeft = new MemLoc(0x91EFC8, 3); // order blue, green, red
        public static readonly MemLoc MenuBotLeft = new MemLoc(0x91EFCC, 3);
        public static readonly MemLoc MenuTopRight = new MemLoc(0x91EFD0, 3);
        public static readonly MemLoc MenuBotRight = new MemLoc(0x91EFD4, 3);

        public static readonly MemLoc CloudLimit = new MemLoc(0xDBFD9B);
        public static readonly MemLoc BarretLimit = new MemLoc(0xDBFE1F);
        public static readonly MemLoc TifaLimit = new MemLoc(0xDBFEA3);

        public static readonly MemLoc CloudMaxHP = new MemLoc(0xDBFDBA, 2);
        public static readonly MemLoc CloudCurHP = new MemLoc(0xDBFDB8, 2);
        public static readonly MemLoc CloudStr = new MemLoc(0xDBFD8E);
        public static readonly MemLoc CloudVit = new MemLoc(0xDBFD8F);
        public static readonly MemLoc CloudMag = new MemLoc(0xDBFD90);
        public static readonly MemLoc CloudSpi = new MemLoc(0xDBFD91);
        public static readonly MemLoc CloudDex = new MemLoc(0xDBFD92);
        public static readonly MemLoc CloudLuc = new MemLoc(0xDBFD93);

        public static readonly MemLoc TifaMaxHP = new MemLoc(0xDBFEC2, 2);
        public static readonly MemLoc TifaCurHP = new MemLoc(0xDBFEC0, 2);
        public static readonly MemLoc TifaStr = new MemLoc(0xDBFE96);
        public static readonly MemLoc TifaVit = new MemLoc(0xDBFE97);
        public static readonly MemLoc TifaMag = new MemLoc(0xDBFE98);
        public static readonly MemLoc TifaSpi = new MemLoc(0xDBFE99);
        public static readonly MemLoc TifaDex = new MemLoc(0xDBFE9A);
        public static readonly MemLoc TifaLuc = new MemLoc(0xDBFE9B);

        public static readonly MemLoc BarretMaxHP = new MemLoc(0xDBFE3E, 2);
        public static readonly MemLoc BarretCurHP = new MemLoc(0xDBFE3C, 2);
        public static readonly MemLoc BarretStr = new MemLoc(0xDBFE12);
        public static readonly MemLoc BarretVit = new MemLoc(0xDBFE13);
        public static readonly MemLoc BarretMag = new MemLoc(0xDBFE14);
        public static readonly MemLoc BarretSpi = new MemLoc(0xDBFE15);
        public static readonly MemLoc BarretDex = new MemLoc(0xDBFE16);
        public static readonly MemLoc BarretLuc = new MemLoc(0xDBFE17);

        public static readonly MemLoc Gil = new MemLoc(0xDC08B4, 4); // 4 bytes
    }
}
