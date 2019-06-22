using System.Collections.Generic;
using System.Linq;

namespace InteractiveSeven.Core.Data.Items
{
    public class Armlets
    {
        private const int ItemOffset = 256;
        public ushort ItemId => (ushort)(Value + ItemOffset);
        public string Name { get; }
        public ushort Value { get; }

        private Armlets(byte value, string name)
        {
            Name = name;
            Value = value;
        }

        public static IList<Armlets> All = new[]
        {
            new Armlets(0, "Bronze Bangle"),
            new Armlets(1, "Iron Bangle"),
            new Armlets(2, "Titan Bangle"),
            new Armlets(3, "Mythril Armlet"),
            new Armlets(4, "Carbon Bangle"),
            new Armlets(5, "Silver Armlet"),
            new Armlets(6, "Gold Armlet"),
            new Armlets(7, "Diamond Bangle"),
            new Armlets(8, "Crystal Bangle"),
            new Armlets(9, "Platinum Bangle"),
            new Armlets(10, "Rune Armlet"),
            new Armlets(11, "Edincoat"),
            new Armlets(12, "Wizard Bracelet"),
            new Armlets(13, "Adaman Bangle"),
            new Armlets(14, "Gigas Armlet"),
            new Armlets(15, "Imperial Guard"),
            new Armlets(16, "Aegis Armlet"),
            new Armlets(17, "Fourth Bracelet"),
            new Armlets(18, "Warrior Bangle"),
            new Armlets(19, "Shinra Beta"),
            new Armlets(20, "Shinra Alpha"),
            new Armlets(21, "Four Slots"),
            new Armlets(22, "Fire Armlet"),
            new Armlets(23, "Aurora Armlet"),
            new Armlets(24, "Bolt Armlet"),
            new Armlets(25, "Dragon Armlet"),
            new Armlets(26, "Minerva Band"),
            new Armlets(27, "Escort Guard"),
            new Armlets(28, "Mystile"),
            new Armlets(29, "Ziedrich"),
            new Armlets(30, "Precious Watch"),
            new Armlets(31, "Chocobracelet"),
        };

        public static bool IsValid(int armletId)
            => Get(armletId) != null;

        public static Armlets Get(int armletId)
            => All.SingleOrDefault(x => x.Value == armletId);
    }
}