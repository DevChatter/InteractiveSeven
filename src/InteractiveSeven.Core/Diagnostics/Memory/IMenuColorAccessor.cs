using InteractiveSeven.Core.Models;

namespace InteractiveSeven.Core.Diagnostics.Memory
{
    public interface IMenuColorAccessor
    {
        MenuColors GetMenuColors(string processName);
        void SetMenuColors(string processName, MenuColors menuColors);
    }
}