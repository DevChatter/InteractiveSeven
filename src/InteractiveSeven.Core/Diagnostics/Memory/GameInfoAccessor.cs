using InteractiveSeven.Core.FinalFantasy;
using InteractiveSeven.Core.Settings;
using Tseng.GameData;

namespace InteractiveSeven.Core.Diagnostics.Memory
{
    public class GameInfoAccessor : IGameInfoAccessor
    {
        private readonly IMemoryAccessor _memory;
        private ApplicationSettings Settings => ApplicationSettings.Instance;

        public GameInfoAccessor(IMemoryAccessor memory)
        {
            _memory = memory;
        }

        public FF7SaveMap GetGameInfoMap()
        {
            var bytes = new byte[Addresses.SaveMapStart.NumBytes];
            _memory.ReadMem(Settings.ProcessName, Addresses.SaveMapStart.Address, bytes);

            return new FF7SaveMap(bytes);
        }
    }
}