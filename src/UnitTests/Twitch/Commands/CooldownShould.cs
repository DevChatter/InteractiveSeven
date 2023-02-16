using System;
using FluentAssertions;
using InteractiveSeven.Core;
using InteractiveSeven.Core.Commands;
using InteractiveSeven.Core.Model;
using Moq;
using Xunit;

namespace UnitTests.Twitch.Commands
{
    public class CooldownShould
    {
        [Fact]
        public void BeReady_WhenCreated()
        {
            var tracker = new CooldownTracker(1, MakeMock(out Mock<IClock> mock));
            mock.Setup(x => x.UtcNow).Returns(DateTime.UtcNow);

            tracker.IsReady.Should().BeTrue();
        }

        [Fact]
        public void NotBeReady_AfterUsed()
        {
            var tracker = new CooldownTracker(1, MakeMock(out Mock<IClock> mock));
            mock.Setup(x => x.UtcNow).Returns(DateTime.UtcNow);

            tracker.Run(new ChatUser("UserName", "UserId"));

            tracker.IsReady.Should().BeFalse();
        }

        [Fact]
        public void BeReady_AfterWaiting()
        {
            const int minutes = 1;
            var tracker = new CooldownTracker(minutes, MakeMock(out Mock<IClock> mock));
            mock.Setup(x => x.UtcNow).Returns(DateTime.UtcNow);

            tracker.Run(new ChatUser("UserName", "UserId"));

            mock.Setup(x => x.UtcNow).Returns(DateTime.UtcNow.AddMinutes(minutes));

            tracker.IsReady.Should().BeTrue();
        }

        private T MakeMock<T>(out Mock<T> mock) where T : class
        {
            mock = new Mock<T>();
            return mock.Object;
        }
    }
}
