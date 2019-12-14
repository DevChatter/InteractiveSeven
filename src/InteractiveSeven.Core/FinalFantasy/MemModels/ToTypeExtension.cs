using System.Runtime.InteropServices;

namespace InteractiveSeven.Core.FinalFantasy.MemModels
{
    public static class ToTypeExtension
    {
        // TODO : Make a Span<T> version of this
        public static T ToType<T>(this byte[] bytes)
            where T : struct
        {
            T stuff;
            GCHandle handle = GCHandle.Alloc(bytes, GCHandleType.Pinned);
            try
            {
                stuff = (T)Marshal.PtrToStructure(handle.AddrOfPinnedObject(), typeof(T));
            }
            finally
            {
                handle.Free();
            }
            return stuff;
        }
    }
}