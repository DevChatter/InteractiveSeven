namespace InteractiveSeven.Core
{
    public interface IChatBot
    {
        bool IsConnected { get; set; }

        void Connect();
        void Disconnect();
    }
}