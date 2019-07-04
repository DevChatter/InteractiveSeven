using InteractiveSeven.Core.Models;

namespace InteractiveSeven.Core.Memory
{
    public class MenuColorAccessor : IMenuColorAccessor
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
                TopLeft = topLeftBuffer.ToColor(),
                BotLeft = botLeftBuffer.ToColor(),
                TopRight = topRightBuffer.ToColor(),
                BotRight = botRightBuffer.ToColor(),
            };
        }

        public void SetMenuColors(string processName, MenuColors menuColors)
        {
            byte[] displayBytes = {
                menuColors.TopLeft.B,
                menuColors.TopLeft.G,
                menuColors.TopLeft.R,
                128,
                menuColors.BotLeft.B,
                menuColors.BotLeft.G,
                menuColors.BotLeft.R,
                128,
                menuColors.TopRight.B,
                menuColors.TopRight.G,
                menuColors.TopRight.R,
                128,
                menuColors.BotRight.B,
                menuColors.BotRight.G,
                menuColors.BotRight.R,
                128
            };
            // TODO: Change to writes of contiguous memory.
            _memoryAccessor.WriteMem(processName, MemLoc.MenuColorAll.Address, displayBytes);

            byte[] saveBytes = {
                menuColors.TopLeft.R,
                menuColors.TopLeft.G,
                menuColors.TopLeft.B,
                menuColors.BotLeft.R,
                menuColors.BotLeft.G,
                menuColors.BotLeft.B,
                menuColors.TopRight.R,
                menuColors.TopRight.G,
                menuColors.TopRight.B,
                menuColors.BotRight.R,
                menuColors.BotRight.G,
                menuColors.BotRight.B
            };

            _memoryAccessor.WriteMem(processName, MemLoc.MenuColorAllSave.Address, saveBytes);
        }

    }
}