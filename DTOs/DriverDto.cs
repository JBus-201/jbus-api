namespace API.DTOs
{
    public class DriverDto
    {
        public int Id { get; set; }
        public UserDto? User { get; set; }
        public int BusId { get; set; }
    }
}