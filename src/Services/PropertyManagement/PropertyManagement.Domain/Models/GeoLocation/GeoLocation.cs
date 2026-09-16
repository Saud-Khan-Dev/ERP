public class GeoLocation : Aggregate<GeoLocationId>
{
  public Code Code { get; private set; } = default!;
  public decimal Latitude { get; private set; }
  public decimal Longitude { get; private set; }
  public decimal? Elevation { get; private set; }
  public decimal? GeoFenceRadius { get; private set; }
  public string CoordinateSystem { get; private set; } = default!;

  public static GeoLocation Create(
      GeoLocationId geoLocationId,
      Code code,
      decimal latitude,
      decimal longitude,
      decimal? elevation,
      decimal? geoFenceRadius,
      string coordinateSystem)
  {
    ArgumentNullException.ThrowIfNull(code);
    Validate(latitude, longitude, geoFenceRadius, coordinateSystem);

    return new GeoLocation
    {
      Id = geoLocationId,
      Code = code,
      Latitude = latitude,
      Longitude = longitude,
      Elevation = elevation,
      GeoFenceRadius = geoFenceRadius,
      CoordinateSystem = coordinateSystem.Trim()
    };
  }

  public void Update(
      Code code,
      decimal latitude,
      decimal longitude,
      decimal? elevation,
      decimal? geoFenceRadius,
      string coordinateSystem)
  {
    ArgumentNullException.ThrowIfNull(code);
    Validate(latitude, longitude, geoFenceRadius, coordinateSystem);

    Code = code;
    Latitude = latitude;
    Longitude = longitude;
    Elevation = elevation;
    GeoFenceRadius = geoFenceRadius;
    CoordinateSystem = coordinateSystem.Trim();
  }

  private static void Validate(decimal latitude, decimal longitude, decimal? geoFenceRadius, string coordinateSystem)
  {
    if (latitude < -90 || latitude > 90)
      throw new DomainException("Latitude must be between -90 and 90.");

    if (longitude < -180 || longitude > 180)
      throw new DomainException("Longitude must be between -180 and 180.");

    if (geoFenceRadius.HasValue && geoFenceRadius.Value < 0)
      throw new DomainException("Geo fence radius cannot be negative.");

    if (string.IsNullOrWhiteSpace(coordinateSystem))
      throw new DomainException("Coordinate system is required.");

    if (coordinateSystem.Length > 50)
      throw new DomainException("Coordinate system cannot exceed 50 characters.");
  }
}
