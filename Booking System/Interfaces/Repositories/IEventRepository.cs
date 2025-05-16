using Booking_System.Controllers;

namespace Booking_System.Interfaces.Repositories
{
    public interface IEventRepository
    {
        Task<IEnumerable<Event>> GetAllEventsAsync(
            string searchTerm = null,
            int? categoryId = null,
            DateTime? startDate = null,
            DateTime? endDate = null,
            bool publishedOnly = true,
            int? creatorId = null,
            int skip = 0,
            int take = 10);

        Task<int> CountEventsAsync(
            string searchTerm = null,
            int? categoryId = null,
            DateTime? startDate = null,
            DateTime? endDate = null,
            bool publishedOnly = true,
            int? creatorId = null);

        Task<Event> GetEventByIdAsync(int id);
        Task<Event> AddEventAsync(Event eventEntity);
        Task<Event> UpdateEventAsync(Event eventEntity);
        Task DeleteEventAsync(int id);
        Task<bool> EventExistsAsync(int id);
        Task<IEnumerable<Event>> GetEventsByTagAsync(int tagId, int skip = 0, int take = 10);
        Task AddEventTagAsync(int eventId, int tagId);
        Task RemoveEventTagAsync(int eventId, int tagId);
    }
}
