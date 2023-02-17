using InteractiveSeven.Core.Chat;
using TwitchOnChatCommandReceivedArgs = TwitchLib.Client.Events.OnChatCommandReceivedArgs;
using TwitchOnConnectedArgs = TwitchLib.Client.Events.OnConnectedArgs;
using TwitchOnDisconnectedEventArgs = TwitchLib.Communication.Events.OnDisconnectedEventArgs;
using TwitchOnJoinedChannelArgs = TwitchLib.Client.Events.OnJoinedChannelArgs;
using TwitchOnLogArgs = TwitchLib.Client.Events.OnLogArgs;
using TwitchOnMessageReceivedArgs = TwitchLib.Client.Events.OnMessageReceivedArgs;



namespace InteractiveSeven.Twitch.Events
{
    public static class EventExtensions
    {
        public static OnLogArgs ToCore(this TwitchOnLogArgs e)
        {
            return new OnLogArgs(e.Data, e.DateTime);
        }

        public static OnJoinedChannelArgs ToCore(this TwitchOnJoinedChannelArgs e)
        {
            return new OnJoinedChannelArgs(e.Channel);
        }

        public static OnMessageReceivedArgs ToCore(this TwitchOnMessageReceivedArgs e)
        {
            return new OnMessageReceivedArgs(e.ChatMessage.Bits, e.ChatMessage.GetChatUser());
        }

        public static OnChatCommandReceivedArgs ToCore(this TwitchOnChatCommandReceivedArgs e)
        {
            return new OnChatCommandReceivedArgs(e.Command.GetCommandData());
        }

        public static OnConnectedArgs ToCore(this TwitchOnConnectedArgs e)
        {
            return new OnConnectedArgs(e.BotUsername);
        }

        public static OnDisconnectedEventArgs ToCore(this TwitchOnDisconnectedEventArgs e)
        {
            return new OnDisconnectedEventArgs();
        }
    }
}
