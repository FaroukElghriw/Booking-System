using Booking_System.Controllers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Booking_System.DataStore
{
    public class BookingConfiguration : IEntityTypeConfiguration<Booking>
    {
        public void Configure(EntityTypeBuilder<Booking> builder)
        {
            builder.HasKey(b => b.Id);

            builder.Property(b => b.TicketQuantity)
                .IsRequired();

            builder.Property(b => b.TotalPrice)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder.Property(b => b.BookingDate)
                .IsRequired();

            builder.Property(b => b.Status)
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(b => b.PaymentMethod)
                .HasMaxLength(50);

            builder.Property(b => b.TransactionId)
                .HasMaxLength(100);

            builder.Property(b => b.CreatedAt)
                .IsRequired()
                .HasDefaultValueSql("GETDATE()");

      
            builder.HasIndex(b => b.BookingDate);
            builder.HasIndex(b => b.Status);
        }
    }
}
