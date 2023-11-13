namespace API.Entities
{
    public class ChargingTransaction
    {
        public int Id { get; set; }
        // TODO enum
        public enum PaymentMethod
        {
            MASTERCARD = 0,
            VISA = 1
        }
        public double Amount { get; set; }
        public DateTime TimeStamp { get; set; }

        // * Link
        public int PassengerId { get; set; }
        public Passenger Passenger { get; set; }
    }
}