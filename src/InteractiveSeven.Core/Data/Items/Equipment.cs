namespace InteractiveSeven.Core.Data.Items
{
    public abstract class Equipment : Items
    {
        public byte EquipmentId { get; }
        protected Equipment(ushort value, string name, ushort typeOffset = 0,
            byte weaponOffset = 0, params string[] words)
            : base(value, name, typeOffset, words)
        {
            EquipmentId = (byte)(value + weaponOffset);
        }

        public abstract bool IsMatchById(ushort id, CharNames charName = null);
        public abstract bool IsMatchByItemId(ushort itemId, CharNames charName = null);
        public abstract bool IsMatchByEquipId(ushort equipId, CharNames charName = null);
    }
}
