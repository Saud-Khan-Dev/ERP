using Microsoft.EntityFrameworkCore;

public class DeleteGeoLocationHandler(IApplicationDbContext context)
  : ICommandHandler<DeleteGeoLocationCommand, Result<DeleteGeoLocationCommandResult>>
{
  public async Task<Result<DeleteGeoLocationCommandResult>> Handle(DeleteGeoLocationCommand command, CancellationToken cancellationToken)
  {
    var geoLocationId = GeoLocationId.Of(command.Id);
    var geoLocation = await context.GeoLocations
        .FirstOrDefaultAsync(g => g.Id == geoLocationId, cancellationToken);

    if (geoLocation is null)
      throw new GeoLocationNotFoundException($"GeoLocation {command.Id} was not found.");

    var isInUse = await context.Properties
        .AnyAsync(p => p.GeoLocationId == geoLocationId, cancellationToken);

    if (isInUse)
      return Result<DeleteGeoLocationCommandResult>.Failure(
          "This geo location is linked to a property. Delete the property instead.");

    context.GeoLocations.Remove(geoLocation);
    await context.SaveChangesAsync(cancellationToken);

    return Result<DeleteGeoLocationCommandResult>.Success(new DeleteGeoLocationCommandResult(true));
  }
}
