using System.Linq;
using InteractiveSeven.Core.FinalFantasy;
using InteractiveSeven.Core.Settings;

namespace InteractiveSeven.Core.Diagnostics.Memory
{
    public class BattleInfoAccessor : IBattleInfoAccessor
    {
        private readonly IMemoryAccessor _memory;
        private ApplicationSettings Settings => ApplicationSettings.Instance;

        public BattleInfoAccessor(IMemoryAccessor memory)
        {
            _memory = memory;
        }

        public FF7BattleMap GetBattleMap()
        {
            var bytes = new byte[Addresses.BattleMapStart.NumBytes];
            _memory.ReadMem(Settings.ProcessName, Addresses.BattleMapStart.Address, bytes);
            var isBattleByte = new byte[1];
            _memory.ReadMem(Settings.ProcessName, Addresses.ActiveBattleState.Address, isBattleByte);

            return new FF7BattleMap(bytes, isBattleByte.Single());
        }
    }
}