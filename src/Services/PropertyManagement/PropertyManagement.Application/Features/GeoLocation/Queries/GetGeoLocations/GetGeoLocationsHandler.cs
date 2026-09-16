using Microsoft.EntityFrameworkCore;

public class GetGeoLocationsHandler(IApplicationDbContext context)
  : IQueryHandler<GetGeoLocationsQuery, Result<GetGeoLocationsQueryResult>>
{
  public async Task<Result<GetGeoLocationsQueryResult>> Handle(GetGeoLocationsQuery query, CancellationToken cancellationToken)
  {
    var pageIndex = query.Pagination.Pageindex;
    var pageSize = query.Pagination.PageSize;

    var geoLocations = context.GeoLocations.AsNoTracking();

    if (query.UnlinkedOnly)
      geoLocations = geoLocations.Where(g => !context.Properties.Any(p => p.GeoLocationId == g.Id));

    var totalCount = await geoLocations.LongCountAsync(cancellationToken);

    var page = await geoLocations
        .OrderBy(g => g.Code)
        .Skip(pageIndex * pageSize)
        .Take(pageSize)
        .ToListAsync(cancellationToken);

    return Result<GetGeoLocationsQueryResult>.Success(
        new GetGeoLocationsQueryResult(
            new PaginatedResult<GeoLocationDto>(pageIndex, pageSize, totalCount, page.Select(g => g.ToDto()).ToList())));
  }
}
