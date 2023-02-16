using System.Collections.Generic;

namespace InteractiveSeven.Core.Chat
{
    public struct CommandData
    {
        public string CommandText { get; set; }
        public List<string> Arguments { get; set; }
        public int Bits { get; set; }
        public string Channel { get; set; }
        public string Message { get; set; }
        public ChatUser User { get; set; }
    }
}
