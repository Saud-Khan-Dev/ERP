using Microsoft.EntityFrameworkCore;

public class CreatePropertyHandler(IApplicationDbContext context)
  : ICommandHandler<CreatePropertyCommand, Result<CreatePropertyCommandResult>>
{
  public async Task<Result<CreatePropertyCommandResult>> Handle(CreatePropertyCommand command, CancellationToken cancellationToken)
  {
    var propertyCode = Code.Of(command.Property.PropertyCode);

    var codeAlreadyUsed = await context.Properties
        .AnyAsync(p => p.PropertyCode == propertyCode, cancellationToken);

    if (codeAlreadyUsed)
      return Result<CreatePropertyCommandResult>.Failure($"A property with code {propertyCode.Value} already exists.");

    GeoLocationId geoLocationId;

    if (command.GeoLocationId.HasValue)
    {
      var linkResult = await ResolveExistingGeoLocation(command.GeoLocationId.Value, cancellationToken);

      if (!linkResult.IsSuccess)
        return Result<CreatePropertyCommandResult>.Failure(linkResult.Message!);

      geoLocationId = linkResult.Value!;
    }
    else
    {
      var geoLocation = CreateGeoLocation(command.GeoLocation!);
      await context.GeoLocations.AddAsync(geoLocation, cancellationToken);
      geoLocationId = geoLocation.Id;
    }

    var property = CreateProperty(command.Property, propertyCode, geoLocationId);

    await context.Properties.AddAsync(property, cancellationToken);
    await context.SaveChangesAsync(cancellationToken);

    return Result<CreatePropertyCommandResult>.Success(
        new CreatePropertyCommandResult(property.Id.Value, geoLocationId.Value));
  }

  private async Task<Result<GeoLocationId>> ResolveExistingGeoLocation(Guid id, CancellationToken cancellationToken)
  {
    var geoLocationId = GeoLocationId.Of(id);

    var exists = await context.GeoLocations
        .AnyAsync(g => g.Id == geoLocationId, cancellationToken);

    if (!exists)
      throw new GeoLocationNotFoundException($"GeoLocation {id} was not found.");

    var alreadyLinked = await context.Properties
        .AnyAsync(p => p.GeoLocationId == geoLocationId, cancellationToken);

    return alreadyLinked
        ? Result<GeoLocationId>.Failure($"GeoLocation {id} is already linked to another property.")
        : Result<GeoLocationId>.Success(geoLocationId);
  }

  private GeoLocation CreateGeoLocation(GeoLocationInput input)
  {
    return GeoLocation.Create(
      geoLocationId: GeoLocationId.Of(Guid.NewGuid()),
      code: Code.Of(input.Code),
      latitude: input.Latitude,
      longitude: input.Longitude,
      elevation: input.Elevation,
      geoFenceRadius: input.GeoFenceRadius,
      coordinateSystem: input.CoordinateSystem
    );
  }

  private Property CreateProperty(PropertyInput input, Code propertyCode, GeoLocationId geoLocationId)
  {
    return Property.Create(
      propertyId: PropertyId.Of(Guid.NewGuid()),
      propertyCode: propertyCode,
      propertyName: Name.Of(input.PropertyName),
      propertyType: input.PropertyType,
      ownershipType: input.OwnershipType,
      geoLocationId: geoLocationId,
      areaSqFt: input.AreaSqFt,
      coveredAreaSqFt: input.CoveredAreaSqFt,
      landAreaSqFt: input.LandAreaSqFt,
      status: input.Status
    );
  }
}
