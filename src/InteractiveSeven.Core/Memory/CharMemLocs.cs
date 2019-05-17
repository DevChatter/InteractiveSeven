using System.Collections.Generic;

namespace InteractiveSeven.Core.Memory
{
    public class CharMemLoc
    {
        public MemLoc Name { get; }
        public MemLoc MaxHP { get; }
        public MemLoc CurHP { get; }
        public MemLoc Strength { get; }
        public MemLoc Vitality { get; }
        public MemLoc Magic { get; }
        public MemLoc Spirit { get; }
        public MemLoc Dexterity { get; }
        public MemLoc Luck { get; }

        private CharMemLoc(MemLoc name, MemLoc maxHp, MemLoc curHp, 
            MemLoc strength, MemLoc vitality, MemLoc magic,
            MemLoc spirit, MemLoc dexterity, MemLoc luck)
        {
            Name = name;
            MaxHP = maxHp;
            CurHP = curHp;
            Strength = strength;
            Vitality = vitality;
            Magic = magic;
            Spirit = spirit;
            Dexterity = dexterity;
            Luck = luck;
        }

        public static CharMemLoc ByName(string name)
        {
            return All[name];
        }

        public static CharMemLoc Cloud = new CharMemLoc(
            MemLoc.CloudName, MemLoc.CloudMaxHP, MemLoc.CloudCurHP,
            MemLoc.CloudStr, MemLoc.CloudVit, MemLoc.CloudMag,
            MemLoc.CloudSpi, MemLoc.CloudDex, MemLoc.CloudLuc);

        public static CharMemLoc Barret = new CharMemLoc(
            MemLoc.BarretName, MemLoc.BarretMaxHP, MemLoc.BarretCurHP,
            MemLoc.BarretStr, MemLoc.BarretVit, MemLoc.BarretMag,
            MemLoc.BarretSpi, MemLoc.BarretDex, MemLoc.BarretLuc);

        public static CharMemLoc Tifa = new CharMemLoc(
            MemLoc.TifaName, MemLoc.TifaMaxHP, MemLoc.TifaCurHP,
            MemLoc.TifaStr, MemLoc.TifaVit, MemLoc.TifaMag,
            MemLoc.TifaSpi, MemLoc.TifaDex, MemLoc.TifaLuc);

        private static readonly Dictionary<string, CharMemLoc> All
            = new Dictionary<string, CharMemLoc>
        {
            [Constants.Cloud] = Cloud,
            [Constants.Barret] = Barret,
            [Constants.Tifa] = Tifa,
        };
    }
}