namespace Booking_System.DTOS
{
    public class EventCreateDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int CategoryId { get; set; }
        public bool IsPublished { get; set; }
        public List<int> TagIds { get; set; } = new List<int>();
    }
}
