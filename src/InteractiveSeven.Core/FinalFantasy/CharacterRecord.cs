using System.Runtime.InteropServices;
using InteractiveSeven.Core.Data;
using Tseng.Constants;

namespace Tseng.GameData
{
    [StructLayout(LayoutKind.Explicit, Size = 0x80)]
    public struct CharacterRecord
    {
        [FieldOffset(0x0)] public byte Id;
        [FieldOffset(0x1)] public byte Level;

        [FieldOffset(0x2)] public byte Strength;
        [FieldOffset(0x3)] public byte Vitality;
        [FieldOffset(0x4)] public byte Magic;
        [FieldOffset(0x5)] public byte Spirit;
        [FieldOffset(0x6)] public byte Dexterity;
        [FieldOffset(0x7)] public byte Luck;

        [FieldOffset(0x8)] public byte StrBonus;
        [FieldOffset(0x9)] public byte VitBonus;
        [FieldOffset(0xA)] public byte MagBonus;
        [FieldOffset(0xB)] public byte SprBonus;
        [FieldOffset(0xC)] public byte DexBonus;
        [FieldOffset(0xD)] public byte LucBonus;

        [FieldOffset(0xE)] public byte LimitLevel;
        [FieldOffset(0xF)] public byte LimitBar;

        [FieldOffset(0x10)] public string RawName;
        [FieldOffset(0x1C)] public byte Weapon;
        [FieldOffset(0x1D)] public byte Armor;
        [FieldOffset(0x1E)] public byte Accessory;
        [FieldOffset(0x1F)] public byte Flags;

        [FieldOffset(0x20)] public byte Row;
        [FieldOffset(0x21)] public byte LevelProgress;
        [FieldOffset(0x22)] public short LimitMask;
        [FieldOffset(0x24)] public short Kills;
        [FieldOffset(0x26)] public short LimitLevel1Uses;
        [FieldOffset(0x28)] public short LimitLevel2Uses;
        [FieldOffset(0x2A)] public short LimitLevel3Uses;
        [FieldOffset(0x2C)] public short CurrentHp;
        [FieldOffset(0x2E)] public short BaseHp;

        [FieldOffset(0x30)] public short CurrentMp;
        [FieldOffset(0x32)] public short BaseMp;
        [FieldOffset(0x38)] public short MaxHp;
        [FieldOffset(0x3A)] public short MaxMp;
        [FieldOffset(0x3C)] public int Experience;

        [FieldOffset(0x40)] public int WeaponMateria1;
        [FieldOffset(0x44)] public int WeaponMateria2;
        [FieldOffset(0x48)] public int WeaponMateria3;
        [FieldOffset(0x4C)] public int WeaponMateria4;
        [FieldOffset(0x50)] public int WeaponMateria5;
        [FieldOffset(0x54)] public int WeaponMateria6;
        [FieldOffset(0x58)] public int WeaponMateria7;
        [FieldOffset(0x5C)] public int WeaponMateria8;

        [FieldOffset(0x60)] public int ArmorMateria1;
        [FieldOffset(0x64)] public int ArmorMateria2;
        [FieldOffset(0x68)] public int ArmorMateria3;
        [FieldOffset(0x6C)] public int ArmorMateria4;
        [FieldOffset(0x70)] public int ArmorMateria5;
        [FieldOffset(0x74)] public int ArmorMateria6;
        [FieldOffset(0x78)] public int ArmorMateria7;
        [FieldOffset(0x7C)] public int ArmorMateria8;

        [FieldOffset(0x80)] public int ExpToLevel;

        public CharNames DefaultName => CharNames.GetById(Id);
        public bool AtFront => Row == FF7Const.Empty;
        public short[] LimitTimes => new []{ LimitLevel1Uses, LimitLevel2Uses, LimitLevel3Uses };
        public int[] ArmorMateria => new[]
        {
            ArmorMateria1,
            ArmorMateria2,
            ArmorMateria3,
            ArmorMateria4,
            ArmorMateria5,
            ArmorMateria6,
            ArmorMateria7,
            ArmorMateria8,
        };
        public int[] WeaponMateria => new[]
        {
            WeaponMateria1,
            WeaponMateria2,
            WeaponMateria3,
            WeaponMateria4,
            WeaponMateria5,
            WeaponMateria6,
            WeaponMateria7,
            WeaponMateria8,
        };

    }
}