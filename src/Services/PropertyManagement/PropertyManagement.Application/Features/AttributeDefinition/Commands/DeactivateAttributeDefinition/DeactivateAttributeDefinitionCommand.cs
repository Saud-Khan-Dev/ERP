using FluentValidation;

public sealed record DeactivateAttributeDefinitionCommandResult(bool IsSuccess);

public sealed record DeactivateAttributeDefinitionCommand(Guid Id, bool IsActive)
  : ICommand<Result<DeactivateAttributeDefinitionCommandResult>>;

public class DeactivateAttributeDefinitionCommandValidator : AbstractValidator<DeactivateAttributeDefinitionCommand>
{
  public DeactivateAttributeDefinitionCommandValidator()
  {
    RuleFor(x => x.Id).NotEmpty().WithMessage("Id field is required.");
  }
}
