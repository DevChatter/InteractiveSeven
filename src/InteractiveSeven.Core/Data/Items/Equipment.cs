namespace InteractiveSeven.Core.Data.Items
{
    public abstract class Equipment : Items
    {
        public byte EquipmentId { get; }
        protected Equipment(ushort id, string name, ushort defaultPrice, bool enabled, ushort typeOffset = 0,
            byte weaponOffset = 0, params string[] words)
            : base(id, name, defaultPrice, enabled, typeOffset, words)
        {
            EquipmentId = (byte)(id + weaponOffset);
        }

        public abstract bool IsMatchByCharacter(CharNames charName);
        public abstract bool IsMatchById(ushort id, CharNames charName);
        public abstract bool IsMatchByItemId(ushort itemId, CharNames charName);
        public abstract bool IsMatchByEquipId(ushort equipId, CharNames charName);
        public abstract bool IsMatchByName(string name, CharNames charName);
    }
}
