using InteractiveSeven.Core.Battle;
using InteractiveSeven.Core.Diagnostics.Memory;
using InteractiveSeven.Core.ViewModels;

namespace InteractiveSeven.Core.Moods
{
    public class LooseChangeMood : Mood
    {
        private readonly IGilAccessor _gilAccessor;

        public LooseChangeMood(IGilAccessor gilAccessor)
            : base("Loose Change Mood")
        {
            _gilAccessor = gilAccessor;
        }

        public const int DefaultId = 4;
        public override int Id => DefaultId;
        public override void ApplyEffect()
        {
            // TODO: Vary this and make configurable.
            //       Should be something based on current money with a minimum.
            _gilAccessor.RemoveGil(10);
        }
    }
}
