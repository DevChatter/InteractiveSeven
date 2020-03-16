using InteractiveSeven.Core.FinalFantasy;

namespace InteractiveSeven.Core.Diagnostics.Memory
{
    public interface IMenuStatusAccessor
    {
        void LockMenu(MenuFlags menuFlags);
        void UnlockMenu(MenuFlags menuFlags);
        MenuFlags GetMenuStatus();
    }
}