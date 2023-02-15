using System.Collections.Generic;
using InteractiveSeven.Core.Model;
using TwitchLib.Client.Models;

namespace InteractiveSeven.Core.Models
{
    public struct CommandData
    {
        public static CommandData FromChatCommand(ChatCommand chatCommand)
        {
            return new CommandData
            {
                Arguments = chatCommand.ArgumentsAsList,
                Bits = chatCommand.ChatMessage.Bits,
                Channel = chatCommand.ChatMessage.Channel,
                CommandText = chatCommand.CommandText,
                Message = chatCommand.ChatMessage.Message,
                User = ChatUser.FromChatMessage(chatCommand.ChatMessage)
            };
        }

        public string CommandText { get; set; }
        public List<string> Arguments { get; set; }
        public int Bits { get; set; }
        public string Channel { get; set; }
        public string Message { get; set; }
        public ChatUser User { get; set; }
    }
}
