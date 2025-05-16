namespace Booking_System.Controllers
{
    public class Tag:BaseModel
    {
        public string Name { get; set; }

        
        public ICollection<TagEvent> EventTags { get; set; }
    }
}
