using Microsoft.EntityFrameworkCore;

public class UpdateGeoLocationHandler(IApplicationDbContext context)
  : ICommandHandler<UpdateGeoLocationCommand, Result<UpdateGeoLocationCommandResult>>
{
  public async Task<Result<UpdateGeoLocationCommandResult>> Handle(UpdateGeoLocationCommand command, CancellationToken cancellationToken)
  {
    var geoLocationId = GeoLocationId.Of(command.Id);
    var geoLocation = await context.GeoLocations
        .FirstOrDefaultAsync(g => g.Id == geoLocationId, cancellationToken);

    if (geoLocation is null)
      throw new GeoLocationNotFoundException($"GeoLocation {command.Id} was not found.");

    var code = Code.Of(command.GeoLocation.Code);

    var codeAlreadyUsed = await context.GeoLocations
        .AnyAsync(g => g.Code == code && g.Id != geoLocationId, cancellationToken);

    if (codeAlreadyUsed)
      return Result<UpdateGeoLocationCommandResult>.Failure($"A geo location with code {code.Value} already exists.");

    geoLocation.Update(
      code: code,
      latitude: command.GeoLocation.Latitude,
      longitude: command.GeoLocation.Longitude,
      elevation: command.GeoLocation.Elevation,
      geoFenceRadius: command.GeoLocation.GeoFenceRadius,
      coordinateSystem: command.GeoLocation.CoordinateSystem
    );

    await context.SaveChangesAsync(cancellationToken);

    return Result<UpdateGeoLocationCommandResult>.Success(new UpdateGeoLocationCommandResult(true));
  }
}
