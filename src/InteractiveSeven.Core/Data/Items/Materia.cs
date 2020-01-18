using System.Collections.Generic;
using System.Linq;

namespace InteractiveSeven.Core.Data.Items
{
    public class Materia
    {
        public byte Value { get; }
        public string Name { get; }

        public Materia(byte value, string name)
        {
            Value = value;
            Name = name;
        }

        public static IList<Materia> All = new[]
        {
            new Materia(0 , "MP Plus"),
            new Materia(1 , "HP Plus"),
            new Materia(2 , "Speed Plus"),
            new Materia(3 , "Magic Plus"),
            new Materia(4 , "Luck Plus"),
            new Materia(5 , "EXP Plus"),
            new Materia(6 , "Gil Plus"),
            new Materia(7 , "Enemy Away"),
            new Materia(8 , "Enemy Lure"),
            new Materia(9 , "Chocobo Lure"),
            new Materia(10, "Pre-Emptive"),
            new Materia(11, "Long Range"),
            new Materia(12, "Mega All"),
            new Materia(13, "Counter Attack"),
            new Materia(14, "Slash-All"),
            new Materia(15, "Double Cut"),
            new Materia(16, "Cover"),
            new Materia(17, "Underwater"),
            new Materia(18, "HP<->MP"),
            new Materia(19, "W-Magic"),
            new Materia(20, "W-Summon"),
            new Materia(21, "W-Item"),
            //new Materia(22, "Blank"),
            new Materia(23, "All"),
            new Materia(24, "Counter"),
            new Materia(25, "Magic Counter"),
            new Materia(26, "MP Turbo"),
            new Materia(27, "MP Absorb"),
            new Materia(28, "HP Absorb"),
            new Materia(29, "Elemental"),
            new Materia(30, "Added Effect"),
            new Materia(31, "Sneak Attack"),
            new Materia(32, "Final Attack"),
            new Materia(33, "Added Cut"),
            new Materia(34, "Steal as well"),
            new Materia(35, "Quadra Magic"),
            new Materia(36, "Steal"),
            new Materia(37, "Sense"),
            //new Materia(38, "Blank"),
            new Materia(39, "Throw"),
            new Materia(40, "Morph"),
            new Materia(41, "Deathblow"),
            new Materia(42, "Manipulate"),
            new Materia(43, "Mime"),
            new Materia(44, "Enemy Skill"),
            //new Materia(45, "Blank"),
            //new Materia(46, "Blank"),
            //new Materia(47, "Blank"),
            new Materia(48, "Master Command"),
            new Materia(49, "Fire"),
            new Materia(50, "Ice"),
            new Materia(51, "Earth"),
            new Materia(52, "Lightning"),
            new Materia(53, "Restore"),
            new Materia(54, "Heal"),
            new Materia(55, "Revive"),
            new Materia(56, "Seal"),
            new Materia(57, "Mystify"),
            new Materia(58, "Transform"),
            new Materia(59, "Exit"),
            new Materia(60, "Poison"),
            new Materia(61, "Gravity"),
            new Materia(62, "Barrier"),
            //new Materia(63, "Blank"),
            new Materia(64, "Comet"),
            new Materia(65, "Time"),
            //new Materia(66, "Blank"),
            //new Materia(67, "Blank"),
            new Materia(68, "Destruct"),
            new Materia(69, "Contain"),
            new Materia(70, "Full Cure"),
            new Materia(71, "Shield"),
            new Materia(72, "Ultima"),
            new Materia(73, "Master Magic"),
            new Materia(74, "Choco/Mog"),
            new Materia(75, "Shiva"),
            new Materia(76, "Ifrit"),
            new Materia(77, "Ramuh"),
            new Materia(78, "Titan"),
            new Materia(79, "Odin"),
            new Materia(80, "Leviathan"),
            new Materia(81, "Bahamut"),
            new Materia(82, "Kujata"),
            new Materia(83, "Alexander"),
            new Materia(84, "Phoenix"),
            new Materia(85, "Neo Bahamut"),
            new Materia(86, "Hades"),
            new Materia(87, "Typhon"),
            new Materia(88, "Bahamut ZERO"),
            new Materia(89, "Knights of Round"),
            new Materia(90, "Master Summon"),
        };

        public static Materia Get(int materiaId)
            => All.SingleOrDefault(x => x.Value == materiaId);
    }
}