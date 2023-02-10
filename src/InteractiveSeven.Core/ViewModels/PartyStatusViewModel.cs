using System;
using System.Linq;
using InteractiveSeven.Core.Data;
using InteractiveSeven.Core.FinalFantasy;
using InteractiveSeven.Core.FinalFantasy.Constants;
using InteractiveSeven.Core.FinalFantasy.MemModels;
using InteractiveSeven.Core.FinalFantasy.Models;
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

            var characters = map.LiveParty.Select(x => Character.FromCharacterRecord(x, gameDatabase)).ToArray();

            for (var index = 0; index < characters.Length; ++index)
            {
                // Skip empty party
                if (characters[index].Id == FF7Const.Empty) continue;

                var chr = characters[index];

                var effect = characters[index].StatusEffectsValue;

                if (battleMap.IsActiveBattle)
                {
                    BattleActor battleActor = battleMap.Party[index];
                    chr.CurrentHp = battleActor.CurrentHp;
                    chr.MaxHp = battleActor.MaxHp;
                    chr.CurrentMp = battleActor.CurrentMp;
                    chr.MaxMp = battleActor.MaxMp;
                    chr.Level = battleActor.Level;
                    effect = battleActor.Status;
                    chr.BackRow = battleActor.IsBackRow;
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