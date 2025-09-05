using AutoMapper;
using HotelEasy.Entities;
using HotelEasy.Services.DTO;

namespace HotelEasy.Services.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserDTO>().ReverseMap();
            CreateMap<User, EditUserDTO>().ReverseMap();

            CreateMap<Hotel, HotelDTO>().ReverseMap();
            CreateMap<Hotel, CreateHotelDTO>().ReverseMap();
            CreateMap<Hotel, HotelShortDTO>().ReverseMap();
            CreateMap<Hotel, HotelDetailsDTO>().ReverseMap();

            CreateMap<Room, RoomDTO>().ReverseMap();
            CreateMap<Room, RoomDetailDTO>().ReverseMap();
            CreateMap<Room, CreateRoomDTO>().ReverseMap();
            CreateMap<Room, RoomInReservationDTO>().ReverseMap();

            CreateMap<Reservation, ReservationDTO>().ReverseMap();
            CreateMap<Reservation, CreateReservationDTO>().ReverseMap();

            CreateMap<Hotel, HotelDTO>()
                .ForMember(dest => dest.HotelImages, opt => opt.MapFrom(src => src.HotelImages));
            CreateMap<HotelImage, HotelImageDTO>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.HotelImageId))
                .ForMember(dest => dest.Url, opt => opt.MapFrom(src => src.ImageUrl));

            CreateMap<RoomImage, RoomImageDTO>()
                .ForMember(dest => dest.Url, opt => opt.MapFrom(src => src.ImageUrl));
        }
    }
}