using System;
using System.Collections.Generic;

namespace HotelEasy.Entities;

public partial class Room
{
    public int RoomId { get; set; }

    public int HotelId { get; set; }

    public string RoomNumber { get; set; } = null!;

    public int Capacity { get; set; }

    public decimal Price { get; set; }

    public string RoomType { get; set; } = null!;

    public bool IsAvailable { get; set; }

    public DateTime? CreatedAt21180105 { get; set; }

    public virtual Hotel Hotel { get; set; } = null!;

    public virtual ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
}
