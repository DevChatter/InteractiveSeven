using InteractiveSeven.UI.Models;
using System;

namespace InteractiveSeven.UI.Memory
{
    public class MenuColorAccessor
    {
        private readonly IMemoryAccessor _memoryAccessor;

        private static readonly IntPtr TopLeftAddress = new IntPtr(0x0091EFC8);
        private static readonly IntPtr BotLeftAddress = new IntPtr(0x0091EFCC);
        private static readonly IntPtr TopRightAddress = new IntPtr(0x0091EFD0);
        private static readonly IntPtr BotRightAddress = new IntPtr(0x0091EFD4);

        public MenuColorAccessor(IMemoryAccessor memoryAccessor)
        {
            _memoryAccessor = memoryAccessor;
        }

        public MenuColors GetMenuColors(string processName)
        {
            byte[] topLeftBuffer = new byte[3];
            byte[] botLeftBuffer = new byte[3];
            byte[] topRightBuffer = new byte[3];
            byte[] botRightBuffer = new byte[3];

            _memoryAccessor.ReadMem(processName, TopLeftAddress, topLeftBuffer);
            _memoryAccessor.ReadMem(processName, BotLeftAddress, botLeftBuffer);
            _memoryAccessor.ReadMem(processName, TopRightAddress, topRightBuffer);
            _memoryAccessor.ReadMem(processName, BotRightAddress, botRightBuffer);

            return new MenuColors
            {
                TopLeft = new MenuCornerColor(topLeftBuffer),
                BotLeft = new MenuCornerColor(botLeftBuffer),
                TopRight = new MenuCornerColor(topRightBuffer),
                BotRight = new MenuCornerColor(botRightBuffer)
            };
        }

        public void SetMenuColors(string processName, MenuColors menuColors)
        {
            _memoryAccessor.WriteMem(processName, TopLeftAddress, menuColors.TopLeft.AsArray());
            _memoryAccessor.WriteMem(processName, BotLeftAddress, menuColors.BotLeft.AsArray());
            _memoryAccessor.WriteMem(processName, TopRightAddress, menuColors.TopRight.AsArray());
            _memoryAccessor.WriteMem(processName, BotRightAddress, menuColors.BotRight.AsArray());
        }

    }
}