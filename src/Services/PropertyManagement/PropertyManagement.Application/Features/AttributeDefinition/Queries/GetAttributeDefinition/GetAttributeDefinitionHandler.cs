using Microsoft.EntityFrameworkCore;

public class GetAttributeDefinitionHandler(IApplicationDbContext context)
  : IQueryHandler<GetAttributeDefinitionQuery, Result<GetAttributeDefinitionQueryResult>>
{
  public async Task<Result<GetAttributeDefinitionQueryResult>> Handle(GetAttributeDefinitionQuery query, CancellationToken cancellationToken)
  {
    var definitionId = AttributeDefinitionId.Of(query.Id);
    var definition = await context.AttributeDefinitions
        .AsNoTracking()
        .FirstOrDefaultAsync(d => d.Id == definitionId, cancellationToken);

    if (definition is null)
      throw new AttributeDefinitionNotFoundException($"Attribute definition {query.Id} was not found.");

    return Result<GetAttributeDefinitionQueryResult>.Success(
        new GetAttributeDefinitionQueryResult(definition.ToDto()));
  }
}
