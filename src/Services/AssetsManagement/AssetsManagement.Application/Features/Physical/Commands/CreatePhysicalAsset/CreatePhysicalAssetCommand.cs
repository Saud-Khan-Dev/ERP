using FluentValidation;

public sealed record CreatePhysicalAssetCommandResult(Guid AssetId, Guid PhysicalId);
public sealed record CreatePhysicalAssetCommand(PhysicalAssetDto Physical) : ICommand<Result<CreatePhysicalAssetCommandResult>>;



public class PhysicalAssetDtoValidator : AbstractValidator<PhysicalAssetDto>
{
  public PhysicalAssetDtoValidator()
  {
    RuleFor(x => x.AssetId).NotEmpty().WithMessage("AssetId is required.");
    RuleFor(x => x.PhysicalId).NotEmpty().WithMessage("PhysicalId is required.");
  }
}