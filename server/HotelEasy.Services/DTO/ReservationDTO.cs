namespace HotelEasy.Services.DTO
{
    public class ReservationDTO
    {
        public DateOnly CheckInDate { get; set; }

        public DateOnly CheckOutDate { get; set; }

        public virtual RoomInReservationDTO Room { get; set; } = null!;

        public virtual UserDTO User { get; set; } = null!;
    }
}