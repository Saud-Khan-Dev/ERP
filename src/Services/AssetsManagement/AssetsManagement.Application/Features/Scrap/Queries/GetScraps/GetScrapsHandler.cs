using Microsoft.EntityFrameworkCore;

public class GetScrapsHandler(IApplicationDbContext context) : IQueryHandler<GetScrapsQuery, Result<GetScrapsQueryResult>>
{
  public async Task<Result<GetScrapsQueryResult>> Handle(GetScrapsQuery request, CancellationToken cancellationToken)
  {
    var scraps = await context.Scraps.ToListAsync();
    if (!scraps.Any())
    {
      return Result<GetScrapsQueryResult>.Failure("No scraps found");
    }
    return Result<GetScrapsQueryResult>.Success(new GetScrapsQueryResult(scraps.ToScrapList()));
  }
}