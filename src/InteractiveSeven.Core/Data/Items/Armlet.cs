namespace InteractiveSeven.Core.Data.Items
{
    public class Armlet : Equipment
    {
        private const int ArmletOffset = 256;

        internal Armlet(byte id, string name, ushort defaultPrice, bool enabled = true)
            : base(id, name, defaultPrice, enabled, ArmletOffset)
        {
        }

        public override bool IsMatchByCharacter(CharNames charName) => true;

        public override bool IsMatchById(ushort id, CharNames charName)
            => Id == id;

        public override bool IsMatchByItemId(ushort itemId, CharNames charName)
            => ItemId == itemId;

        public override bool IsMatchByEquipId(ushort equipId, CharNames charName)
            => EquipmentId == equipId;

        public override bool IsMatchByName(string name, CharNames charName)
            => IsMatchByName(name);
    }
}