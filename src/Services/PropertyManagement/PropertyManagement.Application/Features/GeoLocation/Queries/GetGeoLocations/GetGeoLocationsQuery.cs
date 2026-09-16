public sealed record GetGeoLocationsQueryResult(PaginatedResult<GeoLocationDto> GeoLocations);

public sealed record GetGeoLocationsQuery(PaginationRequest Pagination, bool UnlinkedOnly)
  : IQuery<Result<GetGeoLocationsQueryResult>>;
