namespace InteractiveSeven.Core.Data.Items
{
    public abstract class Weapon : Equipment
    {
        public CharNames CharName { get; }

        protected Weapon(ushort id, string name, ushort defaultPrice, ushort typeOffset, CharNames charName,
            byte weaponOffset, bool enabled, params string[] words)
            : base(id, name, defaultPrice, enabled, typeOffset, weaponOffset, words)
        {
            CharName = charName;
        }

        public override bool IsMatchByCharacter(CharNames charName) => CharName == charName;

        public override bool IsMatchById(ushort id, CharNames charName = null)
            => Id == id && CharName == charName;

        public override bool IsMatchByItemId(ushort itemId, CharNames charName = null)
            => ItemId == itemId && CharName == charName;

        public override bool IsMatchByEquipId(ushort equipId, CharNames charName = null)
            => EquipmentId == equipId && CharName == charName;

        public override bool IsMatchByName(string name, CharNames charName = null)
            => Name.NoSpaces().StartsWithIns(name) && CharName == charName;
    }

    public class CloudWeapon : Weapon
    {
        internal CloudWeapon(ushort id, string name, ushort defaultPrice, params string[] words)
            : base(id, name, defaultPrice, 128, CharNames.Cloud, 0, true, words)
        {
        }
    }

    public class TifaWeapon : Weapon
    {
        internal TifaWeapon(ushort id, string name, ushort defaultPrice, params string[] words)
            : base(id, name, defaultPrice, 144, CharNames.Tifa, 16, true, words)
        {
        }
    }

    public class BarretWeapon : Weapon
    {
        internal BarretWeapon(ushort id, string name, ushort defaultPrice, params string[] words)
            : base(id, name, defaultPrice, 160, CharNames.Barret, 32, true, words)
        {
        }
    }

    public class RedWeapon : Weapon
    {
        internal RedWeapon(ushort id, string name, ushort defaultPrice, params string[] words)
            : base(id, name, defaultPrice, 176, CharNames.Red, 48, true, words)
        {
        }
    }

    public class AerisWeapon : Weapon
    {
        internal AerisWeapon(ushort id, string name, ushort defaultPrice, params string[] words)
            : base(id, name, defaultPrice, 190, CharNames.Aeris, 62, true, words)
        {
        }
    }

    public class CidWeapon : Weapon
    {
        internal CidWeapon(ushort id, string name, ushort defaultPrice, params string[] words)
            : base(id, name, defaultPrice, 201, CharNames.Cid, 73, true, words)
        {
        }
    }

    public class YuffieWeapon : Weapon
    {
        internal YuffieWeapon(ushort id, string name, ushort defaultPrice, params string[] words)
            : base(id, name, defaultPrice, 215, CharNames.Yuffie, 87, true, words)
        {
        }
    }

    public class CaitSithWeapon : Weapon
    {
        internal CaitSithWeapon(ushort id, string name, ushort defaultPrice, params string[] words)
            : base(id, name, defaultPrice, 229, CharNames.CaitSith, 101, true, words)
        {
        }
    }

    public class VincentWeapon : Weapon
    {
        internal VincentWeapon(ushort id, string name, ushort defaultPrice, params string[] words)
            : base(id, name, defaultPrice, 242, CharNames.Vincent, 114, true, words)
        {
        }
    }

    public class SephirothWeapon : Weapon
    {
        internal SephirothWeapon(ushort id, string name, params string[] words)
            : base(id, name, ushort.MaxValue, 255, null, 127, false, words)
        {
        }
    }
}