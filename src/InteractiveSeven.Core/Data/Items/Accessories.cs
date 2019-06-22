using System.Collections.Generic;
using System.Linq;

namespace InteractiveSeven.Core.Data.Items
{
    public class Accessories
    {
        private const int ItemOffset = 288;
        public ushort ItemId => (ushort)(Value + ItemOffset);
        public string Name { get; }
        public byte Value { get; }

        private Accessories(byte value, string name)
        {
            Name = name;
            Value = value;
        }

        public static IList<Accessories> All = new[]
        {
            new Accessories(0, "Power Wrist"),
            new Accessories(1, "Protect Vest"),
            new Accessories(2, "Earring"),
            new Accessories(3, "Talisman"),
            new Accessories(4, "Choco Feather"),
            new Accessories(5, "Amulet"),
            new Accessories(6, "Champion Belt"),
            new Accessories(7, "Poison Ring"),
            new Accessories(8, "Touph Ring"),
            new Accessories(9, "Circlet"),
            new Accessories(10, "Star Pendant"),
            new Accessories(11, "Silver Glasses"),
            new Accessories(12, "Headband"),
            new Accessories(13, "Fairy Ring"),
            new Accessories(14, "Jem Ring"),
            new Accessories(15, "White Cape"),
            new Accessories(16, "Sprint Shoes"),
            new Accessories(17, "Peace Ring"),
            new Accessories(18, "Ribbon"),
            new Accessories(19, "Fire Ring"),
            new Accessories(20, "Ice Ring"),
            new Accessories(21, "Bolt Ring"),
            new Accessories(22, "Tetra Elemental"),
            new Accessories(23, "Safety Bit"),
            new Accessories(24, "Fury Ring"),
            new Accessories(25, "Curse Ring"),
            new Accessories(26, "Protect Ring"),
            new Accessories(27, "Cat's Bell"),
            new Accessories(28, "Reflect Ring"),
            new Accessories(29, "Water Ring"),
            new Accessories(30, "Sneak Glove"),
            new Accessories(31, "HypnoCrown"),
        };

        public static bool IsValid(int accessoryId)
            => Get(accessoryId) != null;

        public static Accessories Get(int accessoryId)
            => All.SingleOrDefault(x => x.Value == accessoryId);
    }
}