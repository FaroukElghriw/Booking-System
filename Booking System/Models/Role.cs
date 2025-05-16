namespace Booking_System.Controllers
{
    public class Role:BaseModel
    {
        public string Name { get; set; }
        public string Description { get; set; }

        // Navigation property
        public ICollection<User> Users { get; set; }
    }
}
