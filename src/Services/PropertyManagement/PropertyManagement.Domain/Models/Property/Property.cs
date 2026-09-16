public class Property : Aggregate<PropertyId>
{
  public Code PropertyCode { get; private set; } = default!;
  public Name PropertyName { get; private set; } = default!;
  public PropertyType PropertyType { get; private set; }
  public OwnershipType OwnershipType { get; private set; }
  public GeoLocationId GeoLocationId { get; private set; } = default!;
  public decimal AreaSqFt { get; private set; }
  public decimal CoveredAreaSqFt { get; private set; }
  public decimal LandAreaSqFt { get; private set; }
  public PropertyStatus Status { get; private set; }

  public static Property Create(
      PropertyId propertyId,
      Code propertyCode,
      Name propertyName,
      PropertyType propertyType,
      OwnershipType ownershipType,
      GeoLocationId geoLocationId,
      decimal areaSqFt,
      decimal coveredAreaSqFt,
      decimal landAreaSqFt,
      PropertyStatus status)
  {
    ArgumentNullException.ThrowIfNull(propertyCode);
    ArgumentNullException.ThrowIfNull(propertyName);
    ArgumentNullException.ThrowIfNull(geoLocationId);
    ValidateAreas(areaSqFt, coveredAreaSqFt, landAreaSqFt);

    return new Property
    {
      Id = propertyId,
      PropertyCode = propertyCode,
      PropertyName = propertyName,
      PropertyType = propertyType,
      OwnershipType = ownershipType,
      GeoLocationId = geoLocationId,
      AreaSqFt = areaSqFt,
      CoveredAreaSqFt = coveredAreaSqFt,
      LandAreaSqFt = landAreaSqFt,
      Status = status
    };
  }

  public void Update(
      Code propertyCode,
      Name propertyName,
      PropertyType propertyType,
      OwnershipType ownershipType,
      decimal areaSqFt,
      decimal coveredAreaSqFt,
      decimal landAreaSqFt,
      PropertyStatus status)
  {
    ArgumentNullException.ThrowIfNull(propertyCode);
    ArgumentNullException.ThrowIfNull(propertyName);
    ValidateAreas(areaSqFt, coveredAreaSqFt, landAreaSqFt);

    PropertyCode = propertyCode;
    PropertyName = propertyName;
    PropertyType = propertyType;
    OwnershipType = ownershipType;
    AreaSqFt = areaSqFt;
    CoveredAreaSqFt = coveredAreaSqFt;
    LandAreaSqFt = landAreaSqFt;
    Status = status;
  }

  private static void ValidateAreas(decimal areaSqFt, decimal coveredAreaSqFt, decimal landAreaSqFt)
  {
    if (areaSqFt < 0 || coveredAreaSqFt < 0 || landAreaSqFt < 0)
      throw new DomainException("Area values cannot be negative.");
  }
}
