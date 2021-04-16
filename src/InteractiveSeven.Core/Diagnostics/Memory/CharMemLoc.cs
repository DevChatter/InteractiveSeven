using System;
using System.Collections.Generic;
using InteractiveSeven.Core.Data;

namespace InteractiveSeven.Core.Diagnostics.Memory
{
    public class CharMemLoc
    {
        public MemLoc Level { get; }
        public MemLoc Str { get; }
        public MemLoc Int { get; }
        public MemLoc Mag { get; }
        public MemLoc Vit { get; }
        public MemLoc Luc { get; }
        public MemLoc Dex { get; }
        public MemLoc Name { get; }
        public MemLoc Weapon { get; }
        public MemLoc Armlet { get; }
        public MemLoc Accessory { get; }
        public MemLoc CurHp { get; }
        public MemLoc MaxHp { get; }
        public MemLoc CurMp { get; }
        public MemLoc MaxMp { get; }
        public MemLoc WeaponMateria { get; }
        public MemLoc ArmorMateria { get; }
        public MemLoc StartingName { get; } // Contiguous 12 bytes for each character.

        private CharMemLoc(IntPtr baseAddress, IntPtr startingNameAddress)
        {
            StartingName = new MemLoc(startingNameAddress, 12);

            Level = new MemLoc(baseAddress);
            Str = new MemLoc(IntPtr.Add(baseAddress, 1));
            Int = new MemLoc(IntPtr.Add(baseAddress, 2));
            Mag = new MemLoc(IntPtr.Add(baseAddress, 3));
            Vit = new MemLoc(IntPtr.Add(baseAddress, 4));
            Luc = new MemLoc(IntPtr.Add(baseAddress, 5));
            Dex = new MemLoc(IntPtr.Add(baseAddress, 6));
            Name = new MemLoc(IntPtr.Add(baseAddress, 15), 10);
            Weapon = new MemLoc(IntPtr.Add(baseAddress, 27));
            Armlet = new MemLoc(IntPtr.Add(baseAddress, 28));
            Accessory = new MemLoc(IntPtr.Add(baseAddress, 29));
            CurHp = new MemLoc(IntPtr.Add(baseAddress, 43), 2);
            MaxHp = new MemLoc(IntPtr.Add(baseAddress, 45), 2);
            CurMp = new MemLoc(IntPtr.Add(baseAddress, 47), 2);
            MaxMp = new MemLoc(IntPtr.Add(baseAddress, 49), 2);
            WeaponMateria = new MemLoc(IntPtr.Add(baseAddress, 63), 32);
            ArmorMateria = new MemLoc(IntPtr.Add(baseAddress, 95), 32);
        }

        public static CharMemLoc ByName(CharNames charName)
        {
            return All[charName.Id];
        }

        public static CharMemLoc Cloud { get; } = new(new(0xDBFD8D), new(0x921CB8));
        public static CharMemLoc Barret { get; } = new(new(0xDBFE11), new(0x921CC4));
        public static CharMemLoc Tifa { get; } = new(new(0xDBFE95), new(0x921CD0));
        public static CharMemLoc Aeris { get; } = new(new(0xDBFF19), new(0x921CDC));
        public static CharMemLoc Red { get; } = new(new(0xDBFF9D), new(0x921CE8));
        public static CharMemLoc Yuffie { get; } = new(new(0xDC0021), new(0x921CF4));
        public static CharMemLoc CaitSith { get; } = new(new(0xDC00A5), new(0x921D00));
        public static CharMemLoc Vincent { get; } = new(new(0xDC0129), new(0x921D0C));
        public static CharMemLoc Cid { get; } = new(new(0xDC01AD), new(0x921D18));

        private static readonly Dictionary<int, CharMemLoc> All = new()
        {
            [CharNames.Cloud.Id] = Cloud,
            [CharNames.Barret.Id] = Barret,
            [CharNames.Tifa.Id] = Tifa,
            [CharNames.Aeris.Id] = Aeris,
            [CharNames.Red.Id] = Red,
            [CharNames.Yuffie.Id] = Yuffie,
            [CharNames.CaitSith.Id] = CaitSith,
            [CharNames.Vincent.Id] = Vincent,
            [CharNames.Cid.Id] = Cid,
        };
    }

    public enum FF7CharState
    {
        Sadness = 16,
        Fury = 32,
        BackRow = 254,
        FrontRow = 255
    };
}
