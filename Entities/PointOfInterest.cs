namespace TwoPoi;

public partial class PointOfInterest
{
    public string Id { get; set; } = Guid.NewGuid().ToString("D");

    public string Name { get; set; } = String.Empty;

    public double Latitude { get; set; }

    public double Longitude { get; set; }

    public PoiColorType ColorType { get; set; } = PoiColorType.Fry;
}
