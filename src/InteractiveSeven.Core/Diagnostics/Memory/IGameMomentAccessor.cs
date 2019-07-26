using InteractiveSeven.Core.Data;

namespace InteractiveSeven.Core.Memory
{
    public interface IGameMomentAccessor
    {
        bool AtMomentOrLater(GameMoments momentToCheck);
    }
}