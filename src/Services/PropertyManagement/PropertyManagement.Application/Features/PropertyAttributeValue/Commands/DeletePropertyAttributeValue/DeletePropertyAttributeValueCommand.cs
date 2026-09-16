using FluentValidation;

public sealed record DeletePropertyAttributeValueCommandResult(bool IsSuccess);

/// Clears a single captured value, leaving the attribute definition itself untouched.
public sealed record DeletePropertyAttributeValueCommand(Guid PropertyId, Guid AttributeDefinitionId)
  : ICommand<Result<DeletePropertyAttributeValueCommandResult>>;

public class DeletePropertyAttributeValueCommandValidator : AbstractValidator<DeletePropertyAttributeValueCommand>
{
  public DeletePropertyAttributeValueCommandValidator()
  {
    RuleFor(x => x.PropertyId).NotEmpty().WithMessage("PropertyId field is required.");
    RuleFor(x => x.AttributeDefinitionId).NotEmpty().WithMessage("AttributeDefinitionId field is required.");
  }
}
