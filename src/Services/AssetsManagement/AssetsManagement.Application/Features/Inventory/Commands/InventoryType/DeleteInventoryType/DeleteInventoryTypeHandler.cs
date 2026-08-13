using Microsoft.EntityFrameworkCore;

public class DeleteInventoryTypeHandler(IApplicationDbContext context) : ICommandHandler<DeleteInventoryTypeCommand, Result<DeleteInventoryTypeResult>>
{
  public async Task<Result<DeleteInventoryTypeResult>> Handle(DeleteInventoryTypeCommand command, CancellationToken cancellationToken)
  {
    var inventory = await context.InventoryTypes.FirstOrDefaultAsync(x => x.Id.Value == command.Id);
    if (inventory == null)
    {
      throw new InventoryNotFoundException("Inventory Type NotFound");
    }
    context.InventoryTypes.Remove(inventory);
    await context.SaveChangesAsync(cancellationToken);
    return Result<DeleteInventoryTypeResult>.Success(new DeleteInventoryTypeResult(true));
  }
}