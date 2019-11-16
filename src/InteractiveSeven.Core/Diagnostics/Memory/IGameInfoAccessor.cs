using Tseng.GameData;

namespace InteractiveSeven.Core.Diagnostics.Memory
{
    public interface IGameInfoAccessor
    {
        FF7SaveMap GetGameInfoMap();
    }
}