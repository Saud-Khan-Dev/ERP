using Mapster;
using Microsoft.EntityFrameworkCore;

public class GetPurchaseHandler(IApplicationDbContext context) : IQueryHandler<GetPurchaseQuery, Result<GetPurchaseQueryResult>>
{
  public async Task<Result<GetPurchaseQueryResult>> Handle(GetPurchaseQuery query, CancellationToken cancellationToken)
  {
    var purchase = await context.Purchases.
     Where(p => p.Id == PurchaseId.Of(query.Id))
     .Select(p => new
     {
       Purchase = p,
       Lines = context.PurchaseLines
        .Where(pl => pl.PurchaseId == p.Id)
            .ToList()
     }).FirstOrDefaultAsync(cancellationToken)
    ;
    if (purchase == null)
    {
      throw new PurchaseNotFoundException("Purchase does Not Exist");
    }
    var result = purchase.Adapt<PurchaseDto>();
    return Result<GetPurchaseQueryResult>.Success(new GetPurchaseQueryResult(result));
  }
}