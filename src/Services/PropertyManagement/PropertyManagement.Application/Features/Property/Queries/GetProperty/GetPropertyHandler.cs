using Microsoft.EntityFrameworkCore;

public class GetPropertyHandler(IApplicationDbContext context)
  : IQueryHandler<GetPropertyQuery, Result<GetPropertyQueryResult>>
{
  public async Task<Result<GetPropertyQueryResult>> Handle(GetPropertyQuery query, CancellationToken cancellationToken)
  {
    var propertyId = PropertyId.Of(query.Id);
    var property = await context.Properties
        .AsNoTracking()
        .FirstOrDefaultAsync(p => p.Id == propertyId, cancellationToken);

    if (property is null)
      throw new PropertyNotFoundException($"Property {query.Id} was not found.");

    var geoLocation = await context.GeoLocations
        .AsNoTracking()
        .FirstOrDefaultAsync(g => g.Id == property.GeoLocationId, cancellationToken);

    return Result<GetPropertyQueryResult>.Success(
        new GetPropertyQueryResult(property.ToDto(geoLocation)));
  }
}
