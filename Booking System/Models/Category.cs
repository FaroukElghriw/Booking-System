namespace Booking_System.Controllers
{
    public class Category: BaseModel
    {
        public string Name { get; set; }
        public string Description { get; set; }

        
        public ICollection<Event> Events { get; set; }
    }
}
