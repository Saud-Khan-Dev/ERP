using Mapster;
using Microsoft.EntityFrameworkCore;

public class GetScrapHandler(IApplicationDbContext context) : IQueryHandler<GetScrapQuery, Result<GetScrapQueryResult>>
{
  public async Task<Result<GetScrapQueryResult>> Handle(GetScrapQuery query, CancellationToken cancellationToken)
  {
    var scrap = await context.Scraps.FirstOrDefaultAsync(s => s.Id == ScrapId.Of(query.Id));
    if (scrap == null)
    {
      throw new ScrapNotFoundException("Scrap Not found");
    }
    var res = scrap.Adapt<ScrapDto>();
    return Result<GetScrapQueryResult>.Success(new GetScrapQueryResult(res));
  }
}