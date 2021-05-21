using System.Drawing;
using FluentAssertions;
using InteractiveSeven.Core;
using Xunit;

namespace UnitTests.Core.Extensions.ColorExtensions
{
    public class ToHexStringShould
    {
        [Fact]
        public void ReturnKnownColorsAsHex()
        {
            Color.Red.ToHexString().Should().Be("#FF0000");
            Color.Lime.ToHexString().Should().Be("#00FF00");
            Color.Blue.ToHexString().Should().Be("#0000FF");

            Color.Gray.ToHexString().Should().Be("#808080");
            Color.Black.ToHexString().Should().Be("#000000");
            Color.White.ToHexString().Should().Be("#FFFFFF");
        }

        [Fact]
        public void ReturnRGBColorsAsHex()
        {
            Color.FromArgb(4, 253, 255).ToHexString().Should().Be("#04FDFF");
            Color.FromArgb(16, 00, 32).ToHexString().Should().Be("#100020");
        }
    }
}
