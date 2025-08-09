using System;
using System.Collections.Generic;

namespace HotelEasy.Entities;

public partial class Reservation
{
    public int ReservationId { get; set; }

    public int UserId { get; set; }

    public int RoomId { get; set; }

    public DateOnly CheckInDate { get; set; }

    public DateOnly CheckOutDate { get; set; }

    public DateTime? CreatedAt21180105 { get; set; }

    public virtual Room Room { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
