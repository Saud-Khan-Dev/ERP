using FluentValidation;

public sealed record CreateAttributeDefinitionCommandResult(Guid Id);

public sealed record CreateAttributeDefinitionCommand(CreateAttributeDefinitionInput AttributeDefinition)
  : ICommand<Result<CreateAttributeDefinitionCommandResult>>;

public class CreateAttributeDefinitionInputValidator : AbstractValidator<CreateAttributeDefinitionInput>
{
  public CreateAttributeDefinitionInputValidator()
  {
    RuleFor(x => x.Group).IsInEnum();
    RuleFor(x => x.Code).Code();
    RuleFor(x => x.Label).Name();
    RuleFor(x => x.DataType).IsInEnum();
    RuleFor(x => x.DisplayOrder).GreaterThanOrEqualTo(0);
    RuleFor(x => x.OptionsCsv)
        .NotEmpty()
        .When(x => x.DataType is AttributeDataType.Dropdown or AttributeDataType.MultiSelect)
        .WithMessage("Dropdown and MultiSelect attributes require options.");
  }
}

public class CreateAttributeDefinitionCommandValidator : AbstractValidator<CreateAttributeDefinitionCommand>
{
  public CreateAttributeDefinitionCommandValidator()
  {
    RuleFor(x => x.AttributeDefinition)
        .NotNull()
        .SetValidator(new CreateAttributeDefinitionInputValidator());
  }
}
