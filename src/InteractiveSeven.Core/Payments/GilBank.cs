using InteractiveSeven.Core.Model;
using InteractiveSeven.Core.Models;
using InteractiveSeven.Core.Settings;
using System;
using System.Collections.Generic;
using System.Linq;

namespace InteractiveSeven.Core.Payments
{
    public class GilBank
    {
        private readonly HashSet<string> _knownUsers = new HashSet<string>();
        private ApplicationSettings Settings => ApplicationSettings.Instance;
        private List<Account> Accounts { get; } = new List<Account>();

        private readonly object _padlock = new object();

        public bool HasAccount(string username) => _knownUsers.Contains(username.ToLower());

        public int Deposit(in ChatUser user, int bits)
        {
            bits = Math.Max(0, bits);
            lock (_padlock)
            {
                var account = AccessAccount(user);
                account.Balance += bits;
                return account.Balance;
            }
        }

        public (int balance, int withdrawn) Withdraw(in ChatUser user, int bits, bool requireBalance = false)
        {
            lock (_padlock)
            {
                var account = AccessAccount(user);
                if (bits < 0 || (requireBalance && account.Balance < bits))
                {
                    return (account.Balance, 0);
                }
                int withdrawn = Math.Min(account.Balance, bits);
                account.Balance -= withdrawn;
                return (account.Balance, withdrawn);
            }
        }

        public int CheckBalance(in ChatUser user)
        {
            var account = AccessAccount(user);
            return account.Balance;
        }

        private Account AccessAccount(in ChatUser user)
        {
            string username = user.Username;
            Account account = Accounts.SingleOrDefault(a => a.Username.EqualsIns(username));
            if (account == null)
            {
                account = new Account(username);
                Accounts.Add(account);
            }

            if (user.IsSubscriber && !account.ReceivedSubBonus && Settings.GiveSubscriberBonusBits)
            {
                account.Balance += Settings.SubscriberBonusBits;
                account.ReceivedSubBonus = true;
            }

            return account;
        }

        public void EnsureAccountExists(in ChatUser user)
        {
            bool newUser = _knownUsers.Add(user.Username.ToLower());
            if (newUser)
            {
                lock (_padlock)
                {
                    AccessAccount(user);
                }
            }
        }
    }
}
