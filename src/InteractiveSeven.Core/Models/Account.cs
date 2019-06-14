namespace InteractiveSeven.Core.Models
{
    public class Account
    {
        public Account(string username)
        {
            Username = username;
        }

        public int Balance { get; set; }
        public string Username { get; set; }
        public bool ReceivedSubBonus { get; set; }
    }
}