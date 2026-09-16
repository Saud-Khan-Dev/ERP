using Microsoft.EntityFrameworkCore;

public class DeleteAttributeDefinitionHandler(IApplicationDbContext context)
  : ICommandHandler<DeleteAttributeDefinitionCommand, Result<DeleteAttributeDefinitionCommandResult>>
{
  public async Task<Result<DeleteAttributeDefinitionCommandResult>> Handle(DeleteAttributeDefinitionCommand command, CancellationToken cancellationToken)
  {
    var definitionId = AttributeDefinitionId.Of(command.Id);
    var definition = await context.AttributeDefinitions
        .FirstOrDefaultAsync(d => d.Id == definitionId, cancellationToken);

    if (definition is null)
      throw new AttributeDefinitionNotFoundException($"Attribute definition {command.Id} was not found.");

    var capturedValues = await context.PropertyAttributeValues
        .CountAsync(v => v.AttributeDefinitionId == definitionId, cancellationToken);

    if (capturedValues > 0)
      return Result<DeleteAttributeDefinitionCommandResult>.Failure(
          $"This attribute has {capturedValues} recorded value(s) and cannot be deleted. Deactivate it instead.");

    context.AttributeDefinitions.Remove(definition);
    await context.SaveChangesAsync(cancellationToken);

    return Result<DeleteAttributeDefinitionCommandResult>.Success(new DeleteAttributeDefinitionCommandResult(true));
  }
}
