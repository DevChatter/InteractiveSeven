using FluentAssertions;
using InteractiveSeven.Core.Memory;
using System;
using Xunit;

namespace UnitTests.Core.Memory
{
    public class InventorySlotShould
    {
        [Fact]
        public void BeZeros_AtCreation()
        {
            var inventorySlot = new InventorySlot();

            inventorySlot.AsBytes.Should().AllBeEquivalentTo(0);
            inventorySlot.Value.Should().Be(0);
            Convert.ToString(inventorySlot.Value, 2).Should().Be("0");
        }

        [Theory]
        [InlineData(0, "0")]
        [InlineData(5, "101")]
        [InlineData(10, "1010")]
        [InlineData(99, "1100011")]
        [InlineData(100, "1100100")]
        public void SetQuantityCorrectly(byte quantity, string expected)
        {
            var inventorySlot = new InventorySlot();

            inventorySlot.Quantity = quantity;

            Convert.ToString(inventorySlot.Value, 2).Should().Be(expected);
            inventorySlot.Quantity.Should().Be(quantity);
        }

        [Theory]
        [InlineData(0, "0")]
        [InlineData(5, "1010000000")]
        [InlineData(10, "10100000000")]
        [InlineData(99, "11000110000000")]
        [InlineData(511, "1111111110000000")]
        public void SetItemIdCorrectly(ushort itemId, string expected)
        {
            var inventorySlot = new InventorySlot();

            inventorySlot.ItemId = itemId;

            Convert.ToString(inventorySlot.Value, 2).Should().Be(expected);
            inventorySlot.ItemId.Should().Be(itemId);
        }

        [Theory]
        [InlineData(0, 1, "1")]
        [InlineData(5, 5, "1010000101")]
        [InlineData(10, 10, "10100001010")]
        [InlineData(99, 99, "11000111100011")]
        [InlineData(511, 99, "1111111111100011")]
        public void SetEverythingCorrectly(ushort itemId, byte quantity, string expected)
        {
            var inventorySlot = new InventorySlot();

            inventorySlot.ItemId = itemId;
            inventorySlot.Quantity = quantity;

            Convert.ToString(inventorySlot.Value, 2).Should().Be(expected);
            inventorySlot.ItemId.Should().Be(itemId);
            inventorySlot.Quantity.Should().Be(quantity);
        }
    }
}