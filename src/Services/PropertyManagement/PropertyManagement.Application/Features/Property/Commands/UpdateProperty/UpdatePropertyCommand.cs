using FluentValidation;

public sealed record UpdatePropertyCommandResult(bool IsSuccess);

public sealed record UpdatePropertyCommand(Guid Id, PropertyInput Property)
  : ICommand<Result<UpdatePropertyCommandResult>>;

public class UpdatePropertyCommandValidator : AbstractValidator<UpdatePropertyCommand>
{
  public UpdatePropertyCommandValidator()
  {
    RuleFor(x => x.Id).NotEmpty().WithMessage("Id field is required.");
    RuleFor(x => x.Property).NotNull().SetValidator(new PropertyInputValidator());
  }
}
