using InteractiveSeven.Core.FinalFantasy.MemModels;
using System;
using Tseng.Constants;

namespace Tseng.GameData
{
    public class FF7BattleMap
    {
        private readonly byte[] _map;
        private BattleActor[] _opponents;
        private BattleActor[] _party;

        public FF7BattleMap(byte[] bytes, byte activeBattle)
        {
            IsActiveBattle = activeBattle == 0x01;
            _map = bytes;
        }

        public bool IsActiveBattle { get; }
        public BattleActor[] Opponents => _opponents ??= GetActors(BattleMapOffsets.EnemyActors, 6);
        public BattleActor[] Party => _party ??= GetActors(BattleMapOffsets.PartyActors, 4);

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
    }
}