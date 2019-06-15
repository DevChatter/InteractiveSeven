using System;

namespace InteractiveSeven.Core.Memory
{
    public class BattleMemoryLocations
    {
        public MemLoc BattleStartedIndicator { get; } = new MemLoc(new IntPtr(0x9A13BC));
    }
}