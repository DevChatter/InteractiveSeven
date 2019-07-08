namespace InteractiveSeven.Core.Data.Items
{
    public class Accessory : Equipment
    {
        private const int ItemOffset = 288;

        internal Accessory(byte id, string name, ushort defaultPrice, bool enabled = true)
            : base(id, name, defaultPrice, enabled, ItemOffset)
        {
        }

        public override bool IsMatchById(ushort id, CharNames charName = null)
            => Id == id;

        public override bool IsMatchByItemId(ushort itemId, CharNames charName = null)
            => ItemId == itemId;

        public override bool IsMatchByEquipId(ushort equipId, CharNames charName = null)
            => EquipmentId == equipId;
    }
}