using Booking_System.Controllers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Booking_System.DataStore
{
    public class EventConfiguration : IEntityTypeConfiguration<Event>
    {
        public void Configure(EntityTypeBuilder<Event> builder)
        {

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Title)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(e => e.Description)
                .IsRequired();

            builder.Property(e => e.StartDate)
                .IsRequired();

            builder.Property(e => e.EndDate)
                .IsRequired();

            builder.Property(e => e.Location)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(e => e.Capacity)
                .IsRequired();

            builder.Property(e => e.Price)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder.Property(e => e.IsPublished)
                .IsRequired()
                .HasDefaultValue(false);

            builder.Property(e => e.ImageUrl)
                .HasMaxLength(500);

            builder.Property(e => e.CreatedAt)
                .IsRequired()
                .HasDefaultValueSql("GETDATE()");

            
            builder.HasIndex(e => e.StartDate);
            builder.HasIndex(e => e.IsPublished);

            
            builder.HasOne(e => e.Category)
                .WithMany(c => c.Events)
                .HasForeignKey(e => e.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(e => e.Bookings)
                .WithOne(b => b.Event)
                .HasForeignKey(b => b.EventId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
