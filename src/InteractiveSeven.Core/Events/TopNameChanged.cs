namespace InteractiveSeven.Core.Events
{
    public class TopNameChanged : BaseDomainEvent
    {
        public string CharName { get; set; }
        public string NewName { get; set; }

        public TopNameChanged(string charName, string newName)
        {
            CharName = charName;
            NewName = newName;
        }
    }
}