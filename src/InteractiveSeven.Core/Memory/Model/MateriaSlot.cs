namespace InteractiveSeven.Core.Memory.Model
{
    public class MateriaSlot
    {
        public byte MateriaId { get; set; }
        public uint Experience { get; set; }

        public MateriaSlot(byte materiaId, uint experience = 0, byte unknown = 0)
        {
            MateriaId = materiaId;
            Experience = experience;
        }

        public MateriaSlot(byte[] bytes)
        {
            MateriaId = bytes[0];
            Experience = (uint)((bytes[3] << 16) + (bytes[2] << 8) + bytes[1]);
        }

        public byte[] AsBytes()
        {
            var bytes = new byte[4];
            bytes[0] = MateriaId;
            bytes[1] = (byte)Experience; // Little-Endian
            bytes[2] = (byte)(Experience >> 8);
            bytes[3] = (byte)(Experience >> 16);
            return bytes;
        }
    }
}