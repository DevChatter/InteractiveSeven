namespace InteractiveSeven.Core.Chat
{
    public interface IChatClient
    {
        void SendMessage(string channel, string message);
    }
}
