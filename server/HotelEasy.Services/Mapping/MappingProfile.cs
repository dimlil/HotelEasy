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
            CreateMap<Hotel, HotelDTO>().ReverseMap();
            CreateMap<Room, RoomDTO>().ReverseMap();
            CreateMap<Room, RoomDetailDTO>().ReverseMap();
            CreateMap<Reservation, ReservationDTO>().ReverseMap();
        }
    }
}