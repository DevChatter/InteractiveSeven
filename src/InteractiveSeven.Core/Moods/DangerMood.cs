using InteractiveSeven.Core.Battle;
using InteractiveSeven.Core.Diagnostics.Memory;
using InteractiveSeven.Core.ViewModels;

namespace InteractiveSeven.Core.Moods
{
    public class DangerMood : Mood
    {
        private readonly IStatusAccessor _statusAccessor;
        private readonly PartyStatusViewModel _partyStatus;
        private readonly IBattleInfoAccessor _battleInfoAccessor;

        public DangerMood(IStatusAccessor statusAccessor, PartyStatusViewModel partyStatus,
            IBattleInfoAccessor battleInfoAccessor) : base("Danger Mood")
        {
            _statusAccessor = statusAccessor;
            _partyStatus = partyStatus;
            _battleInfoAccessor = battleInfoAccessor;
        }

        public const int DefaultId = 3;
        public override int Id => DefaultId;
        public override void ApplyEffect()
        {
            if (IsBattleActive())
            {
                var (_, _, hasHaste) = _partyStatus.CheckTargetValidity(Allies.All, StatusEffects.Haste);
                foreach (Allies hasted in hasHaste)
                {
                    _statusAccessor.RemoveActorStatus(hasted, StatusEffects.Haste);
                }
                var (valid, _, _) = _partyStatus.CheckTargetValidity(Allies.All, StatusEffects.Slow);
                foreach (Allies target in valid)
                {
                    _statusAccessor.SetActorStatus(target, StatusEffects.Slow);
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
