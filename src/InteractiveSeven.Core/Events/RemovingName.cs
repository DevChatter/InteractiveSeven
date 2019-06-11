namespace InteractiveSeven.Core.Events
{
    public class RemovingName :BaseDomainEvent
    {
        public string NameToRemove { get; set; }

        public RemovingName(string nameToRemove)
        {
            NameToRemove = nameToRemove;
        }
    }
}