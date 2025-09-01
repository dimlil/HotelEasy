using Microsoft.AspNetCore.Http;

namespace HotelEasy.Services.DTO
{
    public class RoomDTO
    {
        public int RoomId { get; set; }

        public string RoomNumber { get; set; } = null!;

        public int Capacity { get; set; }

        public decimal Price { get; set; }

        public string RoomType { get; set; } = null!;

        public bool IsAvailable { get; set; }
    }

    public class CreateRoomDTO
    {
        public string RoomNumber { get; set; } = null!;

        public int Capacity { get; set; }

        public decimal Price { get; set; }

        public string RoomType { get; set; } = null!;

        public bool IsAvailable { get; set; }

        public int HotelId { get; set; }

        public List<IFormFile>? ImageFiles { get; set; }
    }

    public class RoomDetailDTO
    {
        public int RoomId { get; set; }

        public string RoomNumber { get; set; } = null!;

        public int Capacity { get; set; }

        public decimal Price { get; set; }

        public string RoomType { get; set; } = null!;

        public bool IsAvailable { get; set; }

        public HotelDTO Hotel { get; set; } = null!;

        public ICollection<RoomImageDTO>? RoomImages { get; set; }

        public ICollection<ReservationDTO> Reservations { get; set; } = new List<ReservationDTO>();
    }

    public class RoomInReservationDTO
    {
        public int RoomId { get; set; }

        public string RoomNumber { get; set; } = null!;

        public int Capacity { get; set; }

        public decimal Price { get; set; }

        public string RoomType { get; set; } = null!;

        public bool IsAvailable { get; set; }

        public HotelShortDTO Hotel { get; set; } = null!;
    }
}
public class RoomImageDTO
{
    public int Id { get; set; }
    public string Url { get; set; } = null!;
    public int RoomId { get; set; }
}
