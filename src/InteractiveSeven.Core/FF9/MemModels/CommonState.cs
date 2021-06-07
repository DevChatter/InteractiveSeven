using System.Runtime.InteropServices;

namespace InteractiveSeven.Core.FF9.MemModels
{
    [StructLayout(LayoutKind.Explicit, Size = 0x24)]
    public class CommonState
    {
        [FieldOffset(0x20)]
        public readonly SecondLevel SecondLevel;
    }

    [StructLayout(LayoutKind.Explicit, Size = 0x24)]
    public class SecondLevel
    {
        [FieldOffset(0x20)]
        public readonly ThirdLevel ThirdLevel;
    }

    [StructLayout(LayoutKind.Explicit, Size = 0x24)]
    public class ThirdLevel
    {
        [FieldOffset(0x1C)] public uint Gil;
    }
}
