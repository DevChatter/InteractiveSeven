using System.Collections.Generic;
using System.Linq;

namespace InteractiveSeven.Core.Items
{
    public class Weapons
    {
        public string Name { get; }
        public int Id { get; }
        public int Value { get; }

        private Weapons(int id, int value, string name)
        {
            Name = name;
            Id = id;
            Value = value;
        }

        public static IList<Weapons> CloudWeapons = new[]
        {
            new Weapons(1, 0, "Buster Sword"),
            new Weapons(2, 1, "Mythril Saber"),
            new Weapons(3, 2, "Hardedge"),
            new Weapons(4, 3, "Butterfly Edge"),
            new Weapons(5, 4, "Enhance Sword"),
            new Weapons(6, 5, "Organics"),
            new Weapons(7, 6, "Crystal Sword"),
            new Weapons(8, 7, "Force Stealer"),
            new Weapons(9, 8, "Rune Blade"),
            new Weapons(10, 9, "Murasame"),
            new Weapons(11, 10, "Nail Bat"),
            new Weapons(12, 11, "Yoshiyuki"),
            new Weapons(13, 12, "Apocalypse"),
            new Weapons(14, 13, "Heavens Cloud"),
            new Weapons(15, 14, "Ragnarok"),
            new Weapons(16, 15, "Ultima Weapon"),
        };

        public static IList<Weapons> TifaWeapons = new[]
        {
            new Weapons(1, 16, "Leather Glove"),
            new Weapons(2, 17, "Metal Knuckle"),
            new Weapons(3, 18, "Mythril Claw"),
            new Weapons(4, 19, "Grand Glove"),
            new Weapons(5, 20, "Tiger Fang"),
            new Weapons(6, 21, "Diamond Knuckle"),
            new Weapons(7, 22, "Dragon Claw"),
            new Weapons(8, 23, "Crystal Glove"),
            new Weapons(9, 24, "Motor Drive"),
            new Weapons(10,25, "Platinum Fist"),
            new Weapons(11,26, "Kaiser Knuckle"),
            new Weapons(12,27, "Work Glove"),
            new Weapons(13,28, "Powersoul"),
            new Weapons(14,29, "Master Fist"),
            new Weapons(15,30, "God's Hand"),
            new Weapons(16,31, "Premium Heart"),
        };

        public static IList<Weapons> BarretWeapons = new[]
        {
            new Weapons(1, 32, "Gatling Gun"),
            new Weapons(2, 33, "Assault Gun"),
            new Weapons(3, 34, "Cannon Ball"),
            new Weapons(4, 35, "Atomic Scissors"),
            new Weapons(5, 36, "Heavy Vulcan"),
            new Weapons(6, 37, "Chainsaw"),
            new Weapons(7, 38, "Microlaser"),
            new Weapons(8, 39, "A* M Cannon"),
            new Weapons(9, 40, "W Machine Gun"),
            new Weapons(10,41, "Drill Arm"),
            new Weapons(11,42, "Solid Bazooka"),
            new Weapons(12,43, "Rocket Punch"),
            new Weapons(13,44, "Enemy Launcher"),
            new Weapons(14,45, "Pile Banger"),
            new Weapons(15,46, "Max Ray"),
            new Weapons(16,47, "Missing Score"),
        };

        public static IList<Weapons> RedWeapons = new[]
        {
            new Weapons(1, 48, "Mythril Clip"),
            new Weapons(2, 49, "Diamond Pin"),
            new Weapons(3, 50, "Silver Barrette"),
            new Weapons(4, 51, "Gold Barrette"),
            new Weapons(5, 52, "Adaman Clip"),
            new Weapons(6, 53, "Crystal Comb"),
            new Weapons(7, 54, "Magic Comb"),
            new Weapons(8, 55, "Plus Barrette"),
            new Weapons(9, 56, "Centclip"),
            new Weapons(10,57, "Hairpin"),
            new Weapons(11,58, "Seraph Comb"),
            new Weapons(12,59, "Behimoth Horn"),
            new Weapons(13,60, "Spring Gun Clip"),
            new Weapons(14,61, "Limited Moon"),
        };

        public static IList<Weapons> AerisWeapons = new[]
        {
            new Weapons(1,  62, "Guard Stick"),
            new Weapons(2,  63, "Mythril Rod"),
            new Weapons(3,  64, "Full Metal Staff"),
            new Weapons(4,  65, "Striking Staff"),
            new Weapons(5,  66, "Prism Staff"),
            new Weapons(6,  67, "Aurora Rod"),
            new Weapons(7,  68, "Wizard Staff"),
            new Weapons(8,  69, "Wizer Staff"),
            new Weapons(9,  70, "Fairy Tale"),
            new Weapons(10, 71, "Umbrella"),
            new Weapons(11, 72, "Princess Guard"),
        };

        public static IList<Weapons> CidWeapons = new[]
        {
            new Weapons(1,  73, "Spear"),
            new Weapons(2,  74, "Slash Lance"),
            new Weapons(3,  75, "Trident"),
            new Weapons(4,  76, "Mast Axe"),
            new Weapons(5,  77, "Partisan"),
            new Weapons(6,  78, "Viper Halberd"),
            new Weapons(7,  79, "Javelin"),
            new Weapons(8,  80, "Grow Lance"),
            new Weapons(9,  81, "Mop"),
            new Weapons(10, 82, "Dragoon Lance"),
            new Weapons(11, 83, "Scimitar"),
            new Weapons(12, 84, "Flayer"),
            new Weapons(13, 85, "Spirit Lance"),
            new Weapons(14, 86, "Venus Gospel"),
        };

        public static IList<Weapons> YuffieWeapons = new[]
        {
            new Weapons(1,  87 , "4-point Shuriken"),
            new Weapons(2,  88 , "Boomerang"),
            new Weapons(3,  89 , "Pinwheel"),
            new Weapons(4,  90 , "Razor Ring"),
            new Weapons(5,  91 , "Hawkeye"),
            new Weapons(6,  92 , "Crystal Cross"),
            new Weapons(7,  93 , "Wind Slash"),
            new Weapons(8,  94 , "Twin Viper"),
            new Weapons(9,  95 , "Spiral Shuriken"),
            new Weapons(10, 96 , "Superball"),
            new Weapons(11, 97 , "Magic Shuriken"),
            new Weapons(12, 98 , "Rising Sun"),
            new Weapons(13, 99 , "Oritsuru"),
            new Weapons(14, 100, "Conformer"),
        };

        public static IList<Weapons> CaitSithWeapons = new[]
        {
            new Weapons(1,  101, "Yellow M-phone"),
            new Weapons(2,  102, "Green M-phone"),
            new Weapons(3,  103, "Blue M-phone"),
            new Weapons(4,  104, "Red M-phone"),
            new Weapons(5,  105, "Crystal M-phone"),
            new Weapons(6,  106, "White M-phone"),
            new Weapons(7,  107, "Black M-phone"),
            new Weapons(8,  108, "Silver M-phone"),
            new Weapons(9,  109, "Trumpet Shell"),
            new Weapons(10, 110, "Gold M-phone"),
            new Weapons(11, 111, "Battle Trumpet"),
            new Weapons(12, 112, "Starlight Phone"),
            new Weapons(13, 113, "HP Shout"),
        };

        public static IList<Weapons> VincentWeapons = new[]
        {
            new Weapons(1,  114, "Quicksilver"),
            new Weapons(2,  115, "Shotgun"),
            new Weapons(3,  116, "Shortbarrel"),
            new Weapons(4,  117, "Lariat"),
            new Weapons(5,  118, "Winchester"),
            new Weapons(6,  119, "Peacemaker"),
            new Weapons(7,  120, "Buntline"),
            new Weapons(8,  121, "Long Barrel R"),
            new Weapons(9,  122, "Silver Rifle"),
            new Weapons(10, 123, "Sniper CR"),
            new Weapons(11, 124, "Supershot ST"),
            new Weapons(12, 125, "Outsider"),
            new Weapons(13, 126, "Death Penalty"),
        };

        public static Dictionary<string, IList<Weapons>> AllWeapons
            = new Dictionary<string, IList<Weapons>>
            {
                [CharNames.Cloud] = CloudWeapons,
                [CharNames.Tifa] = TifaWeapons,
                [CharNames.Barret] = BarretWeapons,
                [CharNames.Red] = RedWeapons,
                [CharNames.Aeris] = AerisWeapons,
                [CharNames.Cid] = CidWeapons,
                [CharNames.Yuffie] = YuffieWeapons,
                [CharNames.CaitSith] = CaitSithWeapons,
                [CharNames.Vincent] = VincentWeapons,
            };

        public static bool IsValid(string charName, int weaponId)
            => Get(charName, weaponId) != null;

        public static Weapons Get(string charName, int weaponId)
        {
            if (!AllWeapons.TryGetValue(charName, out IList<Weapons> charWeapons))
            {
                return null;
            }

            return charWeapons.SingleOrDefault(x => x.Id == weaponId);
        }
    }
}