using System;

namespace InteractiveSeven.Core.Chat
{
    public interface IChatClient
    {
        void SendMessage(string channel, string message);
        event EventHandler<OnLogArgs> OnLog;
        event EventHandler<OnJoinedChannelArgs> OnJoinedChannel;
        event EventHandler<OnMessageReceivedArgs> OnMessageReceived;
        event EventHandler<OnChatCommandReceivedArgs> OnChatCommandReceived;
        event EventHandler<OnConnectedArgs> OnConnected;
        event EventHandler<OnDisconnectedEventArgs> OnDisconnected;
        void Connect(string username, string accessToken, string channel);
        void Disconnect();
    }
}
