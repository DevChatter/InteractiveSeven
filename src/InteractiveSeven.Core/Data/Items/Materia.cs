using System.Collections.Generic;
using System.Linq;

namespace InteractiveSeven.Core.Data.Items
{
    public class Materia
    {
        public byte Value { get; }
        public string Name { get; }
        public ushort DefaultPrice { get; set; }

        public Materia(byte value, string name, ushort defaultPrice)
        {
            Value = value;
            Name = name;
            DefaultPrice = defaultPrice;
        }

        public static IList<Materia> All = new[]
        {
            new Materia(0, "MP Plus", 200),
            new Materia(1, "HP Plus", 200),
            new Materia(2, "Speed Plus", 200),
            new Materia(3, "Magic Plus", 200),
            new Materia(4, "Luck Plus", 200),
            new Materia(5, "EXP Plus", 200),
            new Materia(6, "Gil Plus", 200),
            new Materia(7, "Enemy Away", 400),
            new Materia(8, "Enemy Lure", 100),
            new Materia(9, "Chocobo Lure", 100),
            new Materia(10, "Pre-Emptive", 200),
            new Materia(11, "Long Range", 400),
            new Materia(12, "Mega All", 500),
            new Materia(13, "Counter Attack", 200),
            new Materia(14, "Slash-All", 300),
            new Materia(15, "Double Cut", 200),
            new Materia(16, "Cover", 100),
            new Materia(17, "Underwater", 200),
            new Materia(18, "HP<->MP", 500),
            new Materia(19, "W-Magic", 500),
            new Materia(20, "W-Summon", 500),
            new Materia(21, "W-Item", 500),
            //new Materia(22, "Blank"),
            new Materia(23, "All", 200),
            new Materia(24, "Counter", 400),
            new Materia(25, "Magic Counter", 400),
            new Materia(26, "MP Turbo", 200),
            new Materia(27, "MP Absorb", 200),
            new Materia(28, "HP Absorb", 200),
            new Materia(29, "Elemental", 200),
            new Materia(30, "Added Effect", 400),
            new Materia(31, "Sneak Attack", 200),
            new Materia(32, "Final Attack", 400),
            new Materia(33, "Added Cut", 200),
            new Materia(34, "Steal as well", 300),
            new Materia(35, "Quadra Magic", 400),
            new Materia(36, "Steal", 300),
            new Materia(37, "Sense", 100),
            //new Materia(38, "Blank"),
            new Materia(39, "Throw", 200),
            new Materia(40, "Morph", 300),
            new Materia(41, "Deathblow", 300),
            new Materia(42, "Manipulate", 300),
            new Materia(43, "Mime", 300),
            new Materia(44, "Enemy Skill", 200),
            //new Materia(45, "Blank"),
            //new Materia(46, "Blank"),
            //new Materia(47, "Blank"),
            new Materia(48, "Master Command", 2000),
            new Materia(49, "Fire", 100),
            new Materia(50, "Ice", 100),
            new Materia(51, "Earth", 200),
            new Materia(52, "Lightning", 100),
            new Materia(53, "Restore", 100),
            new Materia(54, "Heal", 100),
            new Materia(55, "Revive", 200),
            new Materia(56, "Seal", 200),
            new Materia(57, "Mystify", 200),
            new Materia(58, "Transform", 200),
            new Materia(59, "Exit", 200),
            new Materia(60, "Poison", 300),
            new Materia(61, "Gravity", 200),
            new Materia(62, "Barrier", 200),
            //new Materia(63, "Blank"),
            new Materia(64, "Comet", 600),
            new Materia(65, "Time", 300),
            //new Materia(66, "Blank"),
            //new Materia(67, "Blank"),
            new Materia(68, "Destruct", 200),
            new Materia(69, "Contain", 200),
            new Materia(70, "Full Cure", 200),
            new Materia(71, "Shield", 200),
            new Materia(72, "Ultima", 1000),
            new Materia(73, "Master Magic", 2000),
            new Materia(74, "Choco/Mog", 500),
            new Materia(75, "Shiva", 500),
            new Materia(76, "Ifrit", 500),
            new Materia(77, "Ramuh", 500),
            new Materia(78, "Titan", 500),
            new Materia(79, "Odin", 500),
            new Materia(80, "Leviathan", 500),
            new Materia(81, "Bahamut", 500),
            new Materia(82, "Kujata", 500),
            new Materia(83, "Alexander", 500),
            new Materia(84, "Phoenix", 800),
            new Materia(85, "Neo Bahamut", 500),
            new Materia(86, "Hades", 700),
            new Materia(87, "Typhon", 800),
            new Materia(88, "Bahamut ZERO", 900),
            new Materia(89, "Knights of Round", 1000),
            new Materia(90, "Master Summon", 2000),
        };

        public static Materia Get(int materiaId)
            => All.SingleOrDefault(x => x.Value == materiaId);
    }
}