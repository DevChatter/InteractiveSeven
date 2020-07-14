using System;

namespace InteractiveSeven.Core.Models
{
    public class Account
    {
        public Account()
        {
        }

        public Account(string userId, string username)
        {
            Username = username;
            UserId = userId;
        }

        public int Balance { get; set; }
        public string Username { get; set; }
        public string UserId { get; set; }
        public DateTime LastSubBonus { get; set; }
    }
}