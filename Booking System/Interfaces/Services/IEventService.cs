using Booking_System.DTOS;

namespace Booking_System.Interfaces.Services
{
    public interface IEventService
    {
        Task<EventDto> GetEventByIdAsync(int id);
        Task<PagedResultDto<EventDto>> GetEventsAsync(EventFilterDto filterDto);
        Task<EventDto> CreateEventAsync(EventCreateDto eventDto, int creatorId);
        Task<EventDto> UpdateEventAsync(int id, EventUpdateDto eventDto, int userId);
        Task DeleteEventAsync(int id, int userId);
        Task<EventImageDto> UploadEventImageAsync(int id, Stream imageStream, string fileName, int userId);
        Task<List<CategoryDto>> GetCategoriesAsync();
        Task<List<TagDto>> GetTagsAsync();
        Task<bool> IsEventOwnerAsync(int eventId, int userId);
    }
}
