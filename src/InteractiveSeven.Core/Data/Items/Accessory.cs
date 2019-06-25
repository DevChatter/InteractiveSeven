namespace InteractiveSeven.Core.Data.Items
{
    public class Accessory : Equipment
    {
        private const int ItemOffset = 288;

        internal Accessory(byte value, string name)
            : base(value, name, ItemOffset)
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