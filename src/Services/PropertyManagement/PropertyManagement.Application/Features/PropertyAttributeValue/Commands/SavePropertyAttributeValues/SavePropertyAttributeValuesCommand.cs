using FluentValidation;

public sealed record SavePropertyAttributeValuesCommandResult(int SavedCount);

public sealed record SavePropertyAttributeValuesCommand(Guid PropertyId, IReadOnlyList<AttributeValueInput> Values)
  : ICommand<Result<SavePropertyAttributeValuesCommandResult>>;

public class SavePropertyAttributeValuesCommandValidator : AbstractValidator<SavePropertyAttributeValuesCommand>
{
  public SavePropertyAttributeValuesCommandValidator()
  {
    RuleFor(x => x.PropertyId).NotEmpty().WithMessage("PropertyId field is required.");
    RuleFor(x => x.Values).NotEmpty().WithMessage("At least one attribute value is required.");
    RuleForEach(x => x.Values).ChildRules(value =>
    {
      value.RuleFor(v => v.AttributeDefinitionId)
          .NotEmpty()
          .WithMessage("AttributeDefinitionId field is required.");
    });
  }
}
