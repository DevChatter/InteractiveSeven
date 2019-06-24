using System.Collections.Generic;
using System.Linq;

namespace InteractiveSeven.Core.Data.Items
{
    public static class Weapons
    {
        public static Dictionary<int, IList<BaseWeapons>> AllWeapons
            = new Dictionary<int, IList<BaseWeapons>>
            {
                [CharNames.Cloud.Id] = Items.All.OfType<CloudWeapons>().Select(x => (BaseWeapons)x).ToList(),
                [CharNames.Tifa.Id] = Items.All.OfType<TifaWeapons>().Select(x => (BaseWeapons)x).ToList(),
                [CharNames.Barret.Id] = Items.All.OfType<BarretWeapons>().Select(x => (BaseWeapons)x).ToList(),
                [CharNames.Red.Id] = Items.All.OfType<RedWeapons>().Select(x => (BaseWeapons)x).ToList(),
                [CharNames.Aeris.Id] = Items.All.OfType<AerisWeapons>().Select(x => (BaseWeapons)x).ToList(),
                [CharNames.Cid.Id] = Items.All.OfType<CidWeapons>().Select(x => (BaseWeapons)x).ToList(),
                [CharNames.Yuffie.Id] = Items.All.OfType<YuffieWeapons>().Select(x => (BaseWeapons)x).ToList(),
                [CharNames.CaitSith.Id] = Items.All.OfType<CaitSithWeapons>().Select(x => (BaseWeapons)x).ToList(),
                [CharNames.Vincent.Id] = Items.All.OfType<VincentWeapons>().Select(x => (BaseWeapons)x).ToList(),
            };

        public static bool IsValid(CharNames charName, int weaponId)
            => Get(charName, weaponId) != null;

        public static BaseWeapons Get(CharNames charName, int weaponId)
        {
            if (!AllWeapons.TryGetValue(charName.Id, out IList<BaseWeapons> charWeapons))
            {
                return null;
            }

            return charWeapons.SingleOrDefault(x => x.WeaponId == weaponId);
        }
    }

    public abstract class BaseWeapons : Items
    {
        public byte WeaponId { get; }

        protected BaseWeapons(ushort value, string name, ushort weaponIdOffset,
            ushort itemIdOffset = 0, params string[] words)
            : base(value, name, itemIdOffset, words)
        {
            WeaponId = (byte)(ItemId - weaponIdOffset);
        }
    }

    public class CloudWeapons : BaseWeapons
    {
        internal CloudWeapons(ushort value, string name, params string[] words)
            : base(value, name, 128, 0, words)
        {
        }
    }

    public class TifaWeapons : BaseWeapons
    {
        internal TifaWeapons(ushort value, string name, params string[] words)
            : base(value, name, 144, 16, words)
        {
        }
    }

    public class BarretWeapons : BaseWeapons
    {
        internal BarretWeapons(ushort value, string name, params string[] words)
            : base(value, name, 160, 32, words)
        {
        }
    }

    public class RedWeapons : BaseWeapons
    {
        internal RedWeapons(ushort value, string name, params string[] words)
            : base(value, name, 176, 48, words)
        {
        }
    }

    public class AerisWeapons : BaseWeapons
    {
        internal AerisWeapons(ushort value, string name, params string[] words)
            : base(value, name, 190, 62, words)
        {
        }
    }

    public class CidWeapons : BaseWeapons
    {
        internal CidWeapons(ushort value, string name, params string[] words)
            : base(value, name, 201, 73, words)
        {
        }
    }

    public class YuffieWeapons : BaseWeapons
    {
        internal YuffieWeapons(ushort value, string name, params string[] words)
            : base(value, name, 215, 87, words)
        {
        }
    }

    public class CaitSithWeapons : BaseWeapons
    {
        internal CaitSithWeapons(ushort value, string name, params string[] words)
            : base(value, name, 229, 101, words)
        {
        }
    }

    public class VincentWeapons : BaseWeapons
    {
        internal VincentWeapons(ushort value, string name, params string[] words)
            : base(value, name, 242, 114, words)
        {
        }
    }
}