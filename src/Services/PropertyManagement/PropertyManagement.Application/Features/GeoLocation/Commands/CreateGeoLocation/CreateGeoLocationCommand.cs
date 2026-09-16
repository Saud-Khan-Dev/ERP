using FluentValidation;

public sealed record CreateGeoLocationCommandResult(Guid Id);

public sealed record CreateGeoLocationCommand(GeoLocationInput GeoLocation)
  : ICommand<Result<CreateGeoLocationCommandResult>>;

public class CreateGeoLocationCommandValidator : AbstractValidator<CreateGeoLocationCommand>
{
  public CreateGeoLocationCommandValidator()
  {
    RuleFor(x => x.GeoLocation).NotNull().SetValidator(new GeoLocationInputValidator());
  }
}
