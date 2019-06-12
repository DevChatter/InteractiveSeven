using FluentAssertions;
using InteractiveSeven.Core.Models;
using Xunit;

namespace UnitTests.Core.GilBankTests
{
    public class GilBankShould
    {
        [Theory]
        [InlineData(0)]
        [InlineData(25)]
        [InlineData(100)]
        public void ReturnBalanceAfterOneDeposit(int bits)
        {
            var bank = new GilBank();

            int balance = bank.Deposit("username", bits);

            balance.Should().Be(bits);
        }

        [Theory]
        [InlineData(0, 10, 10)]
        [InlineData(25, 25, 50)]
        [InlineData(100, 150, 250)]
        public void AddToBalanceWithEachDeposit(int first, int second, int expected)
        {
            var bank = new GilBank();

            bank.Deposit("username", first);
            int balance = bank.Deposit("username", second);

            balance.Should().Be(expected);
        }

        [Theory]
        [InlineData(0, 25, 0, 0)]
        [InlineData(12, 25, 12, 0)]
        [InlineData(25, 25, 25, 0)]
        [InlineData(100, 25, 25, 75)]
        public void ReturnBalanceAndWithdrawnAfterWithdrawal(int start, int withdraw, int expectWithdrawn, int expectBal)
        {
            const string Username = "username";
            var bank = new GilBank();

            bank.Deposit(Username, start);
            (int balance, int withdrawn) = bank.Withdraw(Username, withdraw);

            balance.Should().Be(expectBal);
            withdrawn.Should().Be(expectWithdrawn);
        }
    }
}
