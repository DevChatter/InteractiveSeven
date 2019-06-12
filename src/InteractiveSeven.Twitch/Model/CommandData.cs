using InteractiveSeven.Core.Model;
using System.Collections.Generic;
using TwitchLib.Client.Models;

namespace InteractiveSeven.Twitch.Model
{
    public class CommandData
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
                User = new ChatUser(chatCommand.ChatMessage.Username,
                    chatCommand.ChatMessage.UserId,
                    chatCommand.ChatMessage.IsBroadcaster,
                    chatCommand.ChatMessage.IsMe,
                    chatCommand.ChatMessage.IsModerator,
                    chatCommand.ChatMessage.IsSubscriber),
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