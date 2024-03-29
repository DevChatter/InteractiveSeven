﻿namespace InteractiveSeven.Core.Chat
{
    public struct ChatUser
    {
        public ChatUser(string username, string userId,
            bool isBroadcaster = false, bool isMe = false, bool isMod = false, bool isSubscriber = false)
        {
            Username = username.NoAt();
            UserId = userId;
            IsBroadcaster = isBroadcaster;
            IsMe = isMe;
            IsMod = isMod;
            IsSubscriber = isSubscriber;
        }

        public string SafeUsername => Username?.NoAt()?.ToLower();
        public string Username { get; set; }
        public string UserId { get; set; }
        public bool IsBroadcaster { get; set; }
        public bool IsMe { get; set; }
        public bool IsDevChatter => UserId == "188854137" || UserId == "57245338";
        public bool IsShojy => UserId == "29477956";
        public bool IsMod { get; set; }
        public bool IsSubscriber { get; set; }
    }
}
