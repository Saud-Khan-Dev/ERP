using Microsoft.EntityFrameworkCore;

public class GetPropertyDocumentHandler(IApplicationDbContext context)
  : IQueryHandler<GetPropertyDocumentQuery, Result<GetPropertyDocumentQueryResult>>
{
  public async Task<Result<GetPropertyDocumentQueryResult>> Handle(GetPropertyDocumentQuery query, CancellationToken cancellationToken)
  {
    var documentId = PropertyDocumentId.Of(query.Id);
    var document = await context.PropertyDocuments
        .AsNoTracking()
        .FirstOrDefaultAsync(d => d.Id == documentId, cancellationToken);

    if (document is null)
      throw new PropertyDocumentNotFoundException($"Property document {query.Id} was not found.");

    return Result<GetPropertyDocumentQueryResult>.Success(
        new GetPropertyDocumentQueryResult(document.ToDto()));
  }
}
