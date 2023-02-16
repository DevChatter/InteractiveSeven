using InteractiveSeven.Core.Models;
using TwitchLib.Client.Models;

namespace InteractiveSeven.Twitch
{
    public static class ChatCommandExtensions
    {
        public static CommandData GetCommandData(this ChatCommand chatCommand)
        {
            return new CommandData
            {
                Arguments = chatCommand.ArgumentsAsList,
                Bits = chatCommand.ChatMessage.Bits,
                Channel = chatCommand.ChatMessage.Channel,
                CommandText = chatCommand.CommandText,
                Message = chatCommand.ChatMessage.Message,
                User = chatCommand.ChatMessage.GetChatUser()
            };

        }
    }
}
