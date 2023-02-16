using InteractiveSeven.Core.Models;
using TwitchLib.Client.Models;

namespace InteractiveSeven.Twitch
{
    public static class ChatMessageExtensions
    {
        public static ChatUser GetChatUser(this ChatMessage message)
        {
            return new ChatUser(message.Username, message.UserId,
                message.IsBroadcaster, message.IsMe,
                message.IsModerator, message.IsSubscriber);
        }
    }
}
