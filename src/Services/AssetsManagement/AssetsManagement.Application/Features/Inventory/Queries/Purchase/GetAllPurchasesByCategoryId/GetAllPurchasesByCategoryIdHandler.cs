using Microsoft.EntityFrameworkCore;

public class GetAllPurchasesByCategoryIdHandler(IApplicationDbContext context) : IQueryHandler<GetAllPurchasesByCategoryIdQuery, Result<GetAllPurchasesByCategoryIdQueryResult>>
{
  public async Task<Result<GetAllPurchasesByCategoryIdQueryResult>> Handle(GetAllPurchasesByCategoryIdQuery request, CancellationToken cancellationToken)
  {

    var purchase = await context.PurchaseLines
    .Join(
     context.InventoryItems,
     line => line.ItemId,
     item => item.Id,

    (line, item) => new
    {
      line,
      item
    })
    .Join(
      context.InventoryTypes,
      x => x.item.InventoryTypeId,
      type => type.Id,

      (x, type) => new
      {
        x.line,
        type
      }
    )
    .Where(
      x => x.type.InventoryCategoryId == InventoryCategoryId.Of(request.CategoryId)
      )
    .SumAsync(x => x.line.LineTotal, cancellationToken);



    Console.WriteLine(purchase);


    return Result<GetAllPurchasesByCategoryIdQueryResult>.Success(new GetAllPurchasesByCategoryIdQueryResult());
  }
}
