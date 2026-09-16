using FluentValidation;

public sealed record DeleteGeoLocationCommandResult(bool IsSuccess);

public sealed record DeleteGeoLocationCommand(Guid Id) : ICommand<Result<DeleteGeoLocationCommandResult>>;

public class DeleteGeoLocationCommandValidator : AbstractValidator<DeleteGeoLocationCommand>
{
  public DeleteGeoLocationCommandValidator()
  {
    RuleFor(x => x.Id).NotEmpty().WithMessage("Id field is required.");
  }
}
