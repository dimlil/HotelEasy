using System;
using System.Collections.Generic;

namespace HotelEasy.Entities;

public partial class Hotel
{
    public int HotelId { get; set; }

    public int OwnerId { get; set; }

    public string Name { get; set; } = null!;

    public string? Location { get; set; }

    public DateTime? CreatedAt21180105 { get; set; }

    public virtual User Owner { get; set; } = null!;

    public virtual ICollection<Room> Rooms { get; set; } = new List<Room>();
}
