namespace InteractiveSeven.Core.Models
{
    public class ChangeRecord
    {
        public ChangeRecord(string change, string changer)
        {
            Changer = changer;
            Change = change;
        }

        public string Changer { get; set; }
        public string Change { get; set; }
    }
}
