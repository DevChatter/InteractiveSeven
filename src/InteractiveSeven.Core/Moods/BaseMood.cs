namespace InteractiveSeven.Core.Moods
{
    public abstract class BaseMood
    {
        protected BaseMood(string name)
        {
            Name = name;
        }

        public abstract int Id { get; }
        public string Name { get; }

        public abstract void ApplyEffect();
    }
}
