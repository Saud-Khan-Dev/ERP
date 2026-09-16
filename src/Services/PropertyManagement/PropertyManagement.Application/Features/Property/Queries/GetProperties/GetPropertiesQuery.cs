public sealed record GetPropertiesQueryResult(PaginatedResult<PropertyDto> Properties);

public sealed record GetPropertiesQuery(PaginationRequest Pagination, PropertyStatus? Status)
  : IQuery<Result<GetPropertiesQueryResult>>;
