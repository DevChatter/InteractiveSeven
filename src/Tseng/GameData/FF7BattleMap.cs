using System;
using InteractiveSeven.Core.Tseng.Models;
using Tseng.Constants;
using Tseng.Models;

namespace Tseng.GameData
{
    public class FF7BattleMap
    {
        #region Private Fields

        private readonly byte[] _map;

        #endregion Private Fields

        #region Public Constructors

        public FF7BattleMap(byte[] bytes, byte activeBattle)
        {
            IsActiveBattle = activeBattle == 0x01;
            _map = bytes;
        }

        #endregion Public Constructors

        #region Public Properties

        public bool IsActiveBattle { get; set; }

        public BattleActor[] Opponents => GetActors(BattleMapOffsets.EnemyActors, 6);
        public BattleActor[] Party => GetActors(BattleMapOffsets.PartyActors, 4);

        #endregion Public Properties

        #region Private Methods

        private BattleActor[] GetActors(int start, int count)
        {
            var acts = new BattleActor[count];

            for (var i = 0; i < count; ++i)
            {
                var offset = start + i * BattleMapActorOffsets.ActorLength;
                var a = new BattleActor
                {
                    CurrentHp = BitConverter.ToInt32(_map, offset + BattleMapActorOffsets.CurrentHp),
                    MaxHp = BitConverter.ToInt32(_map, offset + BattleMapActorOffsets.MaxHp),
                    CurrentMp = BitConverter.ToInt16(_map, offset + BattleMapActorOffsets.CurrentMp),
                    MaxMp = BitConverter.ToInt16(_map, offset + BattleMapActorOffsets.MaxMp),
                    Level = _map[BattleMapActorOffsets.Level],
                    Status = (StatusEffect)BitConverter.ToUInt32(_map, offset + BattleMapActorOffsets.Status),
                    IsBackRow = (_map[offset + BattleMapActorOffsets.Row] & 0x40) == 0x40
                };
                acts[i] = a;
            }

            return acts;
        }

        #endregion Private Methods
    }
}