using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace API.DTOs
{
    public class BusCreateDto
    {
        [Required]
        public string? BusNumber { get; set; }
        [Required]
        public int Capacity { get; set; }
        // [Required]
        public int RouteId { get; set; }
        // [Required]
        public int DriverId { get; set; }
        [JsonIgnore]
        public DateTime CreatedAt { get; set; }
        [JsonIgnore]
        public DateTime UpdatedAt { get; set; }

    }
}