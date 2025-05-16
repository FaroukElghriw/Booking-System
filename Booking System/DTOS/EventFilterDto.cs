namespace Booking_System.DTOS
{
    public class EventFilterDto
    {
        public string SearchTerm { get; set; }
        public int? CategoryId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool PublishedOnly { get; set; } = true;
        public int? CreatorId { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
