using InteractiveSeven.Core.Battle;

namespace InteractiveSeven.Core.Diagnostics.Memory
{
    public interface IStatusAccessor
    {
        void SetActorStatus(IHasStatus actor, StatusEffects statusEffect);
        bool RemoveActorStatus(IHasStatus actor, StatusEffects statusEffect);
        void ClearNegativeStatuses(IHasStatus target);
    }
}