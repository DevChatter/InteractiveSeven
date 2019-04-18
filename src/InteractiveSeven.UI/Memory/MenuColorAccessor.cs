using InteractiveSeven.UI.Models;
using System;

namespace InteractiveSeven.UI.Memory
{
    public class MenuColorAccessor
    {
        private readonly IMemoryAccessor _memoryAccessor;

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

            _memoryAccessor.ReadMem(processName, MemoryLocations.TopLeftAddress, topLeftBuffer);
            _memoryAccessor.ReadMem(processName, MemoryLocations.BotLeftAddress, botLeftBuffer);
            _memoryAccessor.ReadMem(processName, MemoryLocations.TopRightAddress, topRightBuffer);
            _memoryAccessor.ReadMem(processName, MemoryLocations.BotRightAddress, botRightBuffer);

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
            _memoryAccessor.WriteMem(processName, MemoryLocations.TopLeftAddress, menuColors.TopLeft.AsArray());
            _memoryAccessor.WriteMem(processName, MemoryLocations.BotLeftAddress, menuColors.BotLeft.AsArray());
            _memoryAccessor.WriteMem(processName, MemoryLocations.TopRightAddress, menuColors.TopRight.AsArray());
            _memoryAccessor.WriteMem(processName, MemoryLocations.BotRightAddress, menuColors.BotRight.AsArray());
        }

    }
}