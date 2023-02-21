using Google.Apis.YouTube.v3.Data;
using InteractiveSeven.Core.Chat;

namespace Chat.YouTube
{
    public static class LiveChatMessageExtensions
    {
        public static ChatUser GetChatUser(this LiveChatMessage message)
        {
            return new ChatUser(message.AuthorDetails.DisplayName,
                message.AuthorDetails.ChannelId,
                message.AuthorDetails.IsChatOwner ?? false,
                false,
                message.AuthorDetails.IsChatModerator ?? false,
                message.AuthorDetails.IsChatSponsor ?? false);
        }
    }
}
