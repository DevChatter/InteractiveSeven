using InteractiveSeven.Core.Diagnostics.Memory;
using System.Collections.Generic;
using System.Linq;

namespace InteractiveSeven.Core.Battle
{
    public class Enemies : IHasStatus
    {
        public int Index { get; }
        public MemLoc PrimaryStatus { get; set; }
        public MemLoc SecondaryStatus { get; set; }
        public string[] Words { get; set; }

        public Enemies(int index, MemLoc primaryStatus, MemLoc secondaryStatus, params string[] words)
        {
            Index = index;
            PrimaryStatus = primaryStatus;
            SecondaryStatus = secondaryStatus;
            Words = words ?? new string[0];
            All.Add(this);
        }

        public static List<Enemies> All = new List<Enemies>();
        public static Enemies ByIndex(int index) => All.SingleOrDefault(x => x.Index == index);
        public static List<Enemies> ByWord(string word)
            => All.Where(x => x.Words.Any(w => StringExtensions.EqualsIns(w, word))).ToList();

        public static Enemies A = new Enemies(0, new MemLoc(0x9AB27C), new MemLoc(0xBF2590), "a", "all");
        public static Enemies B = new Enemies(1, new MemLoc(0x9AB2E4), new MemLoc(0xBF2604), "b", "all");
        public static Enemies C = new Enemies(2, new MemLoc(0x9AB34C), new MemLoc(0xBF2678), "c", "all");
        public static Enemies D = new Enemies(3, new MemLoc(0x9AB3B4), new MemLoc(0xBF26EC), "d", "all");
        public static Enemies E = new Enemies(4, new MemLoc(0x9AB41C), new MemLoc(0xBF2760), "e", "all");
        public static Enemies F = new Enemies(5, new MemLoc(0x9AB484), new MemLoc(0xBF27D4), "f", "all");
        // Primary Offset 0x68
        // Secondary Offset 0x74
    }
}