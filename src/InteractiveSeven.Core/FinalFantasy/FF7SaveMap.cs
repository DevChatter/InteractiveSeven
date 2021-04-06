using InteractiveSeven.Core.Data;
using InteractiveSeven.Core.FinalFantasy;
using InteractiveSeven.Core.FinalFantasy.Constants;
using InteractiveSeven.Core.FinalFantasy.MemModels;
using InteractiveSeven.Core.Models;
using Shojy.FF7.Elena.Extensions;
using System;

namespace Tseng.GameData
{
    public class FF7SaveMap
    {
        private readonly byte[] _map;
        private readonly byte[] _colors = MenuColors.Classic.GetDisplayBytes();
        private CharacterRecord[] _liveParty;

        public FF7SaveMap(byte[] map, byte[] colors)
        {
            // Not much else to do here. Checking validity of the response won't be useful in a constructor since
            // we can't get "out" of it, though the caller of our constructor can check .IsValid
            var valid = VerifyMapIntegrity(map);

            IsValid = valid;
            _map = !valid ? null : map;
            _colors = colors ?? _colors;
        }

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

        public CharacterRecord[] LiveParty => _liveParty ??= FillLiveParty();

        private CharacterRecord[] FillLiveParty()
        {
            var liveParty = new[]
            {
                CreateCharacterRecord(_map[SaveMapOffsets.PartyMember1], _map),
                CreateCharacterRecord(_map[SaveMapOffsets.PartyMember2], _map),
                CreateCharacterRecord(_map[SaveMapOffsets.PartyMember3], _map)
            };
            return liveParty;
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
                var resultArray = new CharacterRecord[3]
                {
                    CreateCharacterRecord(_map[SaveMapOffsets.PartyMember1], _map),
                    CreateCharacterRecord(_map[SaveMapOffsets.PartyMember2], _map),
                    CreateCharacterRecord(_map[SaveMapOffsets.PartyMember3], _map)
                };
                if (resultArray[0].Id == FF7Const.Empty)
                {
                    return null;
                }
                if (resultArray[1].Id == FF7Const.Empty)
                {
                    resultArray[1] = default;
                }
                if (resultArray[2].Id == FF7Const.Empty)
                {
                    resultArray[2] = default;
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

        public string WindowColorBottomLeft => $"{_colors[0x2]:X2}{_colors[0x1]:X2}{_colors[0x0]:X2}";
        public string WindowColorBottomRight => $"{_colors[0x6]:X2}{_colors[0x5]:X2}{_colors[0x4]:X2}";
        public string WindowColorTopLeft => $"{_colors[0xA]:X2}{_colors[0x9]:X2}{_colors[0x8]:X2}";
        public string WindowColorTopRight => $"{_colors[0xE]:X2}{_colors[0xD]:X2}{_colors[0xC]:X2}";

        private bool IsValid { get; }

        public static bool VerifyMapIntegrity(byte[] map)
        {
            var consistencyCheck = true;
            try
            {
                if (map[0x4FB] != FF7Const.Empty)
                    consistencyCheck = false;
                if (map[0xB98] != 0x0)
                    consistencyCheck = false;
                if (map[0xBA3] != 0x0)
                    consistencyCheck = false;
            }
            catch (Exception)
            {
                consistencyCheck = false;
            }

            return consistencyCheck;
        }

        private static CharacterRecord CreateCharacterRecord(byte charId, byte[] map)
        {
            var charName = CharNames.GetById(charId);

            var offset = charName.SaveMapRecordOffset;

            if (offset == -1)
            {
                // Invalid or empty record
                return new CharacterRecord { Id = FF7Const.Empty }; ;
            }

            var record = new Span<byte>(map, offset, 0x80).ToArray().ToType<CharacterRecord>();

            return record;
        }
    }
}
