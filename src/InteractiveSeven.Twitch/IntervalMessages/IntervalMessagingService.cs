using InteractiveSeven.Core;
using InteractiveSeven.Core.IntervalMessages;
using InteractiveSeven.Core.Settings;
using System;
using TwitchLib.Client.Interfaces;

namespace InteractiveSeven.Twitch.IntervalMessages
{
    public class IntervalMessagingService : IIntervalMessagingService
    {
        private const int NormalDelayInMinutes = 60;
        private int _messageCount = 0;
        private DateTime _lastMessageTime;
        private readonly ITwitchClient _twitchClient;
        private readonly IClock _clock;

        private TimeSpan ElapsedTime => _clock.UtcNow - _lastMessageTime;

        private TwitchSettings TwitchSettings => ApplicationSettings.Instance.TwitchSettings;

        public IntervalMessagingService(ITwitchClient twitchClient, IClock clock)
        {
            _twitchClient = twitchClient;
            _clock = clock;
            _lastMessageTime = _clock.UtcNow.AddMinutes(NormalDelayInMinutes - (NormalDelayInMinutes * 1.5));
        }

        public void MessageReceived()
        {
            _messageCount++;
            if (_messageCount > 10 && ElapsedTime > TimeSpan.FromMinutes(NormalDelayInMinutes))
            {
                const string message = "Enjoying Interactive Seven? Consider supporting @DevChatter too! (https://twitch.tv/DevChatter)";
                _twitchClient.SendMessage(TwitchSettings.Channel, message);
                _messageCount = 0;
                _lastMessageTime = _clock.UtcNow;
            }
        }
    }
}
