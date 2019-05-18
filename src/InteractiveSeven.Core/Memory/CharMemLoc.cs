using System;
using System.Collections.Generic;

namespace InteractiveSeven.Core.Memory
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
        public MemLoc CurHP { get; }
        public MemLoc MaxHP { get; }
        public MemLoc CurMP { get; }
        public MemLoc MaxMP { get; }

        private CharMemLoc(IntPtr baseAddress)
        {
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
            CurHP = new MemLoc(IntPtr.Add(baseAddress, 43), 2);
            MaxHP = new MemLoc(IntPtr.Add(baseAddress, 45), 2);
            CurMP = new MemLoc(IntPtr.Add(baseAddress, 47), 2);
            MaxMP = new MemLoc(IntPtr.Add(baseAddress, 49), 2);
        }

        public static CharMemLoc ByName(string name)
        {
            return All[name];
        }

        public static CharMemLoc Cloud { get; }
            = new CharMemLoc(new IntPtr(0xDBFD8D));
        public static CharMemLoc Barret { get; }
            = new CharMemLoc(new IntPtr(0xDBFE11));
        public static CharMemLoc Tifa { get; }
            = new CharMemLoc(new IntPtr(0xDBFE95));
        public static CharMemLoc Aeris { get; }
            = new CharMemLoc(new IntPtr(0xDBFF19));
        public static CharMemLoc Red { get; }
            = new CharMemLoc(new IntPtr(0xDBFF9D));
        public static CharMemLoc Yuffie { get; }
            = new CharMemLoc(new IntPtr(0xDC0021));
        public static CharMemLoc CaitSith { get; }
            = new CharMemLoc(new IntPtr(0xDC00A5));
        public static CharMemLoc Vincent { get; }
            = new CharMemLoc(new IntPtr(0xDC0129));
        public static CharMemLoc Cid { get; }
            = new CharMemLoc(new IntPtr(0xDC01AD));

        private static readonly Dictionary<string, CharMemLoc> All
            = new Dictionary<string, CharMemLoc>
            {
                [Constants.Cloud] = Cloud,
                [Constants.Barret] = Barret,
                [Constants.Tifa] = Tifa,
                [Constants.Aeris] = Aeris,
                [Constants.Red] = Red,
                [Constants.Yuffie] = Yuffie,
                [Constants.CaitSith] = CaitSith,
                [Constants.Vincent] = Vincent,
                [Constants.Cid] = Cid,
            };

    }
}