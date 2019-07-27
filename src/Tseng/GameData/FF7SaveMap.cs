using Shojy.FF7.Elena.Extensions;
using System;
using Tseng.Constants;

namespace Tseng.GameData
{
    public class FF7SaveMap
    {
        #region Private Fields

        private readonly byte[] _map;

        #endregion Private Fields

        #region Public Constructors

        public FF7SaveMap(byte[] map)
        {
            // Not much else to do here. Checking validity of the response won't be useful in a constructor since
            // we can't get "out" of it, though the caller of our constructor can check .IsValid
            var valid = VerifyMapIntegrity(map);

            IsValid = valid;
            _map = !valid ? null : map;
        }

        #endregion Public Constructors

        #region Public Properties

        public short BattlePoints => BitConverter.ToInt16(_map, SaveMapOffsets.BattlePoints);
        public short BattlesFought => BitConverter.ToInt16(_map, SaveMapOffsets.BattlesFought);
        public int CountDownTimer => BitConverter.ToInt32(_map, SaveMapOffsets.CountdownTime);
        public byte Direction => _map[0xBA0];
        public byte Disc => _map[SaveMapOffsets.GameDisc];
        public short Escapes => BitConverter.ToInt16(_map, SaveMapOffsets.BattlesEscaped);

        public int FieldTotalSeconds
        {
            get
            {
                int seconds = _map[SaveMapOffsets.PlayTimeSeconds];
                seconds += _map[SaveMapOffsets.PlayTimeMinutes] * 60;
                seconds += _map[SaveMapOffsets.PlayTimeHours] * 3600;
                return seconds;
            }
        }

        public byte[] LiveCharIDs
        {
            get
            {
                var partyIds = new byte[3];
                partyIds[0] = _map[SaveMapOffsets.PartyMember1];
                partyIds[1] = _map[SaveMapOffsets.PartyMember2];
                partyIds[2] = _map[SaveMapOffsets.PartyMember3];
                return partyIds;
            }
        }

        public int LiveGil => BitConverter.ToInt32(_map, SaveMapOffsets.Gil);

        public string LiveMapName
        {
            get
            {
                var mapNameBytes = new byte[32];
                Array.Copy(_map, SaveMapOffsets.CurrentMapName, mapNameBytes, 0, 32);
                return mapNameBytes.ToFFString();
            }
        }

        public CharacterRecord[] LiveParty
        {
            get
            {
                var resultArray = new CharacterRecord[3];
                if (FillChar((Character)_map[SaveMapOffsets.PartyMember1], ref resultArray[0], _map) == false)
                {
                    resultArray[0] = new CharacterRecord { Id = 0xFF };
                }

                if (FillChar((Character)_map[SaveMapOffsets.PartyMember2], ref resultArray[1], _map) == false)
                {
                    resultArray[1] = new CharacterRecord { Id = 0xFF };
                }

                if (FillChar((Character)_map[SaveMapOffsets.PartyMember3], ref resultArray[2], _map) == false)
                {
                    resultArray[2] = new CharacterRecord { Id = 0xFF };
                }

                return resultArray;
            }
        }

        public int LiveTotalSeconds => BitConverter.ToInt32(_map, SaveMapOffsets.NumberOfSecondsPlayed);
        public short LocID => BitConverter.ToInt16(_map, 0xB96);
        public short MapID => BitConverter.ToInt16(_map, 0xB94);
        public short PartyGP => BitConverter.ToInt16(_map, SaveMapOffsets.GP);
        public short PosX => BitConverter.ToInt16(_map, 0xB9A);
        public short PosY => BitConverter.ToInt16(_map, 0xB9);
        public int PreviewGil => BitConverter.ToInt32(_map, SaveMapOffsets.SavePreviewGil);

        public string PreviewMapName
        {
            get
            {
                var mapNameBytes = new byte[32];
                Array.Copy(_map, SaveMapOffsets.SavePreviewLocation, mapNameBytes, 0, 32);
                return mapNameBytes.ToFFString();
            }
        }

        public CharacterRecord[] PreviewParty
        {
            get
            {
                var resultArray = new CharacterRecord[3];
                if (FillChar((Character)_map[0x5], ref resultArray[0], _map) == false)
                {
                    return null;
                }

                if (FillChar((Character)_map[0x6], ref resultArray[1], _map) == false)
                {
                    resultArray[1] = default;
                    resultArray[2] = default;
                    return resultArray;
                }

                if (FillChar((Character)_map[0x7], ref resultArray[2], _map) == false)
                {
                    resultArray[2] = default;
                    return resultArray;
                }

                return resultArray;
            }
        }

        public int PreviewTotalSeconds => BitConverter.ToInt32(_map, SaveMapOffsets.SavePreviewTimePlayed);

        public uint UltimateWeaponHp
        {
            get
            {
                // This is stored as a 24-bit integer, so we have to pad the value out with 0s to use a 32-but conversion
                var paddedHp = new byte[4];
                Array.Copy(_map, SaveMapOffsets.UltimateWeaponHp, paddedHp, 1, 3);
                paddedHp[0] = 0;
                var ultimateWeaponHp = BitConverter.ToUInt32(paddedHp, 0);
                return ultimateWeaponHp;
            }
        }

        public string WindowColorBottomLeft { get; set; }
        public string WindowColorBottomRight { get; set; }
        public string WindowColorTopLeft { get; set; }
        public string WindowColorTopRight { get; set; }

        #endregion Public Properties

        #region Private Properties

        private bool IsValid { get; }

        #endregion Private Properties

        #region Public Methods

        public static bool VerifyMapIntegrity(byte[] map)
        {
            var consistencyCheck = true;
            try
            {
                if (map[0x4FB] != 0xFF)
                    consistencyCheck = false;
                if (map[0xB98] != 0x0)
                    consistencyCheck = false;
                if (map[0xBA3] != 0x0)
                    consistencyCheck = false;
            }
            catch (Exception ex)
            {
                consistencyCheck = false;
            }

            return consistencyCheck;
        }

        #endregion Public Methods

        #region Private Methods

        private static bool FillChar(Character character, ref CharacterRecord characterRecord, byte[] map)
        {
            var offset = GetCharacterOffset(character);

            if (offset == -1)
            {
                // Invalid or empty record
                return false;
            }

            characterRecord.Character = character;

            var characterNameBytes = new byte[12];
            Array.Copy(map, offset + SaveMapCharacterOffsets.Name, characterNameBytes, 0, 12);
            characterRecord.Name = characterNameBytes.ToFFString();

            characterRecord.Level = map[offset + SaveMapCharacterOffsets.Level];
            characterRecord.Strength = map[offset + SaveMapCharacterOffsets.Strength];
            characterRecord.Vitality = map[offset + SaveMapCharacterOffsets.Vitality];
            characterRecord.Magic = map[offset + SaveMapCharacterOffsets.Magic];
            characterRecord.Spirit = map[offset + SaveMapCharacterOffsets.Spirit];
            characterRecord.Dexterity = map[offset + SaveMapCharacterOffsets.Dexterity];
            characterRecord.Luck = map[offset + SaveMapCharacterOffsets.Luck];
            characterRecord.StrBonus = map[offset + SaveMapCharacterOffsets.StrengthBonus];
            characterRecord.VitBonus = map[offset + SaveMapCharacterOffsets.VitalityBonus];
            characterRecord.MagBonus = map[offset + SaveMapCharacterOffsets.MagicBonus];
            characterRecord.SprBonus = map[offset + SaveMapCharacterOffsets.SpiritBonus];
            characterRecord.DexBonus = map[offset + SaveMapCharacterOffsets.DexterityBonus];
            characterRecord.LucBonus = map[offset + SaveMapCharacterOffsets.LuckBonus];
            characterRecord.LimitLevel = map[offset + SaveMapCharacterOffsets.LimitLevel];
            characterRecord.LimitBar = map[offset + 0x0F];
            characterRecord.Weapon = map[offset + SaveMapCharacterOffsets.EquipedWeapon];
            characterRecord.Armor = map[offset + SaveMapCharacterOffsets.EquipedArmor];
            characterRecord.Accessory = map[offset + SaveMapCharacterOffsets.EquipedAccessory];
            characterRecord.Flags = map[offset + SaveMapCharacterOffsets.StatusFlags];
            characterRecord.AtFront = map[offset + SaveMapCharacterOffsets.Row] == 0xFF;
            characterRecord.LevelProgress = map[offset + 0x21];
            characterRecord.LimitMask = BitConverter.ToInt16(map, offset + SaveMapCharacterOffsets.LimitBreaks);
            characterRecord.Kills = BitConverter.ToInt16(map, offset + SaveMapCharacterOffsets.NumberOfKills);
            characterRecord.LimitTimes = new short[3];
            characterRecord.LimitTimes[0] =
                BitConverter.ToInt16(map, offset + SaveMapCharacterOffsets.LimitLevel1Uses);
            characterRecord.LimitTimes[1] =
                BitConverter.ToInt16(map, offset + SaveMapCharacterOffsets.LimitLevel2Uses);
            characterRecord.LimitTimes[2] =
                BitConverter.ToInt16(map, offset + SaveMapCharacterOffsets.LimitLevel3Uses);
            characterRecord.CurrentHp = BitConverter.ToInt16(map, offset + SaveMapCharacterOffsets.CurrentHp);
            characterRecord.BaseHp = BitConverter.ToInt16(map, offset + SaveMapCharacterOffsets.BaseHp);
            characterRecord.MaxHp = BitConverter.ToInt16(map, offset + SaveMapCharacterOffsets.MaxHp);
            characterRecord.CurrentMp = BitConverter.ToInt16(map, offset + SaveMapCharacterOffsets.CurrentMp);
            characterRecord.BaseMp = BitConverter.ToInt16(map, offset + SaveMapCharacterOffsets.BaseMp);
            characterRecord.MaxMp = BitConverter.ToInt16(map, offset + SaveMapCharacterOffsets.MaxMp);
            characterRecord.Experience = BitConverter.ToInt32(map, offset + SaveMapCharacterOffsets.CurrentExp);
            characterRecord.WeaponMateria = new int[8];

            for (var materiaSlot = 0; materiaSlot <= 7; materiaSlot++)
            {
                characterRecord.WeaponMateria[materiaSlot] =
                    map[offset + SaveMapCharacterOffsets.WeaponMateria1 + (4 * materiaSlot)];
            }

            characterRecord.ArmorMateria = new int[8];

            for (var materiaSlot = 0; materiaSlot <= 7; materiaSlot++)
            {
                characterRecord.ArmorMateria[materiaSlot] =
                    map[offset + SaveMapCharacterOffsets.ArmorMateria1 + (4 * materiaSlot)];
            }

            characterRecord.ExpToLevel = BitConverter.ToInt32(map, offset + SaveMapCharacterOffsets.ExpToNextLevel);
            return true;
        }

        private static short GetCharacterOffset(Character character)
        {
            switch (character)
            {
                case Character.Cloud:
                    return SaveMapOffsets.CloudRecord;

                case Character.Barret:
                    return SaveMapOffsets.BarretRecord;

                case Character.Tifa:
                    return SaveMapOffsets.TifaRecord;

                case Character.Aeris:
                    return SaveMapOffsets.AerisRecord;

                case Character.RedXIII:
                    return SaveMapOffsets.RedXIIIRecord;

                case Character.Yuffie:
                    return SaveMapOffsets.YuffieRecord;

                case Character.CaitSith:
                    return SaveMapOffsets.CaitSithRecord;

                case Character.Vincent:
                    return SaveMapOffsets.VincentRecord;

                case Character.Cid:
                    return SaveMapOffsets.CidRecord;

                case Character.YoungCloud:
                    return SaveMapOffsets.YoungCloudRecord;

                case Character.Sephiroth:
                    return SaveMapOffsets.SephirothRecord;

                case Character.Chocobo:
                case Character.None:
                default:
                    return -1;
            }
        }

        #endregion Private Methods
    }
}