using InteractiveSeven.Core.Diagnostics.Memory;

namespace InteractiveSeven.Core.FinalFantasy
{
    public class FF9Addresses
    {
        public MemLoc Gil { get; set; } = new MemLoc(0x25CA660C, 4);
        public MemLoc ZidaneName { get; set; } = new MemLoc(0x27E2DDD4); // Probably wrong.
    }
}
