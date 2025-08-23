namespace HotelEasy.Entities;

public partial class RoomImage
{
    public int RoomImageId { get; set; }

    public int RoomId { get; set; }

    public string ImageUrl { get; set; } = null!;

    public DateTime? CreatedAt21180105 { get; set; }

    public virtual Room Room { get; set; } = null!;
}
