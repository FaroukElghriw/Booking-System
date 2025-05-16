namespace Booking_System.Controllers
{
    public class User:BaseModel
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? LastLogin { get; set; }
        public int RoleId { get; set; }

        // Navigation properties
        public Role Role { get; set; }
        public ICollection<Event> CreatedEvents { get; set; }
        public ICollection<Booking> Bookings { get; set; }
    }
}
