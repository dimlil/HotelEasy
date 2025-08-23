namespace HotelEasy.Entities;

public partial class HotelImage
{
    public int HotelImageId { get; set; }

    public int HotelId { get; set; }

    public string ImageUrl { get; set; } = null!;

    public DateTime? CreatedAt21180105 { get; set; }

    public virtual Hotel Hotel { get; set; } = null!;
}
