namespace InteractiveSeven.Core.Memory.Model
{
    public class MateriaSlot
    {
        public byte MateriaId { get; set; }
        public ushort Experience { get; set; }
        public byte Unknown { get; set; }

        public MateriaSlot(byte materiaId, ushort experience = ushort.MinValue, byte unknown= byte.MinValue)
        {
            MateriaId = materiaId;
            Experience = experience;
            Unknown = unknown;
        }

        public MateriaSlot(byte[] bytes)
        {
            MateriaId = bytes[0];
            Experience = (ushort)((bytes[1] << 8) + bytes[2]);
            Unknown = bytes[3];
        }

        public byte[] AsBytes()
        {
            var bytes = new byte[4];
            bytes[0] = MateriaId;
            bytes[1] = (byte)(Experience >> 8);
            bytes[2] = (byte)Experience;
            bytes[3] = Unknown;
            return bytes;
        }
    }
}