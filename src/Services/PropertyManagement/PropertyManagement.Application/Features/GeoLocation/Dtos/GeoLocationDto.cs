public sealed record GeoLocationInput(
  string Code,
  decimal Latitude,
  decimal Longitude,
  decimal? Elevation,
  decimal? GeoFenceRadius,
  string CoordinateSystem
);

public sealed record GeoLocationDto(
  Guid Id,
  string Code,
  decimal Latitude,
  decimal Longitude,
  decimal? Elevation,
  decimal? GeoFenceRadius,
  string CoordinateSystem
);
