namespace InteractiveSeven.Core.Model
{
    public class ChatUser
    {
        public ChatUser()
        {
        }

        public ChatUser(string username, string userId,
            bool isBroadcaster, bool isMe, bool isMod, bool isSubscriber)
        {
            Username = username;
            UserId = userId;
            IsBroadcaster = isBroadcaster;
            IsMe = isMe;
            IsMod = isMod;
            IsSubscriber = isSubscriber;
        }

        public string Username { get; set; }
        public string UserId { get; set; }
        public bool IsBroadcaster { get; set; }
        public bool IsMe { get; set; }
        public bool IsMod { get; set; }
        public bool IsSubscriber { get; set; }
    }
}