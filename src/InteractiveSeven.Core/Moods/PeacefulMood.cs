using InteractiveSeven.Core.Battle;
using InteractiveSeven.Core.Diagnostics.Memory;
using InteractiveSeven.Core.ViewModels;

namespace InteractiveSeven.Core.Moods
{
    public class PeacefulMood : Mood
    {
        private readonly IStatusAccessor _statusAccessor;
        private readonly PartyStatusViewModel _partyStatus;
        private readonly IBattleInfoAccessor _battleInfoAccessor;

        public PeacefulMood(IStatusAccessor statusAccessor, PartyStatusViewModel partyStatus,
            IBattleInfoAccessor battleInfoAccessor) : base("Peaceful Mood")
        {
            _statusAccessor = statusAccessor;
            _partyStatus = partyStatus;
            _battleInfoAccessor = battleInfoAccessor;
        }

        public const int DefaultId = 2;
        public override int Id => DefaultId;

        public override void ApplyEffect()
        {
            if (IsBattleActive())
            {
                var (valid, _, _) = _partyStatus.CheckTargetValidity(Allies.All, StatusEffects.Barrier);
                foreach (Allies target in valid)
                {
                    _statusAccessor.SetActorStatus(target, StatusEffects.Barrier);
                }
                (valid, _, _) = _partyStatus.CheckTargetValidity(Allies.All, StatusEffects.MBarrier);
                foreach (Allies target in valid)
                {
                    _statusAccessor.SetActorStatus(target, StatusEffects.MBarrier);
                }
            }
        }

        protected bool IsBattleActive()
        {
            var ff7BattleMap = _battleInfoAccessor.GetBattleMap();
            return ff7BattleMap.IsActiveBattle && !ff7BattleMap.IsBattleEnding;
        }

    }
}
