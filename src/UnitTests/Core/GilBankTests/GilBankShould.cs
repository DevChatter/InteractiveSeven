using FluentAssertions;
using InteractiveSeven.Core.Model;
using InteractiveSeven.Core.Models;
using Xunit;

namespace UnitTests.Core.GilBankTests
{
    public class GilBankShould
    {
        private readonly ChatUser _user = new ChatUser("any", "123456", false, false, false, false);

        [Theory]
        [InlineData(0)]
        [InlineData(25)]
        [InlineData(100)]
        public void ReturnBalanceAfterOneDeposit(int bits)
        {
            var bank = new GilBank();

            int balance = bank.Deposit(_user, bits);

            balance.Should().Be(bits);
        }

        [Theory]
        [InlineData(0, 10, 10)]
        [InlineData(25, 25, 50)]
        [InlineData(100, 150, 250)]
        public void AddToBalanceWithEachDeposit(int first, int second, int expected)
        {
            var bank = new GilBank();

            bank.Deposit(_user, first);
            int balance = bank.Deposit(_user, second);

            balance.Should().Be(expected);
        }

        [Theory]
        [InlineData(0, 25, 0, 0)]
        [InlineData(12, 25, 12, 0)]
        [InlineData(25, 25, 25, 0)]
        [InlineData(100, 25, 25, 75)]
        public void ReturnBalanceAndWithdrawnAfterWithdrawal(int start, int withdraw, int expectWithdrawn, int expectBal)
        {
            var bank = new GilBank();

            bank.Deposit(_user, start);
            (int balance, int withdrawn) = bank.Withdraw(_user, withdraw);

            balance.Should().Be(expectBal);
            withdrawn.Should().Be(expectWithdrawn);
        }

        [Theory]
        [InlineData(12, 25)]
        [InlineData(25, 26)]
        [InlineData(100, 200)]
        public void ReturnBalanceAndZeroForRequireFullWithdraw(int start, int withdraw)
        {
            var bank = new GilBank();

            bank.Deposit(_user, start);
            (int balance, int withdrawn) = bank.Withdraw(_user, withdraw, true);

            balance.Should().Be(start);
            withdrawn.Should().Be(0);
        }
    }
}
