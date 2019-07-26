using System;

namespace InteractiveSeven.Core.Diagnostics.Memory
{
    public static class BattleMemoryLocations
    {
        public static MemLoc BattleStartedIndicator { get; } = new MemLoc(new IntPtr(0x9A13BC), 4);
    }
}