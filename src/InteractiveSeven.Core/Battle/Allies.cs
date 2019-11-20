using InteractiveSeven.Core.Diagnostics.Memory;
using System.Collections.Generic;
using System.Linq;

namespace InteractiveSeven.Core.Battle
{
    public class Allies
    {
        public int Index { get; }
        public MemLoc PrimaryStatus { get; set; }
        public MemLoc SecondaryStatus { get; set; }
        public string[] Words { get; set; }

        public Allies(int index, MemLoc primaryStatus, MemLoc secondaryStatus, params string[] words)
        {
            Index = index;
            PrimaryStatus = primaryStatus;
            SecondaryStatus = secondaryStatus;
            Words = words ?? new string[0];
            All.Add(this);
        }

        public static List<Allies> All = new List<Allies>();

        public static List<Allies> ByWord(string word)
            => All.Where(x => x.Words.Any(w => w.EqualsIns(word))).ToList();

        public static Allies Top = new Allies(0, new MemLoc(0x9AB0DC), new MemLoc(0xBF23C0), "top", "all");
        public static Allies Mid = new Allies(1, new MemLoc(0x9AB144), new MemLoc(0xBF2434), "mid", "middle", "all");
        public static Allies Bot = new Allies(2, new MemLoc(0x9AB1AC), new MemLoc(0xBF24A8), "bot", "bottom", "all");
        // Primary Offset 0x68
        // Secondary Offset 0x74
    }
}