namespace API.Entities
{
    public class ChargingTransaction
    {
        public int Id { get; set; }
        public ChargingMethod ChargingMethod { get; set; }
        public double Amount { get; set; }
        public DateTime TimeStamp { get; set; }

        // * Link
        public int? PassengerId { get; set; }
        public virtual Passenger? Passenger { get; set; }
    }
}