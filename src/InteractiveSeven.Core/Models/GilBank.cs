using System;
using System.Collections.Generic;

namespace InteractiveSeven.Core.Models
{
    public class GilBank
    {
        private Dictionary<string, Account> Accounts { get; } = new Dictionary<string, Account>();

        private readonly object _padlock = new object();

        public int Deposit(string username, int bits)
        {
            lock (_padlock)
            {
                var account = AccessAccount(username);
                account.Balance += bits;
                return account.Balance;
            }
        }

        public (int balance, int withdrawn) Withdraw(string username, int bits, bool requireBalance = false)
        {
            lock (_padlock)
            {
                var account = AccessAccount(username);
                if (requireBalance && account.Balance < bits)
                {
                    return (account.Balance, 0);
                }
                int withdrawn = Math.Min(account.Balance, bits);
                account.Balance -= withdrawn;
                return (account.Balance, withdrawn);
            }
        }

        public int CheckBalance(string username)
        {
            var account = AccessAccount(username);
            return account.Balance;
        }

        private Account AccessAccount(string username)
        {
            var account = Accounts.GetOrCreate(username);

            return account;
        }
    }
}
