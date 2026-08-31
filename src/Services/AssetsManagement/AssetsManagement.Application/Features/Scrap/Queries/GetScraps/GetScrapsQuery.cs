public sealed record GetScrapsQueryResult(IEnumerable<ScrapDto> Scraps);
public sealed record GetScrapsQuery() : IQuery<Result<GetScrapsQueryResult>>;