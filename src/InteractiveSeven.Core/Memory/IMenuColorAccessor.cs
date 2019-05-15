using InteractiveSeven.Core.Models;

namespace InteractiveSeven.Core.Memory
{
    public interface IMenuColorAccessor
    {
        MenuColors GetMenuColors(string processName);
        void SetMenuColors(string processName, MenuColors menuColors);
    }
}