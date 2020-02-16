using InteractiveSeven.Core.Battle;
using InteractiveSeven.Core.Data;
using InteractiveSeven.Core.FinalFantasy;
using InteractiveSeven.Core.FinalFantasy.Models;
using System;
using System.Linq;
using Tseng.Constants;
using Tseng.GameData;

namespace InteractiveSeven.Core.ViewModels
{
    public class PartyStatusViewModel
    {
        public Character[] Party { get; set; }
        public int Gil { get; set; }
        public string Location { get; set; }
        public bool ActiveBattle { get; set; }

        public string ColorTopLeft { get; set; }
        public string ColorTopRight { get; set; }
        public string ColorBottomLeft { get; set; }
        public string ColorBottomRight { get; set; }
        public string TimeActive { get; set; }

        public void UpdateStatusFromMap(FF7SaveMap map, FF7BattleMap battleMap, GameDatabase gameDatabase)
        {
            Gil = map.LiveGil;
            Location = map.LiveMapName;
            Party = new Character[3];
            ActiveBattle = battleMap.IsActiveBattle;
            ColorTopLeft = map.WindowColorTopLeft;
            ColorBottomLeft = map.WindowColorBottomLeft;
            ColorBottomRight = map.WindowColorBottomRight;
            ColorTopRight = map.WindowColorTopRight;
            TimeActive = TimeSpan.FromSeconds(map.LiveTotalSeconds).ToString("hh\\:mm\\:ss");

            for (var index = 0; index < map.LiveParty.Length; ++index)
            {
                // Skip empty party
                if (map.LiveParty[index].Id == FF7Const.Empty) continue;

                var chr = Character.FromCharacterRecord(map.LiveParty[index], gameDatabase);

                var effect = (StatusEffects)map.LiveParty[index].Flags;

                if (battleMap.IsActiveBattle)
                {
                    chr.CurrentHp = battleMap.Party[index].CurrentHp;
                    chr.MaxHp = battleMap.Party[index].MaxHp;
                    chr.CurrentMp = battleMap.Party[index].CurrentMp;
                    chr.MaxMp = battleMap.Party[index].MaxMp;
                    chr.Level = battleMap.Party[index].Level;
                    effect = battleMap.Party[index].Status;
                    chr.BackRow = battleMap.Party[index].IsBackRow;
                }

                var effs = effect.ToString().Split(new[] { ", " }, StringSplitOptions.RemoveEmptyEntries).ToList();
                effs.RemoveAll(x => new[] { "None", "Death" }.Contains(x));
                chr.StatusEffects = effs.ToArray();
                chr.StatusEffectsValue = effect;
                Party[index] = chr;
            }
        }
    }
}