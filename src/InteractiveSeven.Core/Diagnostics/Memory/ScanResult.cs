namespace InteractiveSeven.Core.Diagnostics.Memory
{
    public class ScanResult
    {
        public int BaseAddrOffset { get; }
        public byte[] Bytes { get; }

        public ScanResult(int baseAddrOffset, byte[] bytes)
        {
            BaseAddrOffset = baseAddrOffset;
            Bytes = bytes;
        }
    }
}