namespace InteractiveSeven.Core.Moods
{
    public abstract class Mood
    {
        protected Mood(string name)
        {
            Name = name;
        }

        public abstract int Id { get; }
        public string Name { get; }

        public abstract void ApplyEffect();
        public virtual void RemoveEffect() { }
    }
}
