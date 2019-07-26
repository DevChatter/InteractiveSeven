using InteractiveSeven.Core.Data;

namespace InteractiveSeven.Core.Diagnostics.Memory
{
    public interface IGameMomentAccessor
    {
        bool AtMomentOrLater(GameMoments momentToCheck);
    }
}