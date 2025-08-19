namespace HotelEasy.Services.DTO
{
    public class UserDTO
    {
        public string Email { get; set; } = null!;

        public string Role { get; set; } = null!;
    }
    public class UserAdminDTO
    {
        public string Email { get; set; } = null!;
        
        public string Role { get; set; } = null!;

        public ICollection<HotelDTO> Hotels { get; set; } = new List<HotelDTO>();
    }

    public class UserGuestDTO
    {
        public string Email { get; set; } = null!;

        public string Role { get; set; } = null!;

        public ICollection<ReservationDTO> Reservations { get; set; } = new List<ReservationDTO>();
    }

    public class UserRegisterDTO
    {
        public string Email { get; set; } = null!;
        
        public string PasswordHash { get; set; } = null!;
        
        public string Role { get; set; } = null!;
    }
    public class UserLoginDTO
    {
        public string Email { get; set; } = null!;
        
        public string Password { get; set; } = null!;
    }
}