using System.Collections.Generic;
using System.Linq;

namespace InteractiveSeven.Core.Data.Items
{
    public class Items
    {
        public string Name { get; }
        public ushort Value { get; }
        public string[] Words { get; set; }
        public ushort ItemIdOffset { get; set; }

        public ushort ItemId => (ushort)(Value - ItemIdOffset);

        protected Items(ushort value, string name, ushort itemIdOffset = 0, params string[] words)
        {
            Name = name;
            Value = value;
            Words = words ?? new string[0];
            ItemIdOffset = itemIdOffset;
        }

        public static IList<Items> All = new[]
        {
            new Items(0, "Potion"),
            new Items(1, "Hi-Potion"),
            new Items(2, "X-Potion"),
            new Items(3, "Ether"),
            new Items(4, "Turbo Ether"),
            new Items(5, "Elixir"),
            new Items(6, "Megalixir"),
            new Items(7, "Phoenix Down"),
            new Items(8, "Antidote"),
            new Items(9, "Soft"),
            new Items(10, "Maiden's Kiss"),
            new Items(11, "Cornucopia"),
            new Items(12, "Echo Screen"),
            new Items(13, "Hyper"),
            new Items(14, "Tranquilizer"),
            new Items(15, "Remedy"),
            new Items(16, "Smoke Bomb"),
            new Items(17, "Speed Drink"),
            new Items(18, "Hero Drink"),
            new Items(19, "Vaccine"),
            new Items(20, "Grenade"),
            new Items(21, "Shrapnel"),
            new Items(22, "Right arm"),
            new Items(23, "Hourglass"),
            new Items(24, "Kiss of Death"),
            new Items(25, "Spider Web"),
            new Items(27, "Mute Mask"),
            new Items(28, "War Gong"),
            new Items(29, "Loco weed"),
            new Items(30, "Fire Fang"),
            new Items(31, "Fire Veil"),
            new Items(32, "Antarctic Wind"),
            new Items(33, "Ice Crystal"),
            new Items(34, "Bolt Plume"),
            new Items(35, "Swift Bolt"),
            new Items(36, "Earth Drum"),
            new Items(37, "Earth Mallet"),
            new Items(38, "Deadly Waste"),
            new Items(39, "M-Tentacles"),
            new Items(40, "Stardust"),
            new Items(41, "Vampire Fang"),
            new Items(42, "Ghost Hand"),
            new Items(43, "Vagyrisk Claw"),
            new Items(44, "Light Curtain"),
            new Items(45, "Lunar Curtain"),
            new Items(46, "Mirror"),
            new Items(47, "Holy Torch"),
            new Items(48, "Bird Wing"),
            new Items(49, "Dragon Scales"),
            new Items(50, "Impaler"),
            new Items(51, "Shrivel"),
            new Items(52, "Eye drop"),
            new Items(53, "Molotov"),
            new Items(54, "S-mine"),
            new Items(55, "8inch Cannon"),
            new Items(56, "Graviball"),
            new Items(57, "T/S Bomb"),
            new Items(58, "Ink"),
            new Items(59, "Dazers"),
            new Items(60, "Dragon Fang"),
            new Items(61, "Cauldron"),
            new Items(62, "Sylkis Greens"),
            new Items(63, "Reagan Greens"),
            new Items(64, "Mimett Greens"),
            new Items(65, "Curiel Greens"),
            new Items(66, "Pahsana Greens"),
            new Items(67, "Tantal Greens"),
            new Items(68, "Krakka Greens"),
            new Items(70, "Tent"),
            new Items(71, "Power Source"),
            new Items(72, "Guard Source"),
            new Items(73, "Magic Source"),
            new Items(74, "Mind Source"),
            new Items(75, "Speed Source"),
            new Items(76, "Luck Source"),
            new Items(77, "Zeio Nut"),
            new Items(78, "Carob Nut"),
            new Items(79, "Porov Nut"),
            new Items(80, "Pram Nut"),
            new Items(81, "Lasan Nut"),
            new Items(82, "Saraha Nut"),
            new Items(83, "Luchile Nut"),
            new Items(84, "Pepio Nut"),
            new Items(85, "Battery"),
            new Items(86, "Tissue"),
            new Items(87, "Omnislash"),
            new Items(88, "Catastrophe"),
            new Items(89, "Final Heaven"),
            new Items(90, "Great Gospel"),
            new Items(91, "Cosmo Memory"),
            new Items(92, "All Creation"),
            new Items(93, "Chaos"),
            new Items(94, "Highwind"),
            new Items(95 , "1/35 Soldier"),
            new Items(96 , "Super Sweeper"),
            new Items(97 , "Masamune Blade"),
            new Items(98 , "Save Crystal"),
            new Items(99 , "Combat Diary"),
            new Items(100, "Autograph"),
            new Items(101, "Gambler"),
            new Items(102, "Desert Rose"),
            new Items(103, "Earth Harp"),
            new Items(104, "Guide Book"),

            new CloudWeapons(128, "Buster Sword"),
            new CloudWeapons(129, "Mythril Saber"),
            new CloudWeapons(130, "Hardedge"),
            new CloudWeapons(131, "Butterfly Edge"),
            new CloudWeapons(132, "Enhance Sword"),
            new CloudWeapons(133, "Organics"),
            new CloudWeapons(134, "Crystal Sword"),
            new CloudWeapons(135, "Force Stealer"),
            new CloudWeapons(136, "Rune Blade"),
            new CloudWeapons(137, "Murasame"),
            new CloudWeapons(138, "Nail Bat"),
            new CloudWeapons(139, "Yoshiyuki"),
            new CloudWeapons(140, "Apocalypse"),
            new CloudWeapons(141, "Heaven's Cloud"),
            new CloudWeapons(142, "Ragnarok"),
            new CloudWeapons(143, "Ultima Weapon"),

            new TifaWeapons(144, "Leather Glove"),
            new TifaWeapons(145, "Metal Knuckle"),
            new TifaWeapons(146, "Mythril Claw"),
            new TifaWeapons(147, "Grand Glove"),
            new TifaWeapons(148, "Tiger Fang"),
            new TifaWeapons(149, "Diamond Knuckle"),
            new TifaWeapons(150, "Dragon Claw"),
            new TifaWeapons(151, "Crystal Glove"),
            new TifaWeapons(152, "Motor Drive"),
            new TifaWeapons(153, "Platinum Fist"),
            new TifaWeapons(154, "Kaiser Knuckle"),
            new TifaWeapons(155, "Work Glove"),
            new TifaWeapons(156, "Powersoul"),
            new TifaWeapons(157, "Master Fist"),
            new TifaWeapons(158, "God's Hand"),
            new TifaWeapons(159, "Premium Heart"),

            new BarretWeapons(160, "Gatling Gun"),
            new BarretWeapons(161, "Assault Gun"),
            new BarretWeapons(162, "Cannon Ball"),
            new BarretWeapons(163, "Atomic Scissors"),
            new BarretWeapons(164, "Heavy Vulcan"),
            new BarretWeapons(165, "Chainsaw"),
            new BarretWeapons(166, "Microlaser"),
            new BarretWeapons(167, "A* M Cannon"),
            new BarretWeapons(168, "W Machine Gun"),
            new BarretWeapons(169, "Drill ArM"),
            new BarretWeapons(170, "Solid Bazooka"),
            new BarretWeapons(171, "Rocket Punch"),
            new BarretWeapons(172, "Enemy Launcher"),
            new BarretWeapons(173, "Pile Banger"),
            new BarretWeapons(174, "Max Ray"),
            new BarretWeapons(175, "Missing Score"),

            new RedWeapons(176, "Mythril Clip"),
            new RedWeapons(177, "Diamond Pin"),
            new RedWeapons(178, "Silver Barrette"),
            new RedWeapons(179, "Gold Barrette"),
            new RedWeapons(180, "Adaman Clip"),
            new RedWeapons(181, "Crystal Comb"),
            new RedWeapons(182, "Magic Comb"),
            new RedWeapons(183, "Plus Barrette"),
            new RedWeapons(184, "Centclip"),
            new RedWeapons(185, "Hairpin"),
            new RedWeapons(186, "Seraph Comb"),
            new RedWeapons(187, "Behimoth Horn"),
            new RedWeapons(188, "Spring Gun Clip"),
            new RedWeapons(189, "Limited Moon"),

            new AerisWeapons(190, "Guard Stick"),
            new AerisWeapons(191, "Mythril Rod"),
            new AerisWeapons(192, "Full Metal Staff"),
            new AerisWeapons(193, "Striking Staff"),
            new AerisWeapons(194, "Prism Staff"),
            new AerisWeapons(195, "Aurora Rod"),
            new AerisWeapons(196, "Wizard Staff"),
            new AerisWeapons(197, "Wizer Staff"),
            new AerisWeapons(198, "Fairy Tale"),
            new AerisWeapons(199, "Umbrella"),
            new AerisWeapons(200, "Princess Guard"),
        };

        public static bool IsValid(int itemId) => Get(itemId) != null;

        public static Items Get(int itemId)
            => All.SingleOrDefault(x => x.Value == itemId);
    }

    public abstract class BaseWeapons : Items
    {
        private readonly ushort _weaponIdOffset;
        public ushort WeaponId => (ushort)(ItemId - _weaponIdOffset);

        protected BaseWeapons(ushort value, string name, ushort weaponIdOffset,
            ushort itemIdOffset = 0, params string[] words)
            : base(value, name, itemIdOffset, words)
        {
            _weaponIdOffset = weaponIdOffset;
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
}