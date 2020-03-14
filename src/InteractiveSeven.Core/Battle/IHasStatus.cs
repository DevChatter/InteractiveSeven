using InteractiveSeven.Core.Diagnostics.Memory;

namespace InteractiveSeven.Core.Battle
{
    public interface IHasStatus
    {
        MemLoc PrimaryStatus { get; set; }
        MemLoc SecondaryStatus { get; set; }
    }
}