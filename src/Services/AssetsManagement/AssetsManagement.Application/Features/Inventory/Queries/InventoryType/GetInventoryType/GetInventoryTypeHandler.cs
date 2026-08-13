using Mapster;
using Microsoft.EntityFrameworkCore;

public class GetInventoryTypeHandler(IApplicationDbContext context) : IQueryHandler<GetInventoryTypeQuery, Result<GetInventoryTypeResult>>
{
  public async Task<Result<GetInventoryTypeResult>> Handle(GetInventoryTypeQuery query, CancellationToken cancellationToken)
  {
    var inventory = await context.InventoryTypes.FirstOrDefaultAsync(x => x.Id.Value == query.Id);
    if (inventory == null)
    {
      throw new InventoryNotFoundException("Inventory Type NotFound");
    }
    var result = inventory.Adapt<GetInventoryTypeResult>();
    return Result<GetInventoryTypeResult>.Success(result);
    throw new NotImplementedException();
  }
}











