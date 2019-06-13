using TwitchLib.Client.Models;

namespace InteractiveSeven.Core.Model
{
    public class ChatUser
    {
        public ChatUser()
        {
        }

        public ChatUser(string username, string userId,
            bool isBroadcaster = false, bool isMe = false, bool isMod = false, bool isSubscriber = false)
        {
            Username = username;
            UserId = userId;
            IsBroadcaster = isBroadcaster;
            IsMe = isMe;
            IsMod = isMod;
            IsSubscriber = isSubscriber;
        }

        public static ChatUser FromChatMessage(ChatMessage message)
        {
            return new ChatUser
            {
                Username = message.Username,
                UserId = message.UserId,
                IsBroadcaster = message.IsBroadcaster,
                IsMe = message.IsMe,
                IsMod = message.IsModerator,
                IsSubscriber = message.IsSubscriber,
            };
        }

        public string Username { get; set; }
        public string UserId { get; set; }
        public bool IsBroadcaster { get; set; }
        public bool IsMe { get; set; }
        public bool IsMod { get; set; }
        public bool IsSubscriber { get; set; }
    }
}