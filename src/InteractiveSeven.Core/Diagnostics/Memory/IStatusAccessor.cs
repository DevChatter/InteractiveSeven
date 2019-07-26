using InteractiveSeven.Core.Battle;

namespace InteractiveSeven.Core.Memory
{
    public interface IStatusAccessor
    {
        void SetActorStatus(Allies actor, StatusEffects statusEffect);
    }
}