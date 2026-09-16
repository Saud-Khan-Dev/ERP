using Microsoft.EntityFrameworkCore;

public class GetPropertyAttributeValuesHandler(IApplicationDbContext context)
  : IQueryHandler<GetPropertyAttributeValuesQuery, Result<GetPropertyAttributeValuesQueryResult>>
{
  public async Task<Result<GetPropertyAttributeValuesQueryResult>> Handle(GetPropertyAttributeValuesQuery query, CancellationToken cancellationToken)
  {
    var propertyId = PropertyId.Of(query.PropertyId);

    var propertyExists = await context.Properties
        .AnyAsync(p => p.Id == propertyId, cancellationToken);

    if (!propertyExists)
      throw new PropertyNotFoundException($"Property {query.PropertyId} was not found.");

    var definitions = context.AttributeDefinitions
        .AsNoTracking()
        .Where(d => d.IsActive);

    if (query.Group.HasValue)
      definitions = definitions.Where(d => d.Group == query.Group.Value);

    var formAttributes = await definitions
        .OrderBy(d => d.Group)
        .ThenBy(d => d.DisplayOrder)
        .ToListAsync(cancellationToken);

    var values = await context.PropertyAttributeValues
        .AsNoTracking()
        .Where(v => v.PropertyId == propertyId)
        .ToListAsync(cancellationToken);

    var result = formAttributes
        .Select(definition =>
        {
          var value = values.FirstOrDefault(v => v.AttributeDefinitionId == definition.Id);

          return new PropertyAttributeValueDto(
            Id: value?.Id.Value,
            AttributeDefinitionId: definition.Id.Value,
            Group: definition.Group,
            Code: definition.Code.Value,
            Label: definition.Label.Value,
            DataType: definition.DataType,
            IsRequired: definition.IsRequired,
            Options: definition.Options,
            DisplayOrder: definition.DisplayOrder,
            Value: value?.Value ?? definition.DefaultValue
          );
        })
        .ToList();

    return Result<GetPropertyAttributeValuesQueryResult>.Success(
        new GetPropertyAttributeValuesQueryResult(result));
  }
}
