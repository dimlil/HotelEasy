namespace HotelEasy.Services.DTO
{
    public class ReservationDTO
    {
        public int UserId { get; set; }

        public int RoomId { get; set; }

        public DateOnly CheckInDate { get; set; }

        public DateOnly CheckOutDate { get; set; }

        public virtual RoomInReservationDTO Room { get; set; } = null!;

        public virtual UserDTO User { get; set; } = null!;
    }
}