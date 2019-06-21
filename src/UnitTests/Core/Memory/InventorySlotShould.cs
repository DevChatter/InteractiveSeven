using FluentAssertions;
using InteractiveSeven.Core.Memory;
using System;
using Xunit;

namespace UnitTests.Core.Memory
{
    public class InventorySlotShould
    {
        [Fact]
        public void BeOnes_AtCreation()
        {
            var inventorySlot = new InventorySlot();

            inventorySlot.AsBytes.Should().AllBeEquivalentTo(byte.MaxValue);
            inventorySlot.Value.Should().Be(ushort.MaxValue);
            Convert.ToString(inventorySlot.Value, 2).Should().Be("1111111111111111");
        }

        [Theory]
        [InlineData(0, "1111111110000000")]
        [InlineData(5, "1111111110000101")]
        [InlineData(10, "1111111110001010")]
        [InlineData(99, "1111111111100011")]
        [InlineData(100, "1111111111100100")]
        public void SetQuantityCorrectly(byte quantity, string expected)
        {
            var inventorySlot = new InventorySlot();

            inventorySlot.Quantity = quantity;

            Convert.ToString(inventorySlot.Value, 2).Should().Be(expected);
            inventorySlot.Quantity.Should().Be(quantity);
        }

        [Theory]
        [InlineData(0, "1111111")]
        [InlineData(5, "1011111111")]
        [InlineData(10, "10101111111")]
        [InlineData(99, "11000111111111")]
        [InlineData(511, "1111111111111111")]
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