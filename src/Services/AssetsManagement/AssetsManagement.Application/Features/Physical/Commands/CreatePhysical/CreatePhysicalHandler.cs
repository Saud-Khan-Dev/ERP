public class CreatePhysicalHandler : ICommandHandler<CreatePhysicalCommand, Result<CreatePhysicalCommandResult>>
{
  public Task<Result<CreatePhysicalCommandResult>> Handle(CreatePhysicalCommand request, CancellationToken cancellationToken)
  {
    throw new NotImplementedException();
  }
}