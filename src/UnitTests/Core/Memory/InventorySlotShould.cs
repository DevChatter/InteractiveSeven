using FluentAssertions;
using InteractiveSeven.Core.Memory;
using InteractiveSeven.Core.Memory.Model;
using Xunit;

namespace UnitTests.Core.Memory
{
    public class InventorySlotShould
    {
        // 0b_IIII_IIII, 0b_QQQQ_QQQI
        [Theory]
        [InlineData(  0,  1, new byte[] {0b_0000_0000, 0b_0000001_0})]
        [InlineData( 64,  2, new byte[] {0b_0100_0000, 0b_0000010_0})]
        [InlineData(100, 20, new byte[] {0b_0110_0100, 0b_0010100_0})]
        [InlineData(256, 64, new byte[] {0b_0000_0000, 0b_1000000_1})]
        [InlineData(260, 80, new byte[] {0b_0000_0100, 0b_1010000_1})]
        [InlineData(300, 99, new byte[] {0b_0010_1100, 0b_1100011_1})]
        public void DoSomething(ushort itemId, ushort quantity, byte[] bytes)
        {
            var fromValues = new InventorySlot(itemId, quantity);

            byte[] asBytes = fromValues.AsBytes();
            asBytes[0].Should().Be(bytes[0]);
            asBytes[1].Should().Be(bytes[1]);

            var fromBytes = new InventorySlot(bytes);

            fromBytes.ItemId.Should().Be(itemId);
            fromBytes.Quantity.Should().Be(quantity);
        }
    }
}