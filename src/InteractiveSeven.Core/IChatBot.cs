namespace InteractiveSeven.Core
{
    public interface IChatBot
    {
        bool IsConnected { get; }

        void Connect();
        void Disconnect();
    }
}