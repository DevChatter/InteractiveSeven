using System;

namespace InteractiveSeven.Core
{
    public static class SafeLock
    {
        public static void DoInLock(Func<bool> shouldLock, ref object theLock, Action action)
        {
            if (shouldLock())
            {
                lock (theLock)
                {
                    if (shouldLock())
                    {
                        action();
                    }
                }
            }
        }
    }
}
