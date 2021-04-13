using System;
using System.Collections.Generic;
using System.Linq;
using InteractiveSeven.Core.Data;
using InteractiveSeven.Core.Models;
using InteractiveSeven.Core.Settings;

namespace InteractiveSeven.Core.Payments
{
    public class GilBank
    {
        private readonly IDataStore<Account> _accountStore;

        public GilBank(IDataStore<Account> accountStore)
        {
            _accountStore = accountStore;
            List<Account> accounts = accountStore.LoadData() ?? new List<Account>();
            Accounts = accounts;
            AccountsByName = accounts.Where(x => x.Username != null).ToDictionary(x => x.Username);
            AccountsById = accounts.Where(x => x.UserId != null).ToDictionary(x => x.UserId);
        }

        private ApplicationSettings Settings => ApplicationSettings.Instance;
        private List<Account> Accounts { get; }
        private Dictionary<string, Account> AccountsByName { get; }
        private Dictionary<string, Account> AccountsById { get; }

        private readonly object _padlock = new object();

        public bool HasAccount(ChatUser user) => AccountsByName.ContainsKey(user.SafeUsername)
                                                 || AccountsById.ContainsKey(user.UserId);

        public int Deposit(in ChatUser user, int bits)
        {
            bits = Math.Max(0, bits);
            lock (_padlock)
            {
                var account = AccessAccount(user);
                account.Balance += bits;
                _accountStore.SaveData(Accounts);
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

        private Account AccessAccount(ChatUser user)
        {
            Account account;
            if ((user.UserId == null || !AccountsById.TryGetValue(user.UserId, out account))
                && (user.SafeUsername == null || !AccountsByName.TryGetValue(user.SafeUsername, out account)))
            {
                account = CreateAccount(user);
            }
            else if (user.UserId != null && account.UserId != user.UserId)
            {
                account.UserId = user.UserId;
                AccountsById.Add(user.UserId, account);
            }
            else if (user.Username != null && !account.Username.EqualsIns(user.SafeUsername))
            {
                account.Username = user.Username.NoAt();
                AccountsByName.Add(user.SafeUsername, account);
            }

            if (ShouldGiveSubBonus(user, account))
            {
                account.Balance += Settings.SubscriberBonusBits;
                account.LastSubBonus = DateTime.UtcNow;
            }

            return account;
        }

        private Account CreateAccount(ChatUser user)
        {
            var account = new Account(user.UserId, user.SafeUsername);
            Accounts.Add(account);
            if (user.UserId != null)
            {
                AccountsById.Add(user.UserId, account);
            }

            if (user.SafeUsername != null)
            {
                AccountsByName.Add(user.SafeUsername, account);
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
            bool newUser = !AccountsByName.ContainsKey(user.SafeUsername)
                && !AccountsById.ContainsKey(user.UserId);
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
