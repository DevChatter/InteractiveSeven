using System.Runtime.InteropServices;
using InteractiveSeven.Core.Data;
using InteractiveSeven.Core.FinalFantasy.Constants;
using Shojy.FF7.Elena.Extensions;

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

        [MarshalAs(UnmanagedType.ByValArray, ArraySubType = UnmanagedType.U1, SizeConst = 12)]
        [FieldOffset(0x10)] public byte[] RawName;
        [FieldOffset(0x1C)] public byte Weapon;
        [FieldOffset(0x1D)] public byte Armor;
        [FieldOffset(0x1E)] public byte Accessory;
        [FieldOffset(0x1F)] public byte Flags;

        [FieldOffset(0x20)] public byte Row;
        [FieldOffset(0x21)] public byte LevelProgress;
        [FieldOffset(0x22)] public ushort LimitMask;
        [FieldOffset(0x24)] public ushort Kills;
        [FieldOffset(0x26)] public ushort LimitLevel1Uses;
        [FieldOffset(0x28)] public ushort LimitLevel2Uses;
        [FieldOffset(0x2A)] public ushort LimitLevel3Uses;
        [FieldOffset(0x2C)] public ushort CurrentHp;
        [FieldOffset(0x2E)] public ushort BaseHp;

        [FieldOffset(0x30)] public ushort CurrentMp;
        [FieldOffset(0x32)] public ushort BaseMp;
        [FieldOffset(0x38)] public ushort MaxHp;
        [FieldOffset(0x3A)] public ushort MaxMp;
        [FieldOffset(0x3C)] public int Experience;

        [FieldOffset(0x40)] public MateriaRecord WeaponMateria1;
        [FieldOffset(0x44)] public MateriaRecord WeaponMateria2;
        [FieldOffset(0x48)] public MateriaRecord WeaponMateria3;
        [FieldOffset(0x4C)] public MateriaRecord WeaponMateria4;
        [FieldOffset(0x50)] public MateriaRecord WeaponMateria5;
        [FieldOffset(0x54)] public MateriaRecord WeaponMateria6;
        [FieldOffset(0x58)] public MateriaRecord WeaponMateria7;
        [FieldOffset(0x5C)] public MateriaRecord WeaponMateria8;

        [FieldOffset(0x60)] public MateriaRecord ArmorMateria1;
        [FieldOffset(0x64)] public MateriaRecord ArmorMateria2;
        [FieldOffset(0x68)] public MateriaRecord ArmorMateria3;
        [FieldOffset(0x6C)] public MateriaRecord ArmorMateria4;
        [FieldOffset(0x70)] public MateriaRecord ArmorMateria5;
        [FieldOffset(0x74)] public MateriaRecord ArmorMateria6;
        [FieldOffset(0x78)] public MateriaRecord ArmorMateria7;
        [FieldOffset(0x7C)] public MateriaRecord ArmorMateria8;

        [FieldOffset(0x80)] public uint ExpToLevel;

        public CharNames DefaultName => CharNames.GetById(Id);
        public bool AtFront => Row == FF7Const.Empty;
        public string Name => RawName?.ToFFString() ?? "";
        public ushort[] LimitTimes => new[] { LimitLevel1Uses, LimitLevel2Uses, LimitLevel3Uses };
        public MateriaRecord[] ArmorMateria => new[]
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
        public MateriaRecord[] WeaponMateria => new[]
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

    [StructLayout(LayoutKind.Explicit, Size = 4)]
    public struct MateriaRecord
    {
        [FieldOffset(0)] public byte Id;
        [FieldOffset(1)] public byte ApByte1;
        [FieldOffset(2)] public byte ApByte2;
        [FieldOffset(3)] public byte ApByte3;
        public uint Experience => (uint)((ApByte3 << 16) + (ApByte2 << 8) + ApByte1);
    }
}
