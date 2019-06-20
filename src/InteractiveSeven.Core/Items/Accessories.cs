using System.Collections.Generic;
using System.Linq;

namespace InteractiveSeven.Core.Items
{
    public class Accessories
    {
        public string Name { get; }
        public int Id { get; }
        public int Value { get; }

        private Accessories(int id, int value, string name)
        {
            Name = name;
            Id = id;
            Value = value;
        }

        public static IList<Accessories> All = new[]
        {
            new Accessories(1, 0, "Power Wrist"),
            new Accessories(2, 1, "Protect Vest"),
            new Accessories(3, 2, "Earring"),
            new Accessories(4, 3, "Talisman"),
            new Accessories(5, 4, "Choco Feather"),
            new Accessories(6, 5, "Amulet"),
            new Accessories(7, 6, "Champion Belt"),
            new Accessories(8, 7, "Poison Ring"),
            new Accessories(9, 8, "Touph Ring"),
            new Accessories(10, 9, "Circlet"),
            new Accessories(11, 10, "Star Pendant"),
            new Accessories(12, 11, "Silver Glasses"),
            new Accessories(13, 12, "Headband"),
            new Accessories(14, 13, "Fairy Ring"),
            new Accessories(15, 14, "Jem Ring"),
            new Accessories(16, 15, "White Cape"),
            new Accessories(17, 16, "Sprint Shoes"),
            new Accessories(18, 17, "Peace Ring"),
            new Accessories(19, 18, "Ribbon"),
            new Accessories(20, 19, "Fire Ring"),
            new Accessories(21, 20, "Ice Ring"),
            new Accessories(22, 21, "Bolt Ring"),
            new Accessories(23, 22, "Tetra Elemental"),
            new Accessories(24, 23, "Safety Bit"),
            new Accessories(25, 24, "Fury Ring"),
            new Accessories(26, 25, "Curse Ring"),
            new Accessories(27, 26, "Protect Ring"),
            new Accessories(28, 27, "Cat's Bell"),
            new Accessories(29, 28, "Reflect Ring"),
            new Accessories(30, 29, "Water Ring"),
            new Accessories(31, 30, "Sneak Glove"),
            new Accessories(32, 31, "HypnoCrown"),
        };

        public static bool IsValid(int armletId)
            => Get(armletId) != null;

        public static Accessories Get(int armletId)
            => All.SingleOrDefault(x => x.Id == armletId);
    }
}