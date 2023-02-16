using System;
using InteractiveSeven.Core.Models;

namespace InteractiveSeven.Core.Commands
{
    public class CooldownTracker
    {
        private readonly TimeSpan _cooldownTime;
        private readonly IClock _clock;
        private DateTime _nextAvailable = DateTime.MinValue;

        public CooldownTracker(int cooldownMinutes)
            : this(cooldownMinutes, new SystemClock())
        {
        }

        public CooldownTracker(int cooldownMinutes, IClock clock)
        {
            _clock = clock;
            _cooldownTime = TimeSpan.FromMinutes(cooldownMinutes);
        }

        public bool IsReady => _clock.UtcNow > _nextAvailable;

        public void Run(in ChatUser chatUser)
        {
            _nextAvailable = _clock.UtcNow.Add(_cooldownTime);
        }
    }
}
