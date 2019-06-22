using System.Collections.Generic;
using System.Linq;

namespace InteractiveSeven.Core.Data.Items
{
    public class Weapons
    {
        public string Name { get; }
        public byte Value { get; }
        public ushort ItemId { get; }

        private Weapons(byte value, ushort itemId, string name)
        {
            Name = name;
            Value = value;
            ItemId = itemId;
        }

        public static IList<Weapons> CloudWeapons = new[]
        {
            new Weapons(0, 128, "Buster Sword"),
            new Weapons(1, 129, "Mythril Saber"),
            new Weapons(2, 130, "Hardedge"),
            new Weapons(3, 131, "Butterfly Edge"),
            new Weapons(4, 132, "Enhance Sword"),
            new Weapons(5, 133, "Organics"),
            new Weapons(6, 134, "Crystal Sword"),
            new Weapons(7, 135, "Force Stealer"),
            new Weapons(8, 136, "Rune Blade"),
            new Weapons(9, 137, "Murasame"),
            new Weapons(10, 138, "Nail Bat"),
            new Weapons(11, 139, "Yoshiyuki"),
            new Weapons(12, 140, "Apocalypse"),
            new Weapons(13, 141, "Heavens Cloud"),
            new Weapons(14, 142, "Ragnarok"),
            new Weapons(15, 143, "Ultima Weapon"),
        };

        public static IList<Weapons> TifaWeapons = new[]
        {
            new Weapons(16, 144, "Leather Glove"),
            new Weapons(17, 145, "Metal Knuckle"),
            new Weapons(18, 146, "Mythril Claw"),
            new Weapons(19, 147, "Grand Glove"),
            new Weapons(20, 148, "Tiger Fang"),
            new Weapons(21, 149, "Diamond Knuckle"),
            new Weapons(22, 150, "Dragon Claw"),
            new Weapons(23, 151, "Crystal Glove"),
            new Weapons(24, 152, "Motor Drive"),
            new Weapons(25, 153, "Platinum Fist"),
            new Weapons(26, 154, "Kaiser Knuckle"),
            new Weapons(27, 155, "Work Glove"),
            new Weapons(28, 156, "Powersoul"),
            new Weapons(29, 157, "Master Fist"),
            new Weapons(30, 158, "God's Hand"),
            new Weapons(31, 159, "Premium Heart"),
        };

        public static IList<Weapons> BarretWeapons = new[]
        {
            new Weapons(32, 160, "Gatling Gun"),
            new Weapons(33, 161, "Assault Gun"),
            new Weapons(34, 162, "Cannon Ball"),
            new Weapons(35, 163, "Atomic Scissors"),
            new Weapons(36, 164, "Heavy Vulcan"),
            new Weapons(37, 165, "Chainsaw"),
            new Weapons(38, 166, "Microlaser"),
            new Weapons(39, 167, "A* M Cannon"),
            new Weapons(40, 168, "W Machine Gun"),
            new Weapons(41, 169, "Drill Arm"),
            new Weapons(42, 170, "Solid Bazooka"),
            new Weapons(43, 171, "Rocket Punch"),
            new Weapons(44, 172, "Enemy Launcher"),
            new Weapons(45, 173, "Pile Banger"),
            new Weapons(46, 174, "Max Ray"),
            new Weapons(47, 175, "Missing Score"),
        };

        public static IList<Weapons> RedWeapons = new[]
        {
            new Weapons(48, 176, "Mythril Clip"),
            new Weapons(49, 177, "Diamond Pin"),
            new Weapons(50, 178, "Silver Barrette"),
            new Weapons(51, 179, "Gold Barrette"),
            new Weapons(52, 180, "Adaman Clip"),
            new Weapons(53, 181, "Crystal Comb"),
            new Weapons(54, 182, "Magic Comb"),
            new Weapons(55, 183, "Plus Barrette"),
            new Weapons(56, 184, "Centclip"),
            new Weapons(57, 185, "Hairpin"),
            new Weapons(58, 186, "Seraph Comb"),
            new Weapons(59, 187, "Behimoth Horn"),
            new Weapons(60, 188, "Spring Gun Clip"),
            new Weapons(61, 189, "Limited Moon"),
        };

        public static IList<Weapons> AerisWeapons = new[]
        {
            new Weapons(62, 190, "Guard Stick"),
            new Weapons(63, 191, "Mythril Rod"),
            new Weapons(64, 192, "Full Metal Staff"),
            new Weapons(65, 193, "Striking Staff"),
            new Weapons(66, 194, "Prism Staff"),
            new Weapons(67, 195, "Aurora Rod"),
            new Weapons(68, 196, "Wizard Staff"),
            new Weapons(69, 197, "Wizer Staff"),
            new Weapons(70, 198, "Fairy Tale"),
            new Weapons(71, 199, "Umbrella"),
            new Weapons(72, 200, "Princess Guard"),
        };

        public static IList<Weapons> CidWeapons = new[]
        {
            new Weapons(73, 201, "Spear"),
            new Weapons(74, 202, "Slash Lance"),
            new Weapons(75, 203, "Trident"),
            new Weapons(76, 204, "Mast Axe"),
            new Weapons(77, 205, "Partisan"),
            new Weapons(78, 206, "Viper Halberd"),
            new Weapons(79, 207, "Javelin"),
            new Weapons(80, 208, "Grow Lance"),
            new Weapons(81, 209, "Mop"),
            new Weapons(82, 210, "Dragoon Lance"),
            new Weapons(83, 211, "Scimitar"),
            new Weapons(84, 212, "Flayer"),
            new Weapons(85, 213, "Spirit Lance"),
            new Weapons(86, 214, "Venus Gospel"),
        };

        public static IList<Weapons> YuffieWeapons = new[]
        {
            new Weapons(87, 215, "4-point Shuriken"),
            new Weapons(88, 216, "Boomerang"),
            new Weapons(89, 217, "Pinwheel"),
            new Weapons(90, 218, "Razor Ring"),
            new Weapons(91, 219, "Hawkeye"),
            new Weapons(92, 220, "Crystal Cross"),
            new Weapons(93, 221, "Wind Slash"),
            new Weapons(94, 222, "Twin Viper"),
            new Weapons(95, 223, "Spiral Shuriken"),
            new Weapons(96, 224, "Superball"),
            new Weapons(97, 225, "Magic Shuriken"),
            new Weapons(98, 226, "Rising Sun"),
            new Weapons(99, 227, "Oritsuru"),
            new Weapons(100, 228, "Conformer"),
        };

        public static IList<Weapons> CaitSithWeapons = new[]
        {
            new Weapons(101, 229, "Yellow M-phone"),
            new Weapons(102, 230, "Green M-phone"),
            new Weapons(103, 231, "Blue M-phone"),
            new Weapons(104, 232, "Red M-phone"),
            new Weapons(105, 233, "Crystal M-phone"),
            new Weapons(106, 234, "White M-phone"),
            new Weapons(107, 235, "Black M-phone"),
            new Weapons(108, 236, "Silver M-phone"),
            new Weapons(109, 237, "Trumpet Shell"),
            new Weapons(110, 238, "Gold M-phone"),
            new Weapons(111, 239, "Battle Trumpet"),
            new Weapons(112, 240, "Starlight Phone"),
            new Weapons(113, 241, "HP Shout"),
        };

        public static IList<Weapons> VincentWeapons = new[]
        {
            new Weapons(114, 242, "Quicksilver"),
            new Weapons(115, 243, "Shotgun"),
            new Weapons(116, 244, "Shortbarrel"),
            new Weapons(117, 245, "Lariat"),
            new Weapons(118, 246, "Winchester"),
            new Weapons(119, 247, "Peacemaker"),
            new Weapons(120, 248, "Buntline"),
            new Weapons(121, 249, "Long Barrel R"),
            new Weapons(122, 250, "Silver Rifle"),
            new Weapons(123, 251, "Sniper CR"),
            new Weapons(124, 252, "Supershot ST"),
            new Weapons(125, 253, "Outsider"),
            new Weapons(126, 254, "Death Penalty"),
        };

        public static Dictionary<int, IList<Weapons>> AllWeapons
            = new Dictionary<int, IList<Weapons>>
            {
                [CharNames.Cloud.Id] = CloudWeapons,
                [CharNames.Tifa.Id] = TifaWeapons,
                [CharNames.Barret.Id] = BarretWeapons,
                [CharNames.Red.Id] = RedWeapons,
                [CharNames.Aeris.Id] = AerisWeapons,
                [CharNames.Cid.Id] = CidWeapons,
                [CharNames.Yuffie.Id] = YuffieWeapons,
                [CharNames.CaitSith.Id] = CaitSithWeapons,
                [CharNames.Vincent.Id] = VincentWeapons,
            };

        public static bool IsValid(CharNames charName, int weaponId)
            => Get(charName, weaponId) != null;

        public static Weapons Get(CharNames charName, int weaponId)
        {
            if (!AllWeapons.TryGetValue(charName.Id, out IList<Weapons> charWeapons))
            {
                return null;
            }

            return charWeapons.SingleOrDefault(x => x.Value == weaponId);
        }
    }
}