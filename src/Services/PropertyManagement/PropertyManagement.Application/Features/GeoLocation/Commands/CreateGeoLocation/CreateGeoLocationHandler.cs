using Microsoft.EntityFrameworkCore;

public class CreateGeoLocationHandler(IApplicationDbContext context)
  : ICommandHandler<CreateGeoLocationCommand, Result<CreateGeoLocationCommandResult>>
{
  public async Task<Result<CreateGeoLocationCommandResult>> Handle(CreateGeoLocationCommand command, CancellationToken cancellationToken)
  {
    var code = Code.Of(command.GeoLocation.Code);

    var codeAlreadyUsed = await context.GeoLocations
        .AnyAsync(g => g.Code == code, cancellationToken);

    if (codeAlreadyUsed)
      return Result<CreateGeoLocationCommandResult>.Failure($"A geo location with code {code.Value} already exists.");

    var geoLocation = GeoLocation.Create(
      geoLocationId: GeoLocationId.Of(Guid.NewGuid()),
      code: code,
      latitude: command.GeoLocation.Latitude,
      longitude: command.GeoLocation.Longitude,
      elevation: command.GeoLocation.Elevation,
      geoFenceRadius: command.GeoLocation.GeoFenceRadius,
      coordinateSystem: command.GeoLocation.CoordinateSystem
    );

    await context.GeoLocations.AddAsync(geoLocation, cancellationToken);
    await context.SaveChangesAsync(cancellationToken);

    return Result<CreateGeoLocationCommandResult>.Success(
        new CreateGeoLocationCommandResult(geoLocation.Id.Value));
  }
}
