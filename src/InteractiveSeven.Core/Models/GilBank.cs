using System;
using System.Collections.Generic;

namespace InteractiveSeven.Core.Models
{
    public class GilBank
    {
        private Dictionary<string, int> _ledger = new Dictionary<string, int>();

        private readonly object padlock = new object();

        public int Deposit(string username, int bits)
        {
            lock (padlock)
            {
                _ledger.TryGetValue(username, out int balance);
                balance += bits;
                _ledger[username] = balance;
                return balance;
            }
        }

        public (int balance, int withdrawn) Withdraw(string username, int bits, bool requireBalance = false)
        {
            lock (padlock)
            {
                _ledger.TryGetValue(username, out int balance);
                if (requireBalance && balance < bits)
                {
                    return (balance, 0);
                }
                int withdrawn = Math.Min(balance, bits);
                balance -= withdrawn;
                _ledger[username] = balance;
                return (balance, withdrawn);
            }
        }
    }
}
