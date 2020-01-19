using InteractiveSeven.Core.Battle;

namespace InteractiveSeven.Core.Diagnostics.Memory
{
    public interface IStatusAccessor
    {
        void SetActorStatus(Allies actor, StatusEffects statusEffect);
        bool RemoveActorStatus(Allies actor, StatusEffects statusEffect);
        void ClearNegativeStatuses(Allies target);
    }
}