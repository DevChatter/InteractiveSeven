using FluentAssertions;
using InteractiveSeven.Core.FinalFantasy;
using Xunit;

namespace UnitTests.Core.LetterMappingTests
{
    public class MapStringToFf7BytesShould
    {
        [Theory]
        [InlineData("Cloud", new byte[] { 35, 76, 79, 85, 68, 255, 255, 255, 255, 255 })]
        [InlineData("CCCC CCCC", new byte[] { 35, 35, 35, 35, 0, 35, 35, 35, 35, 255 })]
        [InlineData("0123456789", new byte[] { 16, 17, 18, 19, 20, 21, 22, 23, 24, 255 })]
        public void DoSomething_GivenSomething(string text, byte[] expectedBytes)
        {
            var result = text.MapStringToFf7Bytes();

            result.Should().ContainInOrder(expectedBytes);
        }
    }
}
