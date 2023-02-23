using System;
using System.Threading.Tasks;
using InteractiveSeven.Core.Chat;
using InteractiveSeven.Core.Settings;

namespace InteractiveSeven.Core.IntervalMessages
{
    public class IntervalMessagingService
    {
        private const int NormalDelayInMinutes = 60;
        private int _messageCount = 0;
        private DateTime _lastMessageTime;
        private readonly IChatClient _chatClient;
        private readonly IClock _clock;

        private TimeSpan ElapsedTime => _clock.UtcNow - _lastMessageTime;

        private TwitchSettings TwitchSettings => TwitchSettings.Instance;

        public IntervalMessagingService(IChatClient chatClient, IClock clock)
        {
            _chatClient = chatClient;
            _clock = clock;
            _lastMessageTime = _clock.UtcNow.AddMinutes(NormalDelayInMinutes - (NormalDelayInMinutes * 1.5));
        }

        public async Task MessageReceived()
        {
            _messageCount++;
            if (_messageCount > 10 && ElapsedTime > TimeSpan.FromMinutes(NormalDelayInMinutes))
            {
                const string message = "Enjoying Interactive Seven? Consider supporting the developers, @DevChatter and @MrShojy ! (https://twitch.tv/DevChatter)";
                await _chatClient.SendMessage(TwitchSettings.Channel, message);
                _messageCount = 0;
                _lastMessageTime = _clock.UtcNow;
            }
        }
    }
}
