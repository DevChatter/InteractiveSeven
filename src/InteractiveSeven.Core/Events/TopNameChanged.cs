using InteractiveSeven.Core.Data;

namespace InteractiveSeven.Core.Events
{
    public class TopNameChanged : BaseDomainEvent
    {
        public CharNames CharName { get; set; }
        public string NewName { get; set; }

        public TopNameChanged(CharNames charName, string newName)
        {
            CharName = charName;
            NewName = newName;
        }
    }
}