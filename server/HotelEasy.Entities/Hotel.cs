namespace HotelEasy.Entities;

public partial class Hotel
{
    public int HotelId { get; set; }

    public int OwnerId { get; set; }

    public string Name { get; set; } = null!;

    public string? Location { get; set; }

    public DateTime CreatedAt21180105 { get; set; }

    public virtual ICollection<HotelImage> HotelImages { get; set; } = new List<HotelImage>();

    public virtual User Owner { get; set; } = null!;

    public virtual ICollection<Room> Rooms { get; set; } = new List<Room>();
}
