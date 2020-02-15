using System;

namespace InteractiveSeven.Core
{
    [Flags]
    public enum GamePlayEffects : uint
    {
        DisplayOnly = 1,
        MildEffect = 2,
        MajorEffect = 4,
    }
}