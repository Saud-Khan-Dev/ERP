using Microsoft.EntityFrameworkCore;

public class CreateAttributeDefinitionHandler(IApplicationDbContext context)
  : ICommandHandler<CreateAttributeDefinitionCommand, Result<CreateAttributeDefinitionCommandResult>>
{
  public async Task<Result<CreateAttributeDefinitionCommandResult>> Handle(CreateAttributeDefinitionCommand command, CancellationToken cancellationToken)
  {
    var input = command.AttributeDefinition;
    var code = Code.Of(input.Code);

    var codeAlreadyUsed = await context.AttributeDefinitions
        .AnyAsync(d => d.Group == input.Group && d.Code == code, cancellationToken);

    if (codeAlreadyUsed)
      return Result<CreateAttributeDefinitionCommandResult>.Failure(
          $"An attribute with code {code.Value} already exists in the {input.Group} group.");

    var definition = AttributeDefinition.Create(
      attributeDefinitionId: AttributeDefinitionId.Of(Guid.NewGuid()),
      group: input.Group,
      code: code,
      label: Name.Of(input.Label),
      dataType: input.DataType,
      isRequired: input.IsRequired,
      optionsCsv: input.OptionsCsv,
      defaultValue: input.DefaultValue,
      displayOrder: input.DisplayOrder
    );

    await context.AttributeDefinitions.AddAsync(definition, cancellationToken);
    await context.SaveChangesAsync(cancellationToken);

    return Result<CreateAttributeDefinitionCommandResult>.Success(
        new CreateAttributeDefinitionCommandResult(definition.Id.Value));
  }
}
