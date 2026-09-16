using Microsoft.EntityFrameworkCore;

public class SavePropertyAttributeValuesHandler(IApplicationDbContext context)
  : ICommandHandler<SavePropertyAttributeValuesCommand, Result<SavePropertyAttributeValuesCommandResult>>
{
  public async Task<Result<SavePropertyAttributeValuesCommandResult>> Handle(SavePropertyAttributeValuesCommand command, CancellationToken cancellationToken)
  {
    var propertyId = PropertyId.Of(command.PropertyId);

    var propertyExists = await context.Properties
        .AnyAsync(p => p.Id == propertyId, cancellationToken);

    if (!propertyExists)
      throw new PropertyNotFoundException($"Property {command.PropertyId} was not found.");

    var definitionIds = command.Values
        .Select(v => AttributeDefinitionId.Of(v.AttributeDefinitionId))
        .ToList();

    var definitions = await context.AttributeDefinitions
        .Where(d => definitionIds.Contains(d.Id))
        .ToListAsync(cancellationToken);

    var existingValues = await context.PropertyAttributeValues
        .Where(v => v.PropertyId == propertyId && definitionIds.Contains(v.AttributeDefinitionId))
        .ToListAsync(cancellationToken);

    foreach (var input in command.Values)
    {
      var definitionId = AttributeDefinitionId.Of(input.AttributeDefinitionId);
      var definition = definitions.FirstOrDefault(d => d.Id == definitionId);

      if (definition is null)
        throw new AttributeDefinitionNotFoundException($"Attribute definition {input.AttributeDefinitionId} was not found.");

      var existing = existingValues.FirstOrDefault(v => v.AttributeDefinitionId == definitionId);

      if (existing is null)
      {
        var attributeValue = PropertyAttributeValue.Create(
          propertyAttributeValueId: PropertyAttributeValueId.Of(Guid.NewGuid()),
          propertyId: propertyId,
          definition: definition,
          value: input.Value
        );

        await context.PropertyAttributeValues.AddAsync(attributeValue, cancellationToken);
      }
      else
      {
        existing.UpdateValue(definition, input.Value);
      }
    }

    await context.SaveChangesAsync(cancellationToken);

    return Result<SavePropertyAttributeValuesCommandResult>.Success(
        new SavePropertyAttributeValuesCommandResult(command.Values.Count));
  }
}
