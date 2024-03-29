using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace API.DTOs;

public class UserUpdateDto
{
    public string? Name { get; set; }
    public string? PhoneNumber { get; set; }
    // TODO TO REMEMBER [RegularExpression(@"^[A-Za-z0-9_-]*$")]
    [EmailAddress(ErrorMessage = "Invalid email address")]
    public string? Email { get; set; }
    [JsonIgnore]
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    [JsonIgnore]
    public DateTime LastActive { get; set; } = DateTime.UtcNow;
    public string? Sex { get; set; }
}
