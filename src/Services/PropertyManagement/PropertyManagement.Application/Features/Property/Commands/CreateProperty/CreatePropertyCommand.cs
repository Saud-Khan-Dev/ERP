using FluentValidation;

public sealed record CreatePropertyCommandResult(Guid Id, Guid GeoLocationId);

/// Supply either a new <paramref name="GeoLocation"/> to create inline, or the id of an existing unlinked one.
public sealed record CreatePropertyCommand(PropertyInput Property, GeoLocationInput? GeoLocation, Guid? GeoLocationId)
  : ICommand<Result<CreatePropertyCommandResult>>;

public class PropertyInputValidator : AbstractValidator<PropertyInput>
{
  public PropertyInputValidator()
  {
    RuleFor(x => x.PropertyCode).Code();
    RuleFor(x => x.PropertyName).Name();
    RuleFor(x => x.PropertyType).IsInEnum();
    RuleFor(x => x.OwnershipType).IsInEnum();
    RuleFor(x => x.Status).IsInEnum();
    RuleFor(x => x.AreaSqFt).GreaterThanOrEqualTo(0);
    RuleFor(x => x.CoveredAreaSqFt).GreaterThanOrEqualTo(0);
    RuleFor(x => x.LandAreaSqFt).GreaterThanOrEqualTo(0);
  }
}

public class GeoLocationInputValidator : AbstractValidator<GeoLocationInput>
{
  public GeoLocationInputValidator()
  {
    RuleFor(x => x.Code).Code();
    RuleFor(x => x.Latitude).InclusiveBetween(-90, 90);
    RuleFor(x => x.Longitude).InclusiveBetween(-180, 180);
    RuleFor(x => x.GeoFenceRadius).GreaterThanOrEqualTo(0).When(x => x.GeoFenceRadius.HasValue);
    RuleFor(x => x.CoordinateSystem)
        .NotEmpty()
        .WithMessage("Coordinate system is required.")
        .MaximumLength(50);
  }
}

public class CreatePropertyCommandValidator : AbstractValidator<CreatePropertyCommand>
{
  public CreatePropertyCommandValidator()
  {
    RuleFor(x => x.Property).NotNull().SetValidator(new PropertyInputValidator());

    RuleFor(x => x)
        .Must(x => x.GeoLocation is not null ^ x.GeoLocationId.HasValue)
        .WithMessage("Supply either a new geoLocation or an existing geoLocationId, but not both.");

    RuleFor(x => x.GeoLocation!)
        .SetValidator(new GeoLocationInputValidator())
        .When(x => x.GeoLocation is not null);
  }
}
