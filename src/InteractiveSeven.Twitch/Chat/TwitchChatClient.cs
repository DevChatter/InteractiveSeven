using InteractiveSeven.Core.Chat;
using TwitchLib.Client.Interfaces;

namespace InteractiveSeven.Twitch.Chat
{
    public class TwitchChatClient : IChatClient
    {
        private readonly ITwitchClient _twitchClient;

        public TwitchChatClient(ITwitchClient twitchClient)
        {
            _twitchClient = twitchClient;
        }

        public void SendMessage(string channel, string message)
        {
            _twitchClient.SendMessage(channel, message);
        }
    }
}
