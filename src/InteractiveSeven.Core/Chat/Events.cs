using System;

namespace InteractiveSeven.Core.Chat
{
    public class OnLogArgs : EventArgs
    {
        public string Data { get; }
        public DateTime DateTime { get; }

        public OnLogArgs(string data, DateTime dateTime)
        {
            Data = data;
            DateTime = dateTime;
        }
    }

    public class OnJoinedChannelArgs : EventArgs
    {
        public OnJoinedChannelArgs(string channel)
        {
            Channel = channel;
        }

        public string Channel { get; }
    }

    public class OnMessageReceivedArgs : EventArgs
    {
        public OnMessageReceivedArgs(int bits, ChatUser chatUser, string text)
        {
            Bits = bits;
            ChatUser = chatUser;
            Text = text;
        }

        public int Bits { get; }
        public ChatUser ChatUser { get; }
        public string Text { get; }
    }

    public class OnChatCommandReceivedArgs : EventArgs
    {
        public OnChatCommandReceivedArgs(CommandData commandData)
        {
            CommandData = commandData;
        }

        public CommandData CommandData { get; }
    }

    public class OnConnectedArgs : EventArgs
    {
        public string Username { get; }

        public OnConnectedArgs(string username)
        {
            Username = username;
        }
    }

    public class OnDisconnectedEventArgs : EventArgs
    {
    }
}
