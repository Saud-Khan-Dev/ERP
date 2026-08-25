using Microsoft.EntityFrameworkCore;

public class RemovePurchaseLineHandler(IApplicationDbContext context) : ICommandHandler<RemovePurchaseLineCommand, Result<RemovePurchaseLineCommandResult>>
{
  public async Task<Result<RemovePurchaseLineCommandResult>> Handle(RemovePurchaseLineCommand command, CancellationToken cancellationToken)
  {
    var line = await context.PurchaseLines.FirstOrDefaultAsync(l => l.Id == PurchaseLineId.Of(command.Id));
    if (line == null)
    {
      throw new PurchaseNotFoundException("Purchase Line Not Exist");
    }

    context.PurchaseLines.Remove(line);
    await context.SaveChangesAsync(cancellationToken);
    return Result<RemovePurchaseLineCommandResult>.Success(new RemovePurchaseLineCommandResult(true));

  }
}