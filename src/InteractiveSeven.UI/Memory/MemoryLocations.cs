using System;

namespace InteractiveSeven.UI.Memory
{
    public static class MemoryLocations
    {
        public static readonly IntPtr TopLeftAddress = new IntPtr(0x91EFC8); // 3 sequential bytes blue, green, red
        public static readonly IntPtr BotLeftAddress = new IntPtr(0x91EFCC);
        public static readonly IntPtr TopRightAddress = new IntPtr(0x91EFD0);
        public static readonly IntPtr BotRightAddress = new IntPtr(0x91EFD4);

        public static readonly IntPtr CloudLimitAddress = new IntPtr(0xDBFD9B); // byte
        public static readonly IntPtr BarretLimitAddress = new IntPtr(0xDBFE1F); // byte
        public static readonly IntPtr TifaLimitAddress = new IntPtr(0xDBFEA3); // byte

        public static readonly IntPtr CloudMaxHPAddress = new IntPtr(0xDBA4AA); // 2 bytes
        public static readonly IntPtr CloudCurHPAddress = new IntPtr(0xDBFDB8); // 2 bytes

        public static readonly IntPtr TifaMaxHPAddress = new IntPtr(0xDBA8EA); // 2 bytes
        public static readonly IntPtr TifaCurHPAddress = new IntPtr(0xDBFEC0); // 2 bytes

        public static readonly IntPtr BarretMaxHPAddress = new IntPtr(0xDBAD2A); // 2 bytes
        public static readonly IntPtr BarretCurHPAddress = new IntPtr(0xDBFE3C); // 2 bytes

        public static readonly IntPtr GilAddress = new IntPtr(0xDC08B4); // 4 bytes
    }
}
