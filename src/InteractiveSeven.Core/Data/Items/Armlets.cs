namespace InteractiveSeven.Core.Data.Items
{
    public class Armlets : Equipment // TODO: Rename to Armlet
    {
        private const int ArmletOffset = 256;

        internal Armlets(byte value, string name)
            : base(value, name, ArmletOffset)
        {
        }

        public override bool IsMatchById(ushort id, CharNames charName = null)
            => Value == id;

        public override bool IsMatchByItemId(ushort itemId, CharNames charName = null)
            => ItemId == itemId;

        public override bool IsMatchByEquipId(ushort equipId, CharNames charName = null)
            => EquipmentId == equipId;
    }
}