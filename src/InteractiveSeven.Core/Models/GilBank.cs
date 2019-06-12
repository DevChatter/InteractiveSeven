using System;
using System.Collections.Generic;

namespace InteractiveSeven.Core.Models
{
    public class GilBank
    {
        private Dictionary<string, int> Accounts { get; } = new Dictionary<string, int>();

        private readonly object _padlock = new object();

        public int Deposit(string username, int bits)
        {
            lock (_padlock)
            {
                Accounts.TryGetValue(username, out int balance);
                balance += bits;
                Accounts[username] = balance;
                return balance;
            }
        }

        public (int balance, int withdrawn) Withdraw(string username, int bits, bool requireBalance = false)
        {
            lock (_padlock)
            {
                Accounts.TryGetValue(username, out int balance);
                if (requireBalance && balance < bits)
                {
                    return (balance, 0);
                }
                int withdrawn = Math.Min(balance, bits);
                balance -= withdrawn;
                Accounts[username] = balance;
                return (balance, withdrawn);
            }
        }
    }
}
