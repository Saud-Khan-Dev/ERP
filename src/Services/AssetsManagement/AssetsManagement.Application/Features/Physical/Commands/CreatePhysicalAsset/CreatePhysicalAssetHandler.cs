public class CreatePhysicalAssetHandler(IApplicationDbContext context) : ICommandHandler<CreatePhysicalAssetCommand, Result<CreatePhysicalAssetCommandResult>>
{
  public async Task<Result<CreatePhysicalAssetCommandResult>> Handle(CreatePhysicalAssetCommand command, CancellationToken cancellationToken)
  {
    var physicalAsset = Create(command.Physical);
    await context.Physicals.AddAsync(physicalAsset);
    await context.SaveChangesAsync(cancellationToken);
    return Result<CreatePhysicalAssetCommandResult>.Success(new CreatePhysicalAssetCommandResult(physicalAsset.AssetId.Value, physicalAsset.Id.Value));
  }

  private Physical Create(PhysicalAssetDto physical)
  {
    return Physical.Create(PhysicalId.Of(Guid.NewGuid()), AssetId.Of(physical.AssetId));
  }
}