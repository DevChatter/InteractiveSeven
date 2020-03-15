using FluentAssertions;
using InteractiveSeven.Core.FinalFantasy;
using Xunit;

namespace UnitTests.Core.LetterMappingTests
{
    public class MapFf7BytesToStringShould
    {
        [Theory]
        [InlineData("Cloud", new byte[] { 35, 76, 79, 85, 68, 255, 255, 255, 255, 255 })]
        [InlineData("CCCC CCCC", new byte[] { 35, 35, 35, 35, 0, 35, 35, 35, 35, 255 })]
        [InlineData("0123456789", new byte[] { 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 255 })]
        [InlineData("Turks:Reno", new byte[] { 52, 85, 82, 75, 83, 26, 50, 69, 78, 79, 255 })]
        [InlineData("Pyramid ", new byte[] { 48, 89, 82, 65, 77, 73, 68, 0, 255 })]
        [InlineData("Grenade Combatant ", new byte[] { 39, 82, 69, 78, 65, 68, 69, 0, 35, 79, 77, 66, 65, 84, 65, 78, 84, 0, 255 })]
        public void ReturnCorrectString(string expectedText, byte[] bytes)
        {
            var result = bytes.MapFf7BytesToString(bytes.Length);

            result.Should().Be(expectedText);
        }
    }
}