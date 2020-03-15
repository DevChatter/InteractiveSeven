using System.Runtime.InteropServices;

namespace InteractiveSeven.Core.FinalFantasy.MemModels
{
    // Starting Address 0x009A8E9C
    [StructLayout(LayoutKind.Explicit, Size = 184)]
    public struct Scene
    {
        [MarshalAs(UnmanagedType.ByValArray, ArraySubType = UnmanagedType.U1, SizeConst = 12)]
        [FieldOffset(0)] public byte[] RawName;
    }
}