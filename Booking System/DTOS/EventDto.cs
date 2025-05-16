namespace Booking_System.DTOS
{
    public class EventDto
    {
        public int EventId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string ImageUrl { get; set; }
        public bool IsPublished { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public CategoryDto Category { get; set; }
       // public UserDto Creator { get; set; }
        public List<TagDto> Tags { get; set; } = new List<TagDto>();
    }
}
