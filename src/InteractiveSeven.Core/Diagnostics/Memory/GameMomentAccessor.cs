using InteractiveSeven.Core.Data;
using InteractiveSeven.Core.FinalFantasy;
using InteractiveSeven.Core.Settings;
using Microsoft.Extensions.Logging;
using System;

namespace InteractiveSeven.Core.Diagnostics.Memory
{
    public class GameMomentAccessor : IGameMomentAccessor
    {
        private readonly IMemoryAccessor _memoryAccessor;
        private readonly ILogger<GameMomentAccessor> _logger;

        private ushort _lastCheckedMoment;
        private DateTime _nextMomentCheckAllowed = DateTime.UtcNow;
        private static readonly object Padlock = new object();

        private string ProcessName => ApplicationSettings.Instance.ProcessName;

        public GameMomentAccessor(IMemoryAccessor memoryAccessor, ILogger<GameMomentAccessor> logger)
        {
            _memoryAccessor = memoryAccessor;
            _logger = logger;
        }

        public bool AtMomentOrLater(GameMoments momentToCheck)
        {
            if (_lastCheckedMoment >= (ushort)momentToCheck)
            {
                return true;
            }

            TryUpdateMoment();

            return _lastCheckedMoment >= (ushort)momentToCheck;
        }

        private void TryUpdateMoment()
        {
            try
            {
                if (DateTime.UtcNow >= _nextMomentCheckAllowed)
                {
                    lock (Padlock)
                    {
                        if (DateTime.UtcNow >= _nextMomentCheckAllowed)
                        {
                            _nextMomentCheckAllowed = DateTime.UtcNow.AddSeconds(2);
                            byte[] bytes = new byte[Addresses.GameMoment.NumBytes];
                            _memoryAccessor.ReadMem(ProcessName, Addresses.GameMoment.Address, bytes);
                            _lastCheckedMoment = (ushort)((bytes[1] << 8) + bytes[0]);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error fetching game moment.");
            }
        }
    }
}