using System.Collections.Generic;
using System.Linq;

namespace InteractiveSeven.Core.Data.Items
{
    public class Armlets : Items
    {
        private const int ArmletOffset = 256;

        internal Armlets(byte value, string name)
            : base(value, name, ArmletOffset)
        {
        }

        public static IList<Armlets> AllArmlets = Items.All.OfType<Armlets>().ToList();

        public static bool IsValid(int armletId)
            => Get(armletId) != null;

        public static Armlets GetArmlet(int armletId)
            => AllArmlets.SingleOrDefault(x => x.Value == armletId);
    }
}