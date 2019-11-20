using InteractiveSeven.Core.Data;
using InteractiveSeven.Core.Models;
using System.Collections.Generic;

namespace UnitTests.Core.GilBankTests
{
    public class TestMemoryDataStore : IDataStore<Account>
    {
        private List<Account> _startingAccounts;

        public TestMemoryDataStore(List<Account> startingAccounts)
        {
            _startingAccounts = startingAccounts;
        }

        public void SaveData(List<Account> items)
        {
            _startingAccounts = items;
        }

        public List<Account> LoadData()
        {
            return _startingAccounts;
        }
    }
}