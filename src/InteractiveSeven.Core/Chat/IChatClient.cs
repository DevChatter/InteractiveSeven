using System;
using System.Threading.Tasks;

namespace InteractiveSeven.Core.Chat
{
    public interface IChatClient
    {
        Task SendMessage(string channel, string message);
        event EventHandler<OnLogArgs> OnLog;
        event EventHandler<OnJoinedChannelArgs> OnJoinedChannel;
        event EventHandler<OnMessageReceivedArgs> OnMessageReceived;
        event EventHandler<OnChatCommandReceivedArgs> OnChatCommandReceived;
        event EventHandler<OnConnectedArgs> OnConnected;
        event EventHandler<OnDisconnectedEventArgs> OnDisconnected;
        Task Connect(string username, string accessToken, string channel);
        Task Disconnect();
    }
}
