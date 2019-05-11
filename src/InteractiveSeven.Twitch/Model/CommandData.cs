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
                IsBroadcaster = chatCommand.ChatMessage.IsBroadcaster,
                IsMe = chatCommand.ChatMessage.IsMe,
                IsMod = chatCommand.ChatMessage.IsModerator,
                IsSubscriber = chatCommand.ChatMessage.IsSubscriber,
                Message = chatCommand.ChatMessage.Message,
                UserId = chatCommand.ChatMessage.UserId,
                Username = chatCommand.ChatMessage.Username
            };
        }

        public string CommandText { get; set; }
        public List<string> Arguments { get; set; }
        public int Bits { get; set; }
        public string Channel { get; set; }
        public string Message { get; set; }
        public bool IsBroadcaster { get; set; }
        public bool IsMe { get; set; }
        public bool IsMod { get; set; }
        public bool IsSubscriber { get; set; }
        public string UserId { get; set; }
        public string Username { get; set; }
    }
}