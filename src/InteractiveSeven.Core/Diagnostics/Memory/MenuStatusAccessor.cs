using InteractiveSeven.Core.FinalFantasy;
using InteractiveSeven.Core.Settings;
using System;

namespace InteractiveSeven.Core.Diagnostics.Memory
{
    public class MenuStatusAccessor : IMenuStatusAccessor
    {
        private readonly IMemoryAccessor _memoryAccessor;

        private string ProcessName => ApplicationSettings.Instance.ProcessName;

        public MenuStatusAccessor(IMemoryAccessor memoryAccessor)
        {
            _memoryAccessor = memoryAccessor;
        }

        public void LockMenu(MenuFlags menuFlags)
        {
            MenuFlags menuStatus = GetMenuStatus();

            menuStatus |= menuFlags;

            byte[] bytes = BitConverter.GetBytes((int)menuStatus);

            _memoryAccessor.WriteMem(ProcessName, Addresses.MenuLock.Address, bytes);
        }

        public void UnlockMenu(MenuFlags menuFlags)
        {
            MenuFlags menuStatus = GetMenuStatus();

            menuStatus ^= menuFlags;

            byte[] bytes = BitConverter.GetBytes((int)menuStatus);

            _memoryAccessor.WriteMem(ProcessName, Addresses.MenuLock.Address, bytes);
        }

        public MenuFlags GetMenuStatus()
        {
            byte[] bytes = new byte[Addresses.MenuLock.NumBytes];
            _memoryAccessor.ReadMem(ProcessName, Addresses.MenuLock.Address, bytes);
            return (MenuFlags)BitConverter.ToUInt16(bytes);
        }
    }
}