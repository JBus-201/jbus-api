namespace API.DTOs
{
    public class TripUpdateDto
    {
        public DateTime FinishedAt { get; set; }
        public string? Status { get; set; }
        public int PaymentTransactionId { get; set; }
        public PointCreateDto? PickUpPoint { get; set; }
        public PointCreateDto? DropOffPoint { get; set; }
    }
}