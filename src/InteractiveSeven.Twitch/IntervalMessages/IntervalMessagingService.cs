using InteractiveSeven.Core.IntervalMessages;
using InteractiveSeven.Core.Settings;
using System;
using TwitchLib.Client.Interfaces;

namespace InteractiveSeven.Twitch.IntervalMessages
{
    public class IntervalMessagingService : IIntervalMessagingService
    {
        private int _messageCount = 0;
        private DateTime _lastMessageTime = DateTime.UtcNow;
        private readonly ITwitchClient _twitchClient;

        private TimeSpan ElapsedTime => DateTime.UtcNow - _lastMessageTime;

        private TwitchSettings TwitchSettings => ApplicationSettings.Instance.TwitchSettings;

        public IntervalMessagingService(ITwitchClient twitchClient)
        {
            _twitchClient = twitchClient;
        }

        public void MessageReceived()
        {
            _messageCount++;
            if (_messageCount > 10 && ElapsedTime > TimeSpan.FromMinutes(30))
            {
                const string message = "Enjoying Interactive Seven? Consider supporting @DevChatter too! (https://twitch.tv/DevChatter)";
                _twitchClient.SendMessage(TwitchSettings.Channel, message);
                _messageCount = 0;
                _lastMessageTime = DateTime.UtcNow;
            }
        }
    }
}
