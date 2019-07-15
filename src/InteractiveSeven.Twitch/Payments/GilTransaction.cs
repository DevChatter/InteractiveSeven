namespace InteractiveSeven.Twitch.Payments
{
    public class GilTransaction
    {
        public bool Paid { get; }
        public int AmountPaid { get; }

        public GilTransaction(bool paid, int amountPaid)
        {
            Paid = paid;
            AmountPaid = amountPaid;
        }
    }
}