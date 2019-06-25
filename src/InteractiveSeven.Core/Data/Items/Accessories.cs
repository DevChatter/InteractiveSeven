using System.Collections.Generic;
using System.Linq;

namespace InteractiveSeven.Core.Data.Items
{
    public class Accessories : Items
    {
        private const int ItemOffset = 288;

        internal Accessories(byte value, string name)
            : base(value, name, ItemOffset)
        {
        }

        public static IList<Accessories> AllAccessories = Items.All.OfType<Accessories>().ToList();

        public static bool IsValid(int accessoryId)
            => GetAccessory(accessoryId) != null;

        public static Accessories GetAccessory(int accessoryId)
            => AllAccessories.SingleOrDefault(x => x.Value == accessoryId);
    }
}