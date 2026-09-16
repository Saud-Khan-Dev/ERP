using Microsoft.EntityFrameworkCore;

public class GetPropertyDocumentsHandler(IApplicationDbContext context)
  : IQueryHandler<GetPropertyDocumentsQuery, Result<GetPropertyDocumentsQueryResult>>
{
  public async Task<Result<GetPropertyDocumentsQueryResult>> Handle(GetPropertyDocumentsQuery query, CancellationToken cancellationToken)
  {
    var propertyId = PropertyId.Of(query.PropertyId);

    var documents = context.PropertyDocuments
        .AsNoTracking()
        .Where(d => d.PropertyId == propertyId);

    if (query.RelatedGroup.HasValue)
      documents = documents.Where(d => d.RelatedGroup == query.RelatedGroup.Value);

    var result = await documents
        .OrderByDescending(d => d.UploadedOn)
        .ToListAsync(cancellationToken);

    return Result<GetPropertyDocumentsQueryResult>.Success(
        new GetPropertyDocumentsQueryResult(result.Select(d => d.ToDto()).ToList()));
  }
}
