using FluentValidation;

public sealed record UpdatePropertyDocumentCommandResult(bool IsSuccess);

public sealed record UpdatePropertyDocumentCommand(Guid Id, PropertyDocumentInput Document)
  : ICommand<Result<UpdatePropertyDocumentCommandResult>>;

public class UpdatePropertyDocumentCommandValidator : AbstractValidator<UpdatePropertyDocumentCommand>
{
  public UpdatePropertyDocumentCommandValidator()
  {
    RuleFor(x => x.Id).NotEmpty().WithMessage("Id field is required.");
    RuleFor(x => x.Document).NotNull().SetValidator(new PropertyDocumentInputValidator());
  }
}
