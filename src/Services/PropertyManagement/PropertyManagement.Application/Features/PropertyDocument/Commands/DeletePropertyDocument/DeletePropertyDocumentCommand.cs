using FluentValidation;

public sealed record DeletePropertyDocumentCommandResult(bool IsSuccess);

public sealed record DeletePropertyDocumentCommand(Guid Id) : ICommand<Result<DeletePropertyDocumentCommandResult>>;

public class DeletePropertyDocumentCommandValidator : AbstractValidator<DeletePropertyDocumentCommand>
{
  public DeletePropertyDocumentCommandValidator()
  {
    RuleFor(x => x.Id).NotEmpty().WithMessage("Id field is required.");
  }
}
