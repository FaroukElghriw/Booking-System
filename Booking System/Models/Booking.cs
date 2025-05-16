namespace Booking_System.Controllers
{
    public class Booking:BaseModel
    {
        public int EventId { get; set; }
        public int UserId { get; set; }
        public int TicketQuantity { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime BookingDate { get; set; }
        public string Status { get; set; } 
        public string PaymentMethod { get; set; }
        public string TransactionId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public Event Event { get; set; }
        public User User { get; set; }
    }
}
