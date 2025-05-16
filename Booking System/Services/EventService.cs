using AutoMapper;
using Booking_System.Controllers;
using Booking_System.DataStore;
using Booking_System.DTOS;
using Booking_System.Interfaces.Repositories;
using Booking_System.Interfaces.Services;
using System.ComponentModel.DataAnnotations;

namespace Booking_System.Services
{
    public class EventService:EventService
    {
        private readonly IEventRepository _eventRepository;
        private readonly IGenericReposity<Category> _categoryRepository;
        private readonly IGenericReposity<Tag> _tagRepository;
        private readonly Context _context;

        private readonly IImageStorageService _imageStorageService;
        private readonly IMapper _mapper;

        public EventService(
            IEventRepository eventRepository,
            IGenericReposity<Category> categoryRepository,
            IGenericReposity<Tag> tagRepository,
           IImageStorageService imageStorageService,
           Context context,
            IMapper mapper)
        {
            _eventRepository = eventRepository;
            _categoryRepository = categoryRepository;
            _tagRepository = tagRepository;
            _context = context;
             _imageStorageService = imageStorageService;
            _mapper = mapper;
        }

        public async Task<EventDto> GetEventByIdAsync(int id)
        {
            var eventEntity = await _eventRepository.GetEventByIdAsync(id);
            if (eventEntity == null)
            {
                throw new NotFoundException($"Event with ID {id} not found");
            }

            return _mapper.Map<EventDto>(eventEntity);
        }

        public async Task<PagedResultDto<EventDto>> GetEventsAsync(EventFilterDto filterDto)
        {
            var events = await _eventRepository.GetAllEventsAsync(
                filterDto.SearchTerm,
                filterDto.CategoryId,
                filterDto.StartDate,
                filterDto.EndDate,
                filterDto.PublishedOnly,
                filterDto.CreatorId,
                (filterDto.PageNumber - 1) * filterDto.PageSize,
                filterDto.PageSize);

            var totalCount = await _eventRepository.CountEventsAsync(
                filterDto.SearchTerm,
                filterDto.CategoryId,
                filterDto.StartDate,
                filterDto.EndDate,
                filterDto.PublishedOnly,
                filterDto.CreatorId);

            var eventDtos = _mapper.Map<List<EventDto>>(events);

            return new PagedResultDto<EventDto>
            {
                Items = eventDtos,
                TotalCount = totalCount,
                PageNumber = filterDto.PageNumber,
                PageSize = filterDto.PageSize
            };
        }
        public async Task<EventDto> CreateEventAsync(EventCreateDto eventDto, int creatorId)
        {
            // Validate category exists
            if (!await _categoryRepository.CategoryExistsAsync(eventDto.CategoryId))
            {
                throw new ValidationException($"Category with ID {eventDto.CategoryId} does not exist");
            }

            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var eventEntity = _mapper.Map<Event>(eventDto);
                eventEntity.CreatorId = creatorId;
                eventEntity.CreatedAt = DateTime.UtcNow;

                var createdEvent = await _eventRepository.AddEventAsync(eventEntity);

                // Add tags if specified
                if (eventDto.TagIds != null && eventDto.TagIds.Any())
                {
                    foreach (var tagId in eventDto.TagIds)
                    {
                        if (!await _tagRepository.ExistsAsync(E=>E.Id==))
                        {
                            throw new ValidationException($"Tag with ID {tagId} does not exist");
                        }
                        await _eventRepository.AddEventTagAsync(createdEvent.Id, tagId);
                    }
                }

                await transaction.CommitAsync();

                var resultEvent = await _eventRepository.GetEventByIdAsync(createdEvent.Id);
                return _mapper.Map<EventDto>(resultEvent);
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<EventImageDto> UploadEventImageAsync(int id, Stream imageStream, string fileName, int userId)
        {
            var eventEntity = await _eventRepository.GetEventByIdAsync(id);
            if (eventEntity == null)
            {
                throw new NotFoundException($"Event with ID {id} not found");
            }

            if (eventEntity.CreatorId != userId)
            {
                throw new UnauthorizedException("Only the event creator can upload images");
            }

            // Validate file extension
            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };
            var extension = Path.GetExtension(fileName).ToLower();
            if (!allowedExtensions.Contains(extension))
            {
                throw new ValidationException("Invalid image format. Only JPG and PNG are allowed");
            }

            // Upload image using IImageStorageService
            var imageUrl = await IImageStorageService.UploadImageAsync(imageStream, fileName);

            // Update event with image URL
            eventEntity.ImageUrl = imageUrl;
            eventEntity.UpdatedAt = DateTime.UtcNow;
            await _eventRepository.UpdateEventAsync(eventEntity);

            return new EventImageDto
            {
                EventId = id,
                ImageUrl = imageUrl
            };
        }

        public async Task<List<CategoryDto>> GetCategoriesAsync()
        {
            var categories = await _categoryRepository.GetAllAsync();
            return _mapper.Map<List<CategoryDto>>(categories);
        }

        public async Task<List<TagDto>> GetTagsAsync()
        {
            var tags = await _tagRepository.GetAllAsync();
            return _mapper.Map<List<TagDto>>(tags);
        }
    }
}
