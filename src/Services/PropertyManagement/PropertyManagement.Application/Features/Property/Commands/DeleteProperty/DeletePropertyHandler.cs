using Microsoft.EntityFrameworkCore;

public class DeletePropertyHandler(IApplicationDbContext context)
  : ICommandHandler<DeletePropertyCommand, Result<DeletePropertyCommandResult>>
{
  public async Task<Result<DeletePropertyCommandResult>> Handle(DeletePropertyCommand command, CancellationToken cancellationToken)
  {
    var propertyId = PropertyId.Of(command.Id);
    var property = await context.Properties
        .FirstOrDefaultAsync(p => p.Id == propertyId, cancellationToken);

    if (property is null)
      throw new PropertyNotFoundException($"Property {command.Id} was not found.");

    // Attribute values and documents cascade at the database level; the 1:1 geo location does not.
    var geoLocation = await context.GeoLocations
        .FirstOrDefaultAsync(g => g.Id == property.GeoLocationId, cancellationToken);

    context.Properties.Remove(property);

    if (geoLocation is not null)
      context.GeoLocations.Remove(geoLocation);

    await context.SaveChangesAsync(cancellationToken);

    return Result<DeletePropertyCommandResult>.Success(new DeletePropertyCommandResult(true));
  }
}
