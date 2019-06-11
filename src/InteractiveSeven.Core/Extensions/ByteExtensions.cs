namespace InteractiveSeven.Core
{
    public static class ByteExtensions
    {
        public static byte ToByte(this string src)
        {
            byte.TryParse(src, out byte result);
            return result;
        }
    }
}