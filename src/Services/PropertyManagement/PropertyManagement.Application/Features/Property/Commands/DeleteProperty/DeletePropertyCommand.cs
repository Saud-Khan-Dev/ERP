using FluentValidation;

public sealed record DeletePropertyCommandResult(bool IsSuccess);

public sealed record DeletePropertyCommand(Guid Id) : ICommand<Result<DeletePropertyCommandResult>>;

public class DeletePropertyCommandValidator : AbstractValidator<DeletePropertyCommand>
{
  public DeletePropertyCommandValidator()
  {
    RuleFor(x => x.Id).NotEmpty().WithMessage("Id field is required.");
  }
}
