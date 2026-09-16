using Microsoft.EntityFrameworkCore;

public class DeactivateAttributeDefinitionHandler(IApplicationDbContext context)
  : ICommandHandler<DeactivateAttributeDefinitionCommand, Result<DeactivateAttributeDefinitionCommandResult>>
{
  public async Task<Result<DeactivateAttributeDefinitionCommandResult>> Handle(DeactivateAttributeDefinitionCommand command, CancellationToken cancellationToken)
  {
    var definitionId = AttributeDefinitionId.Of(command.Id);
    var definition = await context.AttributeDefinitions
        .FirstOrDefaultAsync(d => d.Id == definitionId, cancellationToken);

    if (definition is null)
      throw new AttributeDefinitionNotFoundException($"Attribute definition {command.Id} was not found.");

    if (command.IsActive)
      definition.Activate();
    else
      definition.Deactivate();

    await context.SaveChangesAsync(cancellationToken);

    return Result<DeactivateAttributeDefinitionCommandResult>.Success(
        new DeactivateAttributeDefinitionCommandResult(true));
  }
}
