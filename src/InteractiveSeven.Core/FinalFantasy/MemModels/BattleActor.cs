using System.Runtime.InteropServices;
using InteractiveSeven.Core.Battle;

namespace InteractiveSeven.Core.FinalFantasy.MemModels
{
    [StructLayout(LayoutKind.Explicit, Size = 0x68)]
    public struct BattleActor
    {
        [FieldOffset(0)] public StatusEffects Status;
        [FieldOffset(4)] public byte Row;
        [FieldOffset(9)] public byte Level;
        [FieldOffset(40)] public ushort CurrentMp;
        [FieldOffset(42)] public ushort MaxMp;
        [FieldOffset(44)] public uint CurrentHp;
        [FieldOffset(48)] public uint MaxHp;
        public bool IsBackRow => (Row & 0x40) == 0x40;
        public bool IsOutOfCombat => (Status & OutOfCombatStatuses) > 0 || CurrentHp == 0; // TODO : Detect is non-existent

        private static readonly StatusEffects OutOfCombatStatuses =
            StatusEffects.Death | StatusEffects.Petrify | StatusEffects.Imprisoned;
    }
}