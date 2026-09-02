public sealed record CreatePhysicalCommandResult(Guid AssetId, Guid PhysicalId);
public sealed record CreatePhysicalCommand():ICommand<Result<CreatePhysicalCommandResult>>;