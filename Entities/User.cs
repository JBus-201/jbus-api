namespace API.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public Role Role { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime LastActive { get; set; }
        public string? PasswordHash { get; set; }
        public Sex Sex { get; set; }
        public DateOnly DateOfBirth { get; set; }

        // * Link
        public virtual Passenger? Passenger { get; set; }
        public virtual Driver? Driver { get; set; }
        public virtual Admin? Admin { get; set; }
    }
}