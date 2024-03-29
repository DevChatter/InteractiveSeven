﻿using System.Collections.Generic;
using System.Linq;

namespace InteractiveSeven.Core.Data.Items
{
    public class Items
    {
        public string Name { get; }
        public ushort Id { get; }
        public string[] Words { get; set; }
        public ushort ItemId { get; }
        public ushort DefaultPrice { get; }
        public bool Enabled { get; }

        protected Items(ushort id, string name, ushort defaultPrice, bool enabled, ushort typeOffset = 0,
            params string[] words)
        {
            DefaultPrice = defaultPrice;
            Enabled = enabled;
            Name = name;
            Id = id;
            ItemId = (ushort)(id + typeOffset);
            Words = words ?? new string[0];
        }

        public static IList<Items> All = new[]
        {
            new Items(0, "Potion", 25, true),
            new Items(1, "Hi-Potion", 100, true),
            new Items(2, "X-Potion", 250, true),
            new Items(3, "Ether", 100, true),
            new Items(4, "Turbo Ether", 250, true),
            new Items(5, "Elixir", 500, true),
            new Items(6, "Megalixir", 1000, true),
            new Items(7, "Phoenix Down", 100, true),
            new Items(8, "Antidote", 25, true),
            new Items(9, "Soft", 50, true),
            new Items(10, "Maiden's Kiss", 50, true),
            new Items(11, "Cornucopia", 50, true),
            new Items(12, "Echo Screen", 25, true),
            new Items(13, "Hyper", 50, true),
            new Items(14, "Tranquilizer", 50, true),
            new Items(15, "Remedy", 250, true),
            new Items(16, "Smoke Bomb", 100, true),
            new Items(17, "Speed Drink", 100, true),
            new Items(18, "Hero Drink", 100, true),
            new Items(19, "Vaccine", 100, true),
            new Items(20, "Grenade", 25, true),
            new Items(21, "Shrapnel", 200, true),
            new Items(22, "Right arm", 400, true),
            new Items(23, "Hourglass", 250, true),
            new Items(24, "Kiss of Death", 500, true),
            new Items(25, "Spider Web", 250, true),
            new Items(27, "Mute Mask", 250, true),
            new Items(28, "War Gong", 250, true),
            new Items(29, "Loco weed", 250, true),
            new Items(30, "Fire Fang", 200, true),
            new Items(31, "Fire Veil", 400, true),
            new Items(32, "Antarctic Wind", 200, true),
            new Items(33, "Ice Crystal", 400, true),
            new Items(34, "Bolt Plume", 200, true),
            new Items(35, "Swift Bolt", 400, true),
            new Items(36, "Earth Drum", 200, true),
            new Items(37, "Earth Mallet", 400, true),
            new Items(38, "Deadly Waste", 200, true),
            new Items(39, "M-Tentacles", 400, true),
            new Items(40, "Stardust", 400, true),
            new Items(41, "Vampire Fang", 100, true),
            new Items(42, "Ghost Hand", 100, true),
            new Items(43, "Vagyrisk Claw", 100, true),
            new Items(44, "Light Curtain", 100, true),
            new Items(45, "Lunar Curtain", 100, true),
            new Items(46, "Mirror", 100, true),
            new Items(47, "Holy Torch", 200, true),
            new Items(48, "Bird Wing", 200, true),
            new Items(49, "Dragon Scales", 300, true),
            new Items(50, "Impaler", 150, true),
            new Items(51, "Shrivel", 150, true),
            new Items(52, "Eye drop", 25, true),
            new Items(53, "Molotov", 150, true),
            new Items(54, "S-mine", 200, true),
            new Items(55, "8inch Cannon", 300, true),
            new Items(56, "Graviball", 100, true),
            new Items(57, "T/S Bomb", 200, true),
            new Items(58, "Ink", 10, true),
            new Items(59, "Dazers", 200, true),
            new Items(60, "Dragon Fang", 500, true),
            new Items(61, "Cauldron", 500, true),
            new Items(62, "Sylkis Greens", 500, true),
            new Items(63, "Reagan Greens", 250, true),
            new Items(64, "Mimett Greens", 250, true),
            new Items(65, "Curiel Greens", 150, true),
            new Items(66, "Pahsana Greens", 100, true),
            new Items(67, "Tantal Greens", 100, true),
            new Items(68, "Krakka Greens", 75, true),
            new Items(70, "Tent", 200, true),
            new Items(71, "Power Source", 1500, false),
            new Items(72, "Guard Source", 1500, false),
            new Items(73, "Magic Source", 1500, false),
            new Items(74, "Mind Source", 1500, false),
            new Items(75, "Speed Source", 1500, false),
            new Items(76, "Luck Source", 1500, false),
            new Items(77, "Zeio Nut", 1000, true),
            new Items(78, "Carob Nut", 500, true),
            new Items(79, "Porov Nut", 100, true),
            new Items(80, "Pram Nut", 100, true),
            new Items(81, "Lasan Nut", 100, true),
            new Items(82, "Saraha Nut", 100, true),
            new Items(83, "Luchile Nut", 100, true),
            new Items(84, "Pepio Nut", 100, true),
            new Items(85, "Battery", ushort.MaxValue, false),
            new Items(86, "Tissue", 10, true),
            new Items(87, "Omnislash", 2500, false),
            new Items(88, "Catastrophe", 2500, false),
            new Items(89, "Final Heaven", 2500, false),
            new Items(90, "Great Gospel", 2500, false),
            new Items(91, "Cosmo Memory", 2500, false),
            new Items(92, "All Creation", 2500, false),
            new Items(93, "Chaos", 2500, false),
            new Items(94, "Highwind", 2500, false),
            new Items(95, "1/35 Soldier", ushort.MaxValue, false),
            new Items(96, "Super Sweeper", ushort.MaxValue, false),
            new Items(97, "Masamune Blade", ushort.MaxValue, false),
            new Items(98, "Save Crystal", ushort.MaxValue, false),
            new Items(99, "Combat Diary", ushort.MaxValue, false),
            new Items(100, "Autograph", ushort.MaxValue, false),
            new Items(101, "Gambler", ushort.MaxValue, false),
            new Items(102, "Desert Rose", ushort.MaxValue, false),
            new Items(103, "Earth Harp", ushort.MaxValue, false),
            new Items(104, "Guide Book", 1000, false),

            new CloudWeapon(0, "Buster Sword", 50),
            new CloudWeapon(1, "Mythril Saber", 100),
            new CloudWeapon(2, "Hardedge", 150),
            new CloudWeapon(3, "Butterfly Edge", 200),
            new CloudWeapon(4, "Enhance Sword", 250),
            new CloudWeapon(5, "Organics", 300),
            new CloudWeapon(6, "Crystal Sword", 400),
            new CloudWeapon(7, "Force Stealer", 450),
            new CloudWeapon(8, "Rune Blade", 500),
            new CloudWeapon(9, "Murasame", 550),
            new CloudWeapon(10, "Nail Bat", 600),
            new CloudWeapon(11, "Yoshiyuki", 650),
            new CloudWeapon(12, "Apocalypse", 700),
            new CloudWeapon(13, "Heaven's Cloud", 750),
            new CloudWeapon(14, "Ragnarok", 800),
            new CloudWeapon(15, "Ultima Weapon", 1000),

            new TifaWeapon(0, "Leather Glove", 50),
            new TifaWeapon(1, "Metal Knuckle", 100),
            new TifaWeapon(2, "Mythril Claw", 150),
            new TifaWeapon(3, "Grand Glove", 200),
            new TifaWeapon(4, "Tiger Fang", 250),
            new TifaWeapon(5, "Diamond Knuckle", 300),
            new TifaWeapon(6, "Dragon Claw", 400),
            new TifaWeapon(7, "Crystal Glove", 450),
            new TifaWeapon(8, "Motor Drive", 500),
            new TifaWeapon(9, "Platinum Fist", 550),
            new TifaWeapon(10, "Kaiser Knuckle", 600),
            new TifaWeapon(11, "Work Glove", 650),
            new TifaWeapon(12, "Powersoul", 700),
            new TifaWeapon(13, "Master Fist", 750),
            new TifaWeapon(14, "God's Hand", 800),
            new TifaWeapon(15, "Premium Heart", 1000),

            new BarretWeapon(0, "Gatling Gun", 50),
            new BarretWeapon(1, "Assault Gun", 100),
            new BarretWeapon(2, "Cannon Ball", 150),
            new BarretWeapon(3, "Atomic Scissors", 200),
            new BarretWeapon(4, "Heavy Vulcan", 250),
            new BarretWeapon(5, "Chainsaw", 300),
            new BarretWeapon(6, "Microlaser", 400),
            new BarretWeapon(7, "A* M Cannon", 450),
            new BarretWeapon(8, "W Machine Gun", 500),
            new BarretWeapon(9, "Drill ArM", 550),
            new BarretWeapon(10, "Solid Bazooka", 600),
            new BarretWeapon(11, "Rocket Punch", 650),
            new BarretWeapon(12, "Enemy Launcher", 700),
            new BarretWeapon(13, "Pile Banger", 750),
            new BarretWeapon(14, "Max Ray", 800),
            new BarretWeapon(15, "Missing Score", 1000),

            new RedWeapon(0, "Mythril Clip", 50),
            new RedWeapon(1, "Diamond Pin", 100),
            new RedWeapon(2, "Silver Barrette", 150),
            new RedWeapon(3, "Gold Barrette", 200),
            new RedWeapon(4, "Adaman Clip", 250),
            new RedWeapon(5, "Crystal Comb", 300),
            new RedWeapon(6, "Magic Comb", 350),
            new RedWeapon(7, "Plus Barrette", 400),
            new RedWeapon(8, "Centclip", 450),
            new RedWeapon(9, "Hairpin", 500),
            new RedWeapon(10, "Seraph Comb", 600),
            new RedWeapon(11, "Behimoth Horn", 700),
            new RedWeapon(12, "Spring Gun Clip", 800),
            new RedWeapon(13, "Limited Moon", 900),

            new AerisWeapon(0, "Guard Stick", 50),
            new AerisWeapon(1, "Mythril Rod", 100),
            new AerisWeapon(2, "Full Metal Staff", 150),
            new AerisWeapon(3, "Striking Staff", 200),
            new AerisWeapon(4, "Prism Staff", 250),
            new AerisWeapon(5, "Aurora Rod", 300),
            new AerisWeapon(6, "Wizard Staff", 350),
            new AerisWeapon(7, "Wizer Staff", 400),
            new AerisWeapon(8, "Fairy Tale", 450),
            new AerisWeapon(9, "Umbrella", 500),
            new AerisWeapon(10, "Princess Guard", 750),

            new CidWeapon(0, "Spear", 50),
            new CidWeapon(1, "Slash Lance", 100),
            new CidWeapon(2, "Trident", 150),
            new CidWeapon(3, "Mast Axe", 200),
            new CidWeapon(4, "Partisan", 250),
            new CidWeapon(5, "Viper Halberd", 300),
            new CidWeapon(6, "Javelin", 350),
            new CidWeapon(7, "Grow Lance", 400),
            new CidWeapon(8, "Mop", 450),
            new CidWeapon(9, "Dragoon Lance", 500),
            new CidWeapon(10, "Scimitar", 600),
            new CidWeapon(11, "Flayer", 700),
            new CidWeapon(12, "Spirit Lance", 800),
            new CidWeapon(13, "Venus Gospel", 900),

            new YuffieWeapon(0, "4-point Sherukin", 50),
            new YuffieWeapon(1, "Boomerang", 100),
            new YuffieWeapon(2, "Pinwheel", 150),
            new YuffieWeapon(3, "Razor Ring", 200),
            new YuffieWeapon(4, "Hawkeye", 250),
            new YuffieWeapon(5, "Crystal Cross", 300),
            new YuffieWeapon(6, "Wind Slash", 350),
            new YuffieWeapon(7, "Twin Viper", 400),
            new YuffieWeapon(8, "Spiral Shuriken", 450),
            new YuffieWeapon(9, "Superball", 500),
            new YuffieWeapon(10, "Magic Shuriken", 600),
            new YuffieWeapon(11, "Rising Sun", 700),
            new YuffieWeapon(12, "Oritsuru", 800),
            new YuffieWeapon(13, "Conformer", 1000),

            new CaitSithWeapon(0, "Yellow M-phone", 50),
            new CaitSithWeapon(1, "Green M-phone", 100),
            new CaitSithWeapon(2, "Blue M-phone", 150),
            new CaitSithWeapon(3, "Red M-phone", 200),
            new CaitSithWeapon(4, "Crystal M-phone", 250),
            new CaitSithWeapon(5, "White M-phone", 300),
            new CaitSithWeapon(6, "Black M-phone", 350),
            new CaitSithWeapon(7, "Silver M-phone", 400),
            new CaitSithWeapon(8, "Trumpet Shell", 450),
            new CaitSithWeapon(9, "Gold M-phone", 500),
            new CaitSithWeapon(10, "Battle Trumpet", 600),
            new CaitSithWeapon(11, "Starlight Phone", 700),
            new CaitSithWeapon(12, "HP Shout", 800),

            new VincentWeapon(0, "Quicksilver", 50),
            new VincentWeapon(1, "Shotgun", 100),
            new VincentWeapon(2, "Shortbarrel", 150),
            new VincentWeapon(3, "Lariat", 200),
            new VincentWeapon(4, "Winchester", 250),
            new VincentWeapon(5, "Peacemaker", 300),
            new VincentWeapon(6, "Buntline", 350),
            new VincentWeapon(7, "Long Barrel R", 400),
            new VincentWeapon(8, "Silver Rifle", 450),
            new VincentWeapon(9, "Sniper CR", 500),
            new VincentWeapon(10, "Supershot ST", 600),
            new VincentWeapon(11, "Outsider", 700),
            new VincentWeapon(12, "Death Penalty", 800),

            new SephirothWeapon(0, "Masamune"),

            new Armlet(0, "Bronze Bangle", 50),
            new Armlet(1, "Iron Bangle", 75),
            new Armlet(2, "Titan Bangle", 100),
            new Armlet(3, "Mythril Armlet", 125),
            new Armlet(4, "Carbon Bangle", 150),
            new Armlet(5, "Silver Armlet", 175),
            new Armlet(6, "Gold Armlet", 200),
            new Armlet(7, "Diamond Bangle", 225),
            new Armlet(8, "Crystal Bangle", 250),
            new Armlet(9, "Platinum Bangle", 275),
            new Armlet(10, "Rune Armlet", 300),
            new Armlet(11, "Edincoat", 325),
            new Armlet(12, "Wizard Bracelet", 350),
            new Armlet(13, "Adaman Bangle", 375),
            new Armlet(14, "Gigas Armlet", 400),
            new Armlet(15, "Imperial Guard", 425),
            new Armlet(16, "Aegis Armlet", 450),
            new Armlet(17, "Fourth Bracelet", 475),
            new Armlet(18, "Warrior Bangle", 500),
            new Armlet(19, "Shinra Beta", 525),
            new Armlet(20, "Shinra Alpha", 550),
            new Armlet(21, "Four Slots", 575),
            new Armlet(22, "Fire Armlet", 600),
            new Armlet(23, "Aurora Armlet", 625),
            new Armlet(24, "Bolt Armlet", 650),
            new Armlet(25, "Dragon Armlet", 675),
            new Armlet(26, "Minerva Band", 700),
            new Armlet(27, "Escort Guard", 750),
            new Armlet(28, "Mystile", 800),
            new Armlet(29, "Ziedrich", 800),
            new Armlet(30, "Precious Watch", 800),
            new Armlet(31, "Chocobracelet", 800),

            new Accessory(0, "Power Wrist", 100),
            new Accessory(1, "Protect Vest", 100),
            new Accessory(2, "Earring", 100),
            new Accessory(3, "Talisman", 100),
            new Accessory(4, "Choco Feather", 100),
            new Accessory(5, "Amulet", 100),
            new Accessory(6, "Champion Belt", 100),
            new Accessory(7, "Poison Ring", 100),
            new Accessory(8, "Touph Ring", 100),
            new Accessory(9, "Circlet", 100),
            new Accessory(10, "Star Pendant", 200),
            new Accessory(11, "Silver Glasses", 200),
            new Accessory(12, "Headband", 200),
            new Accessory(13, "Fairy Ring", 200),
            new Accessory(14, "Jem Ring", 200),
            new Accessory(15, "White Cape", 200),
            new Accessory(16, "Sprint Shoes", 200),
            new Accessory(17, "Peace Ring", 200),
            new Accessory(18, "Ribbon", 1000, false),
            new Accessory(19, "Fire Ring", 200),
            new Accessory(20, "Ice Ring", 200),
            new Accessory(21, "Bolt Ring", 200),
            new Accessory(22, "Tetra Elemental", 400),
            new Accessory(23, "Safety Bit", 200),
            new Accessory(24, "Fury Ring", 200),
            new Accessory(25, "Curse Ring", 500),
            new Accessory(26, "Protect Ring", 200),
            new Accessory(27, "Cat's Bell", 200),
            new Accessory(28, "Reflect Ring", 200),
            new Accessory(29, "Water Ring", 200),
            new Accessory(30, "Sneak Glove", 200),
            new Accessory(31, "HypnoCrown", 200),
        };

        public static Items GetByItemId(int itemId)
            => All.SingleOrDefault(x => x.ItemId == itemId);

        public bool IsMatchByName(string name) => Name.NoSpaces().StartsWithIns(name) || Name.StartsWithIns(name);
    }
}
