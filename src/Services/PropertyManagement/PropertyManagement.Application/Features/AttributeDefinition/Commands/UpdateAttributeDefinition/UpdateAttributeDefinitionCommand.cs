using FluentValidation;

public sealed record UpdateAttributeDefinitionCommandResult(bool IsSuccess);

public sealed record UpdateAttributeDefinitionCommand(Guid Id, UpdateAttributeDefinitionInput AttributeDefinition)
  : ICommand<Result<UpdateAttributeDefinitionCommandResult>>;

public class UpdateAttributeDefinitionInputValidator : AbstractValidator<UpdateAttributeDefinitionInput>
{
  public UpdateAttributeDefinitionInputValidator()
  {
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

public class UpdateAttributeDefinitionCommandValidator : AbstractValidator<UpdateAttributeDefinitionCommand>
{
  public UpdateAttributeDefinitionCommandValidator()
  {
    RuleFor(x => x.Id).NotEmpty().WithMessage("Id field is required.");
    RuleFor(x => x.AttributeDefinition)
        .NotNull()
        .SetValidator(new UpdateAttributeDefinitionInputValidator());
  }
}
