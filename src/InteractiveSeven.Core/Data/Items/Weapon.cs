namespace InteractiveSeven.Core.Data.Items
{
    public abstract class Weapon : Equipment
    {
        public CharNames CharName { get; }

        protected Weapon(ushort id, string name, ushort typeOffset, CharNames charName,
            byte weaponOffset = 0, params string[] words)
            : base(id, name, typeOffset, weaponOffset, words)
        {
            CharName = charName;
        }

        public override bool IsMatchById(ushort id, CharNames charName = null)
            => Id == id && CharName == charName;

        public override bool IsMatchByItemId(ushort itemId, CharNames charName = null)
            => ItemId == itemId && CharName == charName;

        public override bool IsMatchByEquipId(ushort equipId, CharNames charName = null)
            => EquipmentId == equipId && CharName == charName;
    }

    public class CloudWeapon : Weapon
    {
        internal CloudWeapon(ushort id, string name, params string[] words)
            : base(id, name, 128, CharNames.Cloud, 0, words)
        {
        }
    }

    public class TifaWeapon : Weapon
    {
        internal TifaWeapon(ushort id, string name, params string[] words)
            : base(id, name, 144, CharNames.Tifa, 16, words)
        {
        }
    }

    public class BarretWeapon : Weapon
    {
        internal BarretWeapon(ushort id, string name, params string[] words)
            : base(id, name, 160, CharNames.Barret, 32, words)
        {
        }
    }

    public class RedWeapon : Weapon
    {
        internal RedWeapon(ushort id, string name, params string[] words)
            : base(id, name, 176, CharNames.Red, 48, words)
        {
        }
    }

    public class AerisWeapon : Weapon
    {
        internal AerisWeapon(ushort id, string name, params string[] words)
            : base(id, name, 190, CharNames.Aeris, 62, words)
        {
        }
    }

    public class CidWeapon : Weapon
    {
        internal CidWeapon(ushort id, string name, params string[] words)
            : base(id, name, 201, CharNames.Cid, 73, words)
        {
        }
    }

    public class YuffieWeapon : Weapon
    {
        internal YuffieWeapon(ushort id, string name, params string[] words)
            : base(id, name, 215, CharNames.Yuffie, 87, words)
        {
        }
    }

    public class CaitSithWeapon : Weapon
    {
        internal CaitSithWeapon(ushort id, string name, params string[] words)
            : base(id, name, 229, CharNames.CaitSith, 101, words)
        {
        }
    }

    public class VincentWeapon : Weapon
    {
        internal VincentWeapon(ushort id, string name, params string[] words)
            : base(id, name, 242, CharNames.Vincent, 114, words)
        {
        }
    }

    public class SephirothWeapon : Weapon
    {
        internal SephirothWeapon(ushort id, string name, params string[] words)
            : base(id, name, 255, null, 127, words)
        {
        }
    }
}