using System.Collections.Generic;
using System.Linq;

namespace InteractiveSeven.Core.Items
{
    public class Items
    {
        public string Name { get; }
        public int Id { get; }
        public int Value { get; }

        private Items(int id, int value, string name)
        {
            Name = name;
            Id = id;
            Value = value;
        }

        public static IList<Items> All = new[]
        {
            new Items(1, 0, "Potion"),
            new Items(2, 1, "Hi-Potion"),
            new Items(3, 2, "X-Potion"),
            new Items(4, 3, "Ether"),
            new Items(5, 4, "Turbo Ether"),
            new Items(6, 5, "Elixir"),
            new Items(7, 6, "Megalixir"),
            new Items(8, 7, "Phoenix Down"),
            new Items(9, 8, "Antidote"),
            new Items(10, 9, "Soft"),
            new Items(11, 10, "Maiden's Kiss"),
            new Items(12, 11, "Cornucopia"),
            new Items(13, 12, "Echo Screen"),
            new Items(14, 13, "Hyper"),
            new Items(15, 14, "Tranquilizer"),
            new Items(16, 15, "Remedy"),
            new Items(17, 16, "Smoke Bomb"),
            new Items(18, 17, "Speed Drink"),
            new Items(19, 18, "Hero Drink"),
            new Items(20, 19, "Vaccine"),
            new Items(21, 20, "Grenade"),
            new Items(22, 21, "Shrapnel"),
            new Items(23, 22, "Right arm"),
            new Items(24, 23, "Hourglass"),
            new Items(25, 24, "Kiss of Death"),
            new Items(27, 25, "Spider Web"),
            new Items(28, 27, "Mute Mask"),
            new Items(29, 28, "War Gong"),
            new Items(30, 29, "Loco weed"),
            new Items(31, 30, "Fire Fang"),
            new Items(32, 31, "Fire Veil"),
            new Items(33, 32, "Antarctic Wind"),
            new Items(34, 33, "Ice Crystal"),
            new Items(35, 34, "Bolt Plume"),
            new Items(36, 35, "Swift Bolt"),
            new Items(37, 36, "Earth Drum"),
            new Items(38, 37, "Earth Mallet"),
            new Items(39, 38, "Deadly Waste"),
            new Items(40, 39, "M-Tentacles"),
            new Items(41, 40, "Stardust"),
            new Items(42, 41, "Vampire Fang"),
            new Items(43, 42, "Ghost Hand"),
            new Items(44, 43, "Vagyrisk Claw"),
            new Items(45, 44, "Light Curtain"),
            new Items(46, 45, "Lunar Curtain"),
            new Items(47, 46, "Mirror"),
            new Items(48, 47, "Holy Torch"),
            new Items(49, 48, "Bird Wing"),
            new Items(50, 49, "Dragon Scales"),
            new Items(51, 50, "Impaler"),
            new Items(52, 51, "Shrivel"),
            new Items(53, 52, "Eye drop"),
            new Items(54, 53, "Molotov"),
            new Items(55, 54, "S-mine"),
            new Items(56, 55, "8inch Cannon"),
            new Items(57, 56, "Graviball"),
            new Items(58, 57, "T/S Bomb"),
            new Items(59, 58, "Ink"),
            new Items(60, 59, "Dazers"),
            new Items(61, 60, "Dragon Fang"),
            new Items(62, 61, "Cauldron"),
            new Items(63, 62, "Sylkis Greens"),
            new Items(64, 63, "Reagan Greens"),
            new Items(65, 64, "Mimett Greens"),
            new Items(66, 65, "Curiel Greens"),
            new Items(67, 66, "Pahsana Greens"),
            new Items(68, 67, "Tantal Greens"),
            new Items(69, 68, "Krakka Greens"),
            new Items(70, 70, "Tent"),
            new Items(71, 71, "Power Source"),
            new Items(72, 72, "Guard Source"),
            new Items(73, 73, "Magic Source"),
            new Items(74, 74, "Mind Source"),
            new Items(75, 75, "Speed Source"),
            new Items(76, 76, "Luck Source"),
            new Items(77, 77, "Zeio Nut"),
            new Items(78, 78, "Carob Nut"),
            new Items(79, 79, "Porov Nut"),
            new Items(80, 80, "Pram Nut"),
            new Items(81, 81, "Lasan Nut"),
            new Items(82, 82, "Saraha Nut"),
            new Items(83, 83, "Luchile Nut"),
            new Items(84, 84, "Pepio Nut"),
            new Items(85, 85, "Battery"),
            new Items(86, 86, "Tissue"),
        };

        public static bool IsValid(int armletId) => armletId > 0 && armletId < 87;

        public static Items Get(int armletId)
            => All.SingleOrDefault(x => x.Id == armletId);
    }
}