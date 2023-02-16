using System;
using InteractiveSeven.Core.Models;

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
        public OnMessageReceivedArgs(int bits, ChatUser chatUser)
        {
            Bits = bits;
            ChatUser = chatUser;
        }

        public int Bits { get; }
        public ChatUser ChatUser { get; }
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
        public string Channel { get; }
        public string Username { get; }

        public OnConnectedArgs(string channel, string username)
        {
            Channel = channel;
            Username = username;
        }
    }

    public class OnDisconnectedEventArgs : EventArgs
    {
    }
}
