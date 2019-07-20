using System;

namespace InteractiveSeven.Core.Tseng.Models
{
    [Flags]
    public enum StatusEffect:uint
    {
        None          = 0x00000000,

        Death         = 0x00000001,
        NearDeath     = 0x00000002,
        Sleep         = 0x00000004,
        Poison        = 0x00000008,
        Sadness       = 0x00000010,
        Fury          = 0x00000020,
        Confusion     = 0x00000040,
        Silence       = 0x00000080,
        Haste         = 0x00000100,
        Slow          = 0x00000200,
        Stop          = 0x00000400,
        Frog          = 0x00000800,
        Small         = 0x00001000,
        SlowNumb      = 0x00002000,
        Petrify       = 0x00004000,
        Regen         = 0x00008000,

        Barrier       = 0x00010000,
        MBarrier      = 0x00020000,
        Reflect       = 0x00040000,
        Dual          = 0x00080000,
        Shield        = 0x00100000,
        DeathSentence = 0x00200000,
        Manipulate    = 0x00400000,
        Berserk       = 0x00800000,
        Peerless      = 0x01000000,
        Paralyzed     = 0x02000000,
        Darkness      = 0x04000000,
        DualDrain     = 0x08000000,
        DeathForce    = 0x10000000,
        Resist        = 0x20000000,
        LuckyGirl     = 0x40000000,
        Imprisoned    = 0x80000000
    }
}