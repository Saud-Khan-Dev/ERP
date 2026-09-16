using Microsoft.EntityFrameworkCore;

public class DeletePropertyAttributeValueHandler(IApplicationDbContext context)
  : ICommandHandler<DeletePropertyAttributeValueCommand, Result<DeletePropertyAttributeValueCommandResult>>
{
  public async Task<Result<DeletePropertyAttributeValueCommandResult>> Handle(DeletePropertyAttributeValueCommand command, CancellationToken cancellationToken)
  {
    var propertyId = PropertyId.Of(command.PropertyId);
    var definitionId = AttributeDefinitionId.Of(command.AttributeDefinitionId);

    var attributeValue = await context.PropertyAttributeValues
        .FirstOrDefaultAsync(v => v.PropertyId == propertyId && v.AttributeDefinitionId == definitionId, cancellationToken);

    if (attributeValue is null)
      throw new PropertyAttributeValueNotFoundException(
          $"No value recorded for attribute {command.AttributeDefinitionId} on property {command.PropertyId}.");

    context.PropertyAttributeValues.Remove(attributeValue);
    await context.SaveChangesAsync(cancellationToken);

    return Result<DeletePropertyAttributeValueCommandResult>.Success(
        new DeletePropertyAttributeValueCommandResult(true));
  }
}
