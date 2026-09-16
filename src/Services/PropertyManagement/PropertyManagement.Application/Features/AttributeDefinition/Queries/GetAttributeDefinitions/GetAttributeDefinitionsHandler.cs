using Microsoft.EntityFrameworkCore;

public class GetAttributeDefinitionsHandler(IApplicationDbContext context)
  : IQueryHandler<GetAttributeDefinitionsQuery, Result<GetAttributeDefinitionsQueryResult>>
{
  public async Task<Result<GetAttributeDefinitionsQueryResult>> Handle(GetAttributeDefinitionsQuery query, CancellationToken cancellationToken)
  {
    var definitions = context.AttributeDefinitions.AsNoTracking();

    if (query.Group.HasValue)
      definitions = definitions.Where(d => d.Group == query.Group.Value);

    if (!query.IncludeInactive)
      definitions = definitions.Where(d => d.IsActive);

    var result = await definitions
        .OrderBy(d => d.Group)
        .ThenBy(d => d.DisplayOrder)
        .ToListAsync(cancellationToken);

    return Result<GetAttributeDefinitionsQueryResult>.Success(
        new GetAttributeDefinitionsQueryResult(result.Select(d => d.ToDto()).ToList()));
  }
}
