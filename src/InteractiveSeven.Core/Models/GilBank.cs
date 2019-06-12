using System;
using System.Collections.Generic;
using InteractiveSeven.Core.Model;
using InteractiveSeven.Core.Settings;

namespace InteractiveSeven.Core.Models
{
    public class GilBank
    {
        private ApplicationSettings Settings => ApplicationSettings.Instance;
        private Dictionary<string, Account> Accounts { get; } = new Dictionary<string, Account>();

        private readonly object _padlock = new object();

        public int Deposit(ChatUser user, int bits)
        {
            lock (_padlock)
            {
                var account = AccessAccount(user);
                account.Balance += bits;
                return account.Balance;
            }
        }

        public (int balance, int withdrawn) Withdraw(ChatUser user, int bits, bool requireBalance = false)
        {
            lock (_padlock)
            {
                var account = AccessAccount(user);
                if (requireBalance && account.Balance < bits)
                {
                    return (account.Balance, 0);
                }
                int withdrawn = Math.Min(account.Balance, bits);
                account.Balance -= withdrawn;
                return (account.Balance, withdrawn);
            }
        }

        public int CheckBalance(ChatUser user)
        {
            var account = AccessAccount(user);
            return account.Balance;
        }

        private Account AccessAccount(ChatUser user)
        {
            var account = Accounts.GetOrCreate(user.Username);

            if (user.IsSubscriber && !account.ReceivedSubBonus && Settings.GiveSubscriberBonusBits)
            {
                account.Balance += Settings.SubscriberBonusBits;
                account.ReceivedSubBonus = true;
            }

            return account;
        }
    }
}
