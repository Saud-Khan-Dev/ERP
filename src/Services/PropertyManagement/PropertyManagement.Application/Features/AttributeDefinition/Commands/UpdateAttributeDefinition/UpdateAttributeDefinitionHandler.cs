using Microsoft.EntityFrameworkCore;

public class UpdateAttributeDefinitionHandler(IApplicationDbContext context)
  : ICommandHandler<UpdateAttributeDefinitionCommand, Result<UpdateAttributeDefinitionCommandResult>>
{
  public async Task<Result<UpdateAttributeDefinitionCommandResult>> Handle(UpdateAttributeDefinitionCommand command, CancellationToken cancellationToken)
  {
    var definitionId = AttributeDefinitionId.Of(command.Id);
    var definition = await context.AttributeDefinitions
        .FirstOrDefaultAsync(d => d.Id == definitionId, cancellationToken);

    if (definition is null)
      throw new AttributeDefinitionNotFoundException($"Attribute definition {command.Id} was not found.");

    var input = command.AttributeDefinition;
    var code = Code.Of(input.Code);

    var codeAlreadyUsed = await context.AttributeDefinitions
        .AnyAsync(d => d.Group == definition.Group && d.Code == code && d.Id != definitionId, cancellationToken);

    if (codeAlreadyUsed)
      return Result<UpdateAttributeDefinitionCommandResult>.Failure(
          $"An attribute with code {code.Value} already exists in the {definition.Group} group.");

    definition.Update(
      code: code,
      label: Name.Of(input.Label),
      dataType: input.DataType,
      isRequired: input.IsRequired,
      optionsCsv: input.OptionsCsv,
      defaultValue: input.DefaultValue,
      displayOrder: input.DisplayOrder
    );

    await context.SaveChangesAsync(cancellationToken);

    return Result<UpdateAttributeDefinitionCommandResult>.Success(new UpdateAttributeDefinitionCommandResult(true));
  }
}
