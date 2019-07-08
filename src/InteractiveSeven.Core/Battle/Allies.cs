using InteractiveSeven.Core.Memory;
using System.Collections.Generic;
using System.Linq;

namespace InteractiveSeven.Core.Battle
{
    public class Allies
    {
        public MemLoc PrimaryStatus { get; set; }
        public MemLoc SecondaryStatus { get; set; }
        public string[] Words { get; set; }

        public Allies(MemLoc primaryStatus, MemLoc secondaryStatus, params string[] words)
        {
            PrimaryStatus = primaryStatus;
            SecondaryStatus = secondaryStatus;
            Words = words ?? new string[0];
            All.Add(this);
        }

        public static List<Allies> All = new List<Allies>();

        public static Allies ByWord(string word)
            => All.FirstOrDefault(x => x.Words.Any(w => w.EqualsIns(word)));

        public static Allies Top = new Allies(new MemLoc(0x9AB0DC), new MemLoc(0xBF23C0), "top");
        public static Allies Mid = new Allies(new MemLoc(0x9AB144), new MemLoc(0xBF2434), "mid", "middle");
        public static Allies Bot = new Allies(new MemLoc(0x9AB1AC), new MemLoc(0xBF24A8), "bot", "bottom");
        // Primary Offset 0x68
        // Secondary Offset 0x74
    }
}