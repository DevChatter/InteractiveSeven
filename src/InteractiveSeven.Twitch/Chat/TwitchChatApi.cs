using System.Collections.Generic;
using System.Linq;
using InteractiveSeven.Core.Chat;
using TwitchLib.Api.Interfaces;

namespace InteractiveSeven.Twitch.Chat
{
    public class TwitchChatApi : IChatApi
    {
        private readonly ITwitchAPI _twitchApi;

        public TwitchChatApi(ITwitchAPI twitchApi)
        {
            _twitchApi = twitchApi;
        }

        public bool IsValidUsername(string username)
        {
            var response = _twitchApi.Helix.Users.GetUsersAsync(logins: new List<string> { username }).Result;

            return response?.Users?.Any() ?? false;
        }
    }
}
