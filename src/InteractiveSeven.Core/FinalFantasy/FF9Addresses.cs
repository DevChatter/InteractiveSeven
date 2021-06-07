using InteractiveSeven.Core.Diagnostics.Memory;

namespace InteractiveSeven.Core.FinalFantasy
{
    public static class FF9Addresses
    {
        //Find Address of "Common State" subtract 0xC
        //Follow Pointer
        //Offset 20
        //Follow Pointer
        //Offset 20
        //Follow Pointer
        //Offset 20
        //Follow Pointer
        //Offset 1C


        public static MemLoc Gil { get; set; } = new MemLoc(0x25CA660C, 4);
        public static MemLoc ZidaneName { get; set; } = new MemLoc(0x27E2DDD4); // Probably wrong.
    }
}
