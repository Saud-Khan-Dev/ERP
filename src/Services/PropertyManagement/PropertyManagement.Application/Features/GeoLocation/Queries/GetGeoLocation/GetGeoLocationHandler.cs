using Microsoft.EntityFrameworkCore;

public class GetGeoLocationHandler(IApplicationDbContext context)
  : IQueryHandler<GetGeoLocationQuery, Result<GetGeoLocationQueryResult>>
{
  public async Task<Result<GetGeoLocationQueryResult>> Handle(GetGeoLocationQuery query, CancellationToken cancellationToken)
  {
    var geoLocationId = GeoLocationId.Of(query.Id);
    var geoLocation = await context.GeoLocations
        .AsNoTracking()
        .FirstOrDefaultAsync(g => g.Id == geoLocationId, cancellationToken);

    if (geoLocation is null)
      throw new GeoLocationNotFoundException($"GeoLocation {query.Id} was not found.");

    return Result<GetGeoLocationQueryResult>.Success(
        new GetGeoLocationQueryResult(geoLocation.ToDto()));
  }
}
