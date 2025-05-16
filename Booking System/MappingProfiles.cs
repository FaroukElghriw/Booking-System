using AutoMapper;
using Booking_System.Controllers;
using Booking_System.DTOS;

namespace Booking_System
{
    public class MappingProfiles:Profile
    {
        public MappingProfiles()
        {
            CreateMap<Event, EventDto>()
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category))
                .ForMember(dest => dest.Tags, opt => opt.MapFrom(src => src.EventTags.Select(et => et.Tag)));

            CreateMap<EventCreateDto, Event>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.CreatorId, opt => opt.Ignore())
                .ForMember(dest => dest.EventTags, opt => opt.Ignore())
                .ForMember(dest => dest.ImageUrl, opt => opt.Ignore());

            CreateMap<Category, CategoryDto>();
            CreateMap<User, UserDto>();
            CreateMap<Tag, TagDto>();
        }
    }
}
