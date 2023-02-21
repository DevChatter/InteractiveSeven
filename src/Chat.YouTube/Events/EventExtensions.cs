using Google.Apis.YouTube.v3.Data;
using InteractiveSeven.Core.Chat;

namespace Chat.YouTube.Events
{
    public static class EventExtensions
    {
        public static OnMessageReceivedArgs ToCore(this LiveChatMessage message)
        {
            return new OnMessageReceivedArgs(0, message.GetChatUser(), message.Snippet.DisplayMessage);
        }
    }
}
