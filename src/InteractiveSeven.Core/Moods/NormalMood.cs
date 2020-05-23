namespace InteractiveSeven.Core.Moods
{
    public class NormalMood : BaseMood
    {
        public NormalMood() : base("Normal Mood")
        {
        }

        public override int Id => 1;
        public override void ApplyEffect()
        {
        }
    }
}