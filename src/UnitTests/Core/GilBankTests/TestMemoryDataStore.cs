using System.Collections.Generic;
using InteractiveSeven.Core.Data;
using InteractiveSeven.Core.Models;

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