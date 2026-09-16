public sealed record GetGeoLocationQueryResult(GeoLocationDto GeoLocation);

public sealed record GetGeoLocationQuery(Guid Id) : IQuery<Result<GetGeoLocationQueryResult>>;
