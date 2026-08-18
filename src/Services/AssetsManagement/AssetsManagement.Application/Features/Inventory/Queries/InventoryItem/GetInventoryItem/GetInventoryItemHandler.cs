using Mapster;
using Microsoft.EntityFrameworkCore;

public class GetInventoryItemHandler(IApplicationDbContext context) : IQueryHandler<GetInventoryItemQuery, Result<GetInventoryItemQueryResult>>
{
  public async Task<Result<GetInventoryItemQueryResult>> Handle(GetInventoryItemQuery query, CancellationToken cancellationToken)
  {
    var item = await context.InventoryItems.FirstOrDefaultAsync(i => i.Id == InventoryItemId.Of(query.Id));
    if (item == null)
    {
      throw new InventoryItemNotFoundException("Inventory Item not Found");
    }
    var result = item.Adapt<InventoryItemDto>();
    return Result<GetInventoryItemQueryResult>.Success(new GetInventoryItemQueryResult(result));
  }
}