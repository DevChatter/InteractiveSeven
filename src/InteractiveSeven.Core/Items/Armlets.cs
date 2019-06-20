using System.Collections.Generic;
using System.Linq;

namespace InteractiveSeven.Core.Items
{
    public class Armlets
    {
        public string Name { get; }
        public int Id { get; }
        public int Value { get; }

        private Armlets(int id, int value, string name)
        {
            Name = name;
            Id = id;
            Value = value;
        }

        public static IList<Armlets> All = new[]
        {
            new Armlets(1, 0, "Bronze Bangle"),
            new Armlets(2, 1, "Iron Bangle"),
            new Armlets(3, 2, "Titan Bangle"),
            new Armlets(4, 3, "Mythril Armlet"),
            new Armlets(5, 4, "Carbon Bangle"),
            new Armlets(6, 5, "Silver Armlet"),
            new Armlets(7, 6, "Gold Armlet"),
            new Armlets(8, 7, "Diamond Bangle"),
            new Armlets(9, 8, "Crystal Bangle"),
            new Armlets(10, 9, "Platinum Bangle"),
            new Armlets(11, 10, "Rune Armlet"),
            new Armlets(12, 11, "Edincoat"),
            new Armlets(13, 12, "Wizard Bracelet"),
            new Armlets(14, 13, "Adaman Bangle"),
            new Armlets(15, 14, "Gigas Armlet"),
            new Armlets(16, 15, "Imperial Guard"),
            new Armlets(17, 16, "Aegis Armlet"),
            new Armlets(18, 17, "Fourth Bracelet"),
            new Armlets(19, 18, "Warrior Bangle"),
            new Armlets(20, 19, "Shinra Beta"),
            new Armlets(21, 20, "Shinra Alpha"),
            new Armlets(22, 21, "Four Slots"),
            new Armlets(23, 22, "Fire Armlet"),
            new Armlets(24, 23, "Aurora Armlet"),
            new Armlets(25, 24, "Bolt Armlet"),
            new Armlets(26, 25, "Dragon Armlet"),
            new Armlets(27, 26, "Minerva Band"),
            new Armlets(28, 27, "Escort Guard"),
            new Armlets(29, 28, "Mystile"),
            new Armlets(30, 29, "Ziedrich"),
            new Armlets(31, 30, "Precious Watch"),
        };

        public static bool IsValid(int armletId)
            => Get(armletId) != null;

        public static Armlets Get(int armletId)
            => All.SingleOrDefault(x => x.Id == armletId);
    }
}