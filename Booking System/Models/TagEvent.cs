namespace Booking_System.Controllers
{
    public class TagEvent:BaseModel
    {
        public int TagId { get; set; }

      
        public Event Event { get; set; }
        public Tag Tag { get; set; }

    }
}
