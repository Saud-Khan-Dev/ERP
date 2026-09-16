public sealed record PropertyInput(
  string PropertyCode,
  string PropertyName,
  PropertyType PropertyType,
  OwnershipType OwnershipType,
  decimal AreaSqFt,
  decimal CoveredAreaSqFt,
  decimal LandAreaSqFt,
  PropertyStatus Status
);

public sealed record PropertyDto(
  Guid Id,
  string PropertyCode,
  string PropertyName,
  PropertyType PropertyType,
  OwnershipType OwnershipType,
  decimal AreaSqFt,
  decimal CoveredAreaSqFt,
  decimal LandAreaSqFt,
  PropertyStatus Status,
  GeoLocationDto? GeoLocation
);
