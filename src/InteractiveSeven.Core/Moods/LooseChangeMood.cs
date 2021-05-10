using InteractiveSeven.Core.Diagnostics.Memory;

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
            // TODO: make configurable. calculate better
            _gilAccessor.RemoveGil(GetAmount());
        }

        private uint GetAmount()
        {
            var gil = _gilAccessor.GetGil();
            if (gil > 1000000)
            {
                return 2000;
            }
            if (gil > 500000)
            {
                return 1000;
            }
            if (gil > 100000)
            {
                return 200;
            }
            return 100;
        }
    }
}
