using System;

namespace InteractiveSeven.Core
{
    public interface IClock
    {
        DateTime UtcNow { get; }
    }
}