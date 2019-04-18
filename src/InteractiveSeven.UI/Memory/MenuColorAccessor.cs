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

            _memoryAccessor.ReadMem(processName, MemLoc.MenuTopLeft.Address, topLeftBuffer);
            _memoryAccessor.ReadMem(processName, MemLoc.MenuBotLeft.Address, botLeftBuffer);
            _memoryAccessor.ReadMem(processName, MemLoc.MenuTopRight.Address, topRightBuffer);
            _memoryAccessor.ReadMem(processName, MemLoc.MenuBotRight.Address, botRightBuffer);

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
            _memoryAccessor.WriteMem(processName, MemLoc.MenuTopLeft.Address, menuColors.TopLeft.AsArray());
            _memoryAccessor.WriteMem(processName, MemLoc.MenuBotLeft.Address, menuColors.BotLeft.AsArray());
            _memoryAccessor.WriteMem(processName, MemLoc.MenuTopRight.Address, menuColors.TopRight.AsArray());
            _memoryAccessor.WriteMem(processName, MemLoc.MenuBotRight.Address, menuColors.BotRight.AsArray());
        }

    }
}