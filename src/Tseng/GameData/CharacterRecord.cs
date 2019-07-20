namespace Tseng.GameData
{
    public struct CharacterRecord
    {
        #region Public Properties

        public byte Accessory { get; set; }
        public byte Armor { get; set; }
        public int[] ArmorMateria { get; set; }
        public bool AtFront { get; set; }
        public short BaseHp { get; set; }
        public short BaseMp { get; set; }
        public Character Character { get; set; }
        public short CurrentHp { get; set; }
        public short CurrentMp { get; set; }
        public byte DexBonus { get; set; }
        public byte Dexterity { get; set; }
        public int Experience { get; set; }

        public int ExpToLevel { get; set; }

        public byte Flags { get; set; }
        public byte Id { get; set; }
        public short Kills { get; set; }
        public byte Level { get; set; }

        public byte LevelProgress { get; set; }

        public byte LimitBar { get; set; }
        public byte LimitLevel { get; set; }

        public short LimitMask { get; set; }

        public short[] LimitTimes { get; set; }

        public byte LucBonus { get; set; }

        public byte Luck { get; set; }

        public byte MagBonus { get; set; }
        public byte Magic { get; set; }

        public short MaxHp { get; set; }

        public short MaxMp { get; set; }
        public string Name { get; set; }

        public byte Spirit { get; set; }

        public byte SprBonus { get; set; }

        public byte StrBonus { get; set; }

        public byte Strength { get; set; }

        public byte Vitality { get; set; }

        public byte VitBonus { get; set; }

        public byte Weapon { get; set; }

        public int[] WeaponMateria { get; set; }

        #endregion Public Properties
    }
}