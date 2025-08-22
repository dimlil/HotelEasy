namespace HotelEasy.Services.DTO
{
    public class HotelDTO
    {
        public int OwnerId { get; set; }

        public string Name { get; set; } = null!;

        public string? Location { get; set; }

        public DateTime? CreatedAt21180105 { get; set; }

        public UserDTO Owner { get; set; } = null!;
    }

    public class CreateHotelDTO
    {
        public int OwnerId { get; set; }

        public string Name { get; set; } = null!;

        public string? Location { get; set; }
    }

    public class HotelShortDTO
    {
        public string Name { get; set; } = null!;

        public string? Location { get; set; }
    }
}