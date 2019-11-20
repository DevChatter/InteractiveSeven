using InteractiveSeven.Core.Data;
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
        private readonly IDataStore<Account> _accountStore;

        public GilBank(IDataStore<Account> accountStore)
        {
            _accountStore = accountStore;
            Accounts = accountStore.LoadData() ?? new List<Account>();
        }

        private readonly HashSet<string> _knownUsers = new HashSet<string>();
        private ApplicationSettings Settings => ApplicationSettings.Instance;
        private List<Account> Accounts { get; }

        private readonly object _padlock = new object();

        public bool HasAccount(string username) => _knownUsers.Contains(username.ToLower().NoAt());

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
                _accountStore.SaveData(Accounts);
                return (account.Balance, withdrawn);
            }
        }

        public int CheckBalance(in ChatUser user)
        {
            lock (_padlock)
            {
                var account = AccessAccount(user);
                _accountStore.SaveData(Accounts);
                return account.Balance;
            }
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

            if (ShouldGiveSubBonus(user, account))
            {
                account.Balance += Settings.SubscriberBonusBits;
                account.LastSubBonus = DateTime.UtcNow;
            }

            return account;
        }

        private bool ShouldGiveSubBonus(ChatUser user, Account account)
        {
            return user.IsSubscriber
                   && Settings.GiveSubscriberBonusBits
                   && account.LastSubBonus.AddDays(1) < DateTime.UtcNow;
        }

        public void EnsureAccountExists(in ChatUser user)
        {
            bool newUser = _knownUsers.Add(user.Username.ToLower());
            if (newUser)
            {
                lock (_padlock)
                {
                    AccessAccount(user);
                    _accountStore.SaveData(Accounts);
                }
            }
        }
    }
}
