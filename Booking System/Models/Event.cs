namespace Booking_System.Controllers
{
    public class Event : BaseModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Location { get; set; }
        public int Capacity { get; set; }
        public decimal Price { get; set; }
        public bool IsPublished { get; set; }
        public string ImageUrl { get; set; }
        public int CreatorId { get; set; }
        public int CategoryId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

    
        public User Creator { get; set; }
        public Category Category { get; set; }
        public ICollection<TagEvent> EventTags { get; set; }
        public ICollection<Booking> Bookings { get; set; }

    }
}
