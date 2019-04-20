using System.Collections.Generic;

namespace InteractiveSeven.Core.Memory
{
    public  class CharMemLoc
    {
        public MemLoc MaxHP { get; set; }
        public MemLoc CurHP { get; set; }
        public MemLoc Strength { get; set; }
        public MemLoc Vitality { get; set; }
        public MemLoc Magic { get; set; }
        public MemLoc Spirit { get; set; }
        public MemLoc Dexterity { get; set; }
        public MemLoc Luck { get; set; }

        private CharMemLoc(MemLoc maxHp, MemLoc curHp, 
            MemLoc strength, MemLoc vitality, MemLoc magic,
            MemLoc spirit, MemLoc dexterity, MemLoc luck)
        {
            MaxHP = maxHp;
            CurHP = curHp;
            Strength = strength;
            Vitality = vitality;
            Magic = magic;
            Spirit = spirit;
            Dexterity = dexterity;
            Luck = luck;
        }

        public CharMemLoc ByName(string name)
        {
            return _all[name];
        }

        public static CharMemLoc Cloud = new CharMemLoc(
            MemLoc.CloudMaxHP, MemLoc.CloudCurHP,
            MemLoc.CloudStr, MemLoc.CloudVit, MemLoc.CloudMag,
            MemLoc.CloudSpi, MemLoc.CloudDex, MemLoc.CloudLuc);

        public static CharMemLoc Barret = new CharMemLoc(
            MemLoc.BarretMaxHP, MemLoc.BarretCurHP,
            MemLoc.BarretStr, MemLoc.BarretVit, MemLoc.BarretMag,
            MemLoc.BarretSpi, MemLoc.BarretDex, MemLoc.BarretLuc);

        public static CharMemLoc Tifa = new CharMemLoc(
            MemLoc.TifaMaxHP, MemLoc.TifaCurHP,
            MemLoc.TifaStr, MemLoc.TifaVit, MemLoc.TifaMag,
            MemLoc.TifaSpi, MemLoc.TifaDex, MemLoc.TifaLuc);

        private static Dictionary<string, CharMemLoc> _all = new Dictionary<string, CharMemLoc>
        {
            [nameof(Cloud)] = Cloud,
            [nameof(Barret)] = Barret,
            [nameof(Tifa)] = Tifa,
        };
    }
}