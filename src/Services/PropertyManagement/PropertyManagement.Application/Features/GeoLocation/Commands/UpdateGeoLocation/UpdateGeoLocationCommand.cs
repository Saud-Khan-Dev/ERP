using FluentValidation;

public sealed record UpdateGeoLocationCommandResult(bool IsSuccess);

public sealed record UpdateGeoLocationCommand(Guid Id, GeoLocationInput GeoLocation)
  : ICommand<Result<UpdateGeoLocationCommandResult>>;

public class UpdateGeoLocationCommandValidator : AbstractValidator<UpdateGeoLocationCommand>
{
  public UpdateGeoLocationCommandValidator()
  {
    RuleFor(x => x.Id).NotEmpty().WithMessage("Id field is required.");
    RuleFor(x => x.GeoLocation).NotNull().SetValidator(new GeoLocationInputValidator());
  }
}
