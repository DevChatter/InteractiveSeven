namespace InteractiveSeven.Core.Commands.Bidding
{
    public class BidRecord
    {
        public BidRecord(string username, string userId, int bits)
        {
            Username = username;
            UserId = userId;
            Bits = bits;
        }

        public string Username { get; set; }
        public string UserId { get; set; }
        public int Bits { get; set; }
    }
}