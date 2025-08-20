using AutoMapper;
using HotelEasy.Entities;
using HotelEasy.Services.DTO;

namespace HotelEasy.Services.Mapping
{
    public class MappingProfile : Profile
    {
       public MappingProfile()
        {
            CreateMap<Hotel, HotelDTO>().ReverseMap();
            CreateMap<User, UserDTO>().ReverseMap();
        }
    }
}