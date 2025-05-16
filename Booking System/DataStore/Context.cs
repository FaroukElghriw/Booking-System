using Booking_System.Controllers;
using Microsoft.EntityFrameworkCore;

namespace Booking_System.DataStore
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<TagEvent> EventTags { get; set; }
        public DbSet<Booking> Bookings
        {
            get; set;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure the many-to-many relationship between Event and Tag
            modelBuilder.Entity<TagEvent>()
                .HasKey(et => new { et.Id });

            modelBuilder.Entity<TagEvent>()
                .HasOne(et => et.Event)
                .WithMany(e => e.EventTags)
                .HasForeignKey(et => et.Id);

            modelBuilder.Entity<TagEvent>()
                .HasOne(et => et.Tag)
                .WithMany(t => t.EventTags)
                .HasForeignKey(et => et.Id);

           
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(Context).Assembly);

          
            SeedData(modelBuilder);
        }

        private void SeedData(ModelBuilder modelBuilder)
        {
            
            modelBuilder.Entity<Role>().HasData(
                new Role { Id = 1, Name = "Admin", Description = "Administrator with full access" },
                new Role { Id = 2, Name = "Organizer", Description = "Can create and manage events" },
                new Role { Id = 3, Name = "User", Description = "Standard user" }
            );

            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Technology", Description = "Tech events and conferences" },
                new Category {  Id = 2, Name = "Business", Description = "Business and networking events" },
                new Category { Id = 3, Name = "Music", Description = "Concerts and music festivals" },
                new Category { Id = 4, Name = "Sports", Description = "Sports events and competitions" },
                new Category { Id = 5, Name = "Arts", Description = "Art exhibitions and cultural events" }
            );

            
            modelBuilder.Entity<Tag>().HasData(
                new Tag { Id = 1, Name = "Conference" },
                new Tag { Id = 2, Name = "Workshop" },
                new Tag { Id = 3, Name = "Seminar" },
                new Tag { Id = 4, Name = "Networking" },
                new Tag { Id = 5, Name = "Outdoor" },
                new Tag { Id = 6, Name = "Virtual" },
                new Tag { Id = 7, Name = "Free" },
                new Tag { Id = 8, Name = "Paid" },
                new Tag { Id = 9, Name = "Family" },
                new Tag { Id = 10, Name = "Professional" }
            );
        }
    }
}
