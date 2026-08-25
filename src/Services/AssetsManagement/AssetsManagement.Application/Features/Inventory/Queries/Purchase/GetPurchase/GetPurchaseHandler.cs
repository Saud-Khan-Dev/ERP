using Mapster;
using Microsoft.EntityFrameworkCore;

public class GetPurchaseHandler(IApplicationDbContext context) : IQueryHandler<GetPurchaseQuery, Result<GetPurchaseQueryResult>>
{
  public async Task<Result<GetPurchaseQueryResult>> Handle(GetPurchaseQuery query, CancellationToken cancellationToken)
  {
    var purchase = await context.PurchaseLines.FirstOrDefaultAsync(p => p.Id == PurchaseLineId.Of(query.Id));
    if (purchase == null)
    {
      throw new PurchaseNotFoundException("Purchase does Not Exist");
    }
    var result = purchase.Adapt<PurchaseDto>();
    return Result<GetPurchaseQueryResult>.Success(new GetPurchaseQueryResult(result));
  }
}