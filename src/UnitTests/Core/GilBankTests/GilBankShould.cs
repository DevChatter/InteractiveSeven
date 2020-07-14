using FluentAssertions;
using InteractiveSeven.Core.Model;
using InteractiveSeven.Core.Models;
using InteractiveSeven.Core.Payments;
using System.Collections.Generic;
using Xunit;

namespace UnitTests.Core.GilBankTests
{
    public class GilBankShould
    {
        private readonly ChatUser _user = new ChatUser("any", "123456");
        private readonly GilBank _bank;

        public GilBankShould()
        {
            _bank = new GilBank(new TestMemoryDataStore(new List<Account>()));
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(-25)]
        [InlineData(-100)]
        public void IgnoreNegativeDeposits(int bits)
        {
            int balance = _bank.Deposit(_user, 100);
            int result = _bank.Deposit(_user, bits);

            result.Should().Be(balance);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(-25)]
        [InlineData(-100)]
        public void IgnoreNegativeWithdraws(int bits)
        {
            int balance = _bank.Deposit(_user, 100);
            (int result, int withdrawn) = _bank.Withdraw(_user, bits);

            result.Should().Be(balance);
            withdrawn.Should().Be(0);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(25)]
        [InlineData(100)]
        public void ReturnBalanceAfterOneDeposit(int bits)
        {
            int balance = _bank.Deposit(_user, bits);

            balance.Should().Be(bits);
        }

        [Theory]
        [InlineData(0, 10, 10)]
        [InlineData(25, 25, 50)]
        [InlineData(100, 150, 250)]
        public void AddToBalanceWithEachDeposit(int first, int second, int expected)
        {
            _bank.Deposit(_user, first);
            int balance = _bank.Deposit(_user, second);

            balance.Should().Be(expected);
        }

        [Theory]
        [InlineData(0, 25, 0, 0)]
        [InlineData(12, 25, 12, 0)]
        [InlineData(25, 25, 25, 0)]
        [InlineData(100, 25, 25, 75)]
        public void ReturnBalanceAndWithdrawnAfterWithdrawal(int start, int withdraw, int expectWithdrawn, int expectBal)
        {
            _bank.Deposit(_user, start);
            (int balance, int withdrawn) = _bank.Withdraw(_user, withdraw);

            balance.Should().Be(expectBal);
            withdrawn.Should().Be(expectWithdrawn);
        }

        [Theory]
        [InlineData(12, 25)]
        [InlineData(25, 26)]
        [InlineData(100, 200)]
        public void ReturnBalanceAndZeroForRequireFullWithdraw(int start, int withdraw)
        {
            _bank.Deposit(_user, start);
            (int balance, int withdrawn) = _bank.Withdraw(_user, withdraw, true);

            balance.Should().Be(start);
            withdrawn.Should().Be(0);
        }

        [Fact]
        public void FindAccountGivenOnlyName()
        {
            _bank.Deposit(_user, 10);
            (int balance, int withdrawn) = _bank.Withdraw(new ChatUser(_user.Username, null), 5, true);

            balance.Should().Be(5);
            withdrawn.Should().Be(5);
        }

        [Fact]
        public void FindAccountCreatedWithNameOnly()
        {
            _bank.Deposit(new ChatUser(_user.Username, null), 100);
            (int balance, int withdrawn) = _bank.Withdraw(_user, 5, true);

            balance.Should().Be(95);
            withdrawn.Should().Be(5);

            (balance, withdrawn) = _bank.Withdraw(new ChatUser(null, _user.UserId), 5, true);

            balance.Should().Be(90);
            withdrawn.Should().Be(5);
        }
    }
}
