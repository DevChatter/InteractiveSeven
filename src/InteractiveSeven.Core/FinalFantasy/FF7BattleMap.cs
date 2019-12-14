using InteractiveSeven.Core.Battle;
using System;
using System.Linq;
using InteractiveSeven.Core.FinalFantasy.MemModels;
using Tseng.Constants;

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
                var a = new Span<byte>(_map, offset, BattleMapActorOffsets.ActorLength)
                    .ToArray()
                    .ToType<BattleActor>();
                acts[i] = a;
            }

            return acts;
        }

        #endregion Private Methods
    }
}