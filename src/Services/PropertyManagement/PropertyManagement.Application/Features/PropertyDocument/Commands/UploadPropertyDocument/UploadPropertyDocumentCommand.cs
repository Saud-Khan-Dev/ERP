using FluentValidation;

public sealed record UploadPropertyDocumentCommandResult(Guid Id);

public sealed record UploadPropertyDocumentCommand(Guid PropertyId, PropertyDocumentInput Document)
  : ICommand<Result<UploadPropertyDocumentCommandResult>>;

public class PropertyDocumentInputValidator : AbstractValidator<PropertyDocumentInput>
{
  public PropertyDocumentInputValidator()
  {
    RuleFor(x => x.DocumentType).IsInEnum();
    RuleFor(x => x.RelatedGroup).IsInEnum().When(x => x.RelatedGroup.HasValue);
    RuleFor(x => x.FileUrl).NotEmpty().WithMessage("File URL is required.");
    RuleFor(x => x.FileName).NotEmpty().MaximumLength(255);
    RuleFor(x => x.FileSizeBytes).GreaterThanOrEqualTo(0).When(x => x.FileSizeBytes.HasValue);
    RuleFor(x => x.Remarks).MaximumLength(1000);
  }
}

public class UploadPropertyDocumentCommandValidator : AbstractValidator<UploadPropertyDocumentCommand>
{
  public UploadPropertyDocumentCommandValidator()
  {
    RuleFor(x => x.PropertyId).NotEmpty().WithMessage("PropertyId field is required.");
    RuleFor(x => x.Document).NotNull().SetValidator(new PropertyDocumentInputValidator());
  }
}
