using Microsoft.EntityFrameworkCore;

public class GetPropertiesHandler(IApplicationDbContext context)
  : IQueryHandler<GetPropertiesQuery, Result<GetPropertiesQueryResult>>
{
  public async Task<Result<GetPropertiesQueryResult>> Handle(GetPropertiesQuery query, CancellationToken cancellationToken)
  {
    var pageIndex = query.Pagination.Pageindex;
    var pageSize = query.Pagination.PageSize;

    var properties = context.Properties.AsNoTracking();

    if (query.Status.HasValue)
      properties = properties.Where(p => p.Status == query.Status.Value);

    var totalCount = await properties.LongCountAsync(cancellationToken);

    var page = await properties
        .OrderBy(p => p.PropertyCode)
        .Skip(pageIndex * pageSize)
        .Take(pageSize)
        .ToListAsync(cancellationToken);

    var geoLocationIds = page.Select(p => p.GeoLocationId).ToList();
    var geoLocations = await context.GeoLocations
        .AsNoTracking()
        .Where(g => geoLocationIds.Contains(g.Id))
        .ToListAsync(cancellationToken);

    var data = page
        .Select(p => p.ToDto(geoLocations.FirstOrDefault(g => g.Id == p.GeoLocationId)))
        .ToList();

    return Result<GetPropertiesQueryResult>.Success(
        new GetPropertiesQueryResult(
            new PaginatedResult<PropertyDto>(pageIndex, pageSize, totalCount, data)));
  }
}
