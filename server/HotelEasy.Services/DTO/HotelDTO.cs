using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http;

namespace HotelEasy.Services.DTO
{
    public class HotelDTO
    {
        public int HotelId { get; set; }

        public string Name { get; set; } = null!;

        public string? Location { get; set; }

        public ICollection<HotelImageDTO>? HotelImages { get; set; }
    }
    public class HotelDetailsDTO
    {
        public int HotelId { get; set; }

        public int OwnerId { get; set; }

        public string Name { get; set; } = null!;

        public string? Location { get; set; }

        public DateTime? CreatedAt21180105 { get; set; }

        public UserDTO Owner { get; set; } = null!;

        public ICollection<RoomDTO> Rooms { get; set; } = null!;

        public ICollection<HotelImageDTO>? HotelImages { get; set; }
    }

    public class CreateHotelDTO
    {
        public int OwnerId { get; set; }

        public string Name { get; set; } = null!;

        public string? Location { get; set; }

        public List<IFormFile>? ImageFiles { get; set; }
    }

    public class HotelShortDTO
    {
        public string Name { get; set; } = null!;

        public string? Location { get; set; }
    }

    public class HotelImageDTO
    {
        public int Id { get; set; }
        public string Url { get; set; } = null!;
        public int HotelId { get; set; }
        [JsonIgnore]
        public HotelDTO Hotel { get; set; } = null!;
    }
}