using System;

namespace InteractiveSeven.Core.FinalFantasy
{
    [Flags]
    public enum MenuFlags : ushort
    {
        Item = 0x1,
        Magic = 0x2,
        Materia = 0x4,
        Equip = 0x8,
        Status = 0x10,
        Order = 0x20,
        Limit = 0x40,
        Config = 0x80,
        PHS = 0x100,
        Save = 0x200,
    }
}