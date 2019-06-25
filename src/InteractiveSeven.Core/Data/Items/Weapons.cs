namespace InteractiveSeven.Core.Data.Items
{
    public abstract class Weapons : Equipment
    {
        public CharNames CharName { get; }

        protected Weapons(ushort value, string name, ushort typeOffset, CharNames charName,
            byte weaponOffset = 0, params string[] words)
            : base(value, name, typeOffset, weaponOffset, words)
        {
            CharName = charName;
        }

        public override bool IsMatchById(ushort id, CharNames charName = null)
            => Value == id && CharName == charName;

        public override bool IsMatchByItemId(ushort itemId, CharNames charName = null)
            => ItemId == itemId && CharName == charName;

        public override bool IsMatchByEquipId(ushort equipId, CharNames charName = null)
            => EquipmentId == equipId && CharName == charName;
    }

    public class CloudWeapons : Weapons
    {
        internal CloudWeapons(ushort value, string name, params string[] words)
            : base(value, name, 128, CharNames.Cloud, 0, words)
        {
        }
    }

    public class TifaWeapons : Weapons
    {
        internal TifaWeapons(ushort value, string name, params string[] words)
            : base(value, name, 144, CharNames.Tifa, 16, words)
        {
        }
    }

    public class BarretWeapons : Weapons
    {
        internal BarretWeapons(ushort value, string name, params string[] words)
            : base(value, name, 160, CharNames.Barret, 32, words)
        {
        }
    }

    public class RedWeapons : Weapons
    {
        internal RedWeapons(ushort value, string name, params string[] words)
            : base(value, name, 176, CharNames.Red, 48, words)
        {
        }
    }

    public class AerisWeapons : Weapons
    {
        internal AerisWeapons(ushort value, string name, params string[] words)
            : base(value, name, 190, CharNames.Aeris, 62, words)
        {
        }
    }

    public class CidWeapons : Weapons
    {
        internal CidWeapons(ushort value, string name, params string[] words)
            : base(value, name, 201, CharNames.Cid, 73, words)
        {
        }
    }

    public class YuffieWeapons : Weapons
    {
        internal YuffieWeapons(ushort value, string name, params string[] words)
            : base(value, name, 215, CharNames.Yuffie, 87, words)
        {
        }
    }

    public class CaitSithWeapons : Weapons
    {
        internal CaitSithWeapons(ushort value, string name, params string[] words)
            : base(value, name, 229, CharNames.CaitSith, 101, words)
        {
        }
    }

    public class VincentWeapons : Weapons
    {
        internal VincentWeapons(ushort value, string name, params string[] words)
            : base(value, name, 242, CharNames.Vincent, 114, words)
        {
        }
    }

    public class SephirothWeapons : Weapons
    {
        internal SephirothWeapons(ushort value, string name, params string[] words)
            : base(value, name, 255, null, 127, words)
        {
        }
    }
}