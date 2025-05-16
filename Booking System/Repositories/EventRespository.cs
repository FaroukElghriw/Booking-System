using Booking_System.Controllers;
using Booking_System.DataStore;
using Booking_System.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Booking_System.Repositories
{
    public class EventRespository: IEventRepository
    {
        private readonly Context _context;

        public EventRespository(Context context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Event>> GetAllEventsAsync(
            string searchTerm = null,
            int? categoryId = null,
            DateTime? startDate = null,
            DateTime? endDate = null,
            bool publishedOnly = true,
            int? creatorId = null,
            int skip = 0,
            int take = 10)
        {
            IQueryable<Event> query = _context.Events
                .Include(e => e.Category)
                .Include(e => e.Creator)
                .Include(e => e.EventTags)
                    .ThenInclude(et => et.Tag)
                .AsQueryable();

            query = ApplyFilters(query, searchTerm, categoryId, startDate, endDate, publishedOnly, creatorId);

            return await query
                .OrderBy(e => e.StartDate)
                .Skip(skip)
                .Take(take)
                .ToListAsync();
        }

        public async Task<int> CountEventsAsync(
            string searchTerm = null,
            int? categoryId = null,
            DateTime? startDate = null,
            DateTime? endDate = null,
            bool publishedOnly = true,
            int? creatorId = null)
        {
            IQueryable<Event> query = _context.Events.AsQueryable();

            query = ApplyFilters(query, searchTerm, categoryId, startDate, endDate, publishedOnly, creatorId);

            return await query.CountAsync();
        }

        private IQueryable<Event> ApplyFilters(
            IQueryable<Event> query,
            string searchTerm = null,
            int? categoryId = null,
            DateTime? startDate = null,
            DateTime? endDate = null,
            bool publishedOnly = true,
            int? creatorId = null)
        {
            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                query = query.Where(e => e.Title.Contains(searchTerm) ||
                                        e.Description.Contains(searchTerm) ||
                                        e.Location.Contains(searchTerm));
            }

            if (categoryId.HasValue)
            {
                query = query.Where(e => e.CategoryId == categoryId);
            }

            if (startDate.HasValue)
            {
                query = query.Where(e => e.StartDate >= startDate);
            }

            if (endDate.HasValue)
            {
                query = query.Where(e => e.EndDate <= endDate);
            }

            if (publishedOnly)
            {
                query = query.Where(e => e.IsPublished);
            }

            if (creatorId.HasValue)
            {
                query = query.Where(e => e.CreatorId == creatorId);
            }

            return query;
        }

        public async Task<Event> GetEventByIdAsync(int id)
        {
            return await _context.Events
                .Include(e => e.Category)
                .Include(e => e.Creator)
                .Include(e => e.EventTags)
                    .ThenInclude(et => et.Tag)
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<Event> AddEventAsync(Event eventEntity)
        {
            _context.Events.Add(eventEntity);
            await _context.SaveChangesAsync();
            return eventEntity;
        }

        public async Task<Event> UpdateEventAsync(Event eventEntity)
        {
            _context.Entry(eventEntity).State = EntityState.Modified;
            eventEntity.UpdatedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();
            return eventEntity;
        }

        public async Task DeleteEventAsync(int id)
        {
            var eventToDelete = await _context.Events.FindAsync(id);
            if (eventToDelete != null)
            {
                _context.Events.Remove(eventToDelete);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> EventExistsAsync(int id)
        {
            return await _context.Events.AnyAsync(e => e.Id == id);
        }

        public async Task<IEnumerable<Event>> GetEventsByTagAsync(int tagId, int skip = 0, int take = 10)
        {
            return await _context.Events
                .Include(e => e.Category)
                .Include(e => e.Creator)
                .Include(e => e.EventTags)
                    .ThenInclude(et => et.Tag)
                .Where(e => e.EventTags.Any(et => et.TagId == tagId) && e.IsPublished)
                .OrderBy(e => e.StartDate)
                .Skip(skip)
                .Take(take)
                .ToListAsync();
        }

        public async Task AddEventTagAsync(int eventId, int tagId)
        {
            var eventTag = new TagEvent
            {
                Id = eventId,
                TagId = tagId
            };

            _context.EventTags.Add(eventTag);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveEventTagAsync(int eventId, int tagId)
        {
            var eventTag = await _context.EventTags
                .FirstOrDefaultAsync(et => et.Id == eventId && et.TagId == tagId);

            if (eventTag != null)
            {
                _context.EventTags.Remove(eventTag);
                await _context.SaveChangesAsync();
            }
        }
    }
}
