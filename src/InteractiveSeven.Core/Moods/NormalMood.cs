namespace InteractiveSeven.Core.Moods
{
    public class NormalMood : Mood
    {
        public const int DefaultId = 1;

        public NormalMood() : base("Normal Mood")
        {
        }

        public override int Id => DefaultId;
        public override void ApplyEffect()
        {
        }
    }
}
