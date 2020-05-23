using InteractiveSeven.Core.Battle;
using InteractiveSeven.Core.Diagnostics.Memory;
using InteractiveSeven.Core.ViewModels;

namespace InteractiveSeven.Core.Moods
{
    // TODO: Every minute run the mood checker
    //       - If Time Remaining:
    //          Show Message and Alert Chat about remaining time
    //       - If Time Complete:
    //          Reset All Mood Voting
    //          Change the Current Mood to Top Voted Mood

    public abstract class BaseMood
    {
        protected BaseMood(string name)
        {
            Name = name;
        }

        public abstract int Id { get; }
        public string Name { get; }

        public abstract void ApplyEffect();
    }

    public class NormalMood : BaseMood
    {
        public NormalMood() : base("Normal Mood")
        {
        }

        public override int Id => 1;
        public override void ApplyEffect()
        {
        }
    }

    public class PeacefulMood : BaseMood
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

        public override int Id => 2;

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

    public class DangerMood : BaseMood
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

        public override int Id => 3;
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
