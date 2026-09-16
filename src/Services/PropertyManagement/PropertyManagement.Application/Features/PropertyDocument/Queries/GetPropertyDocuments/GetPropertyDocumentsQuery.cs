public sealed record GetPropertyDocumentsQueryResult(IReadOnlyList<PropertyDocumentDto> Documents);

public sealed record GetPropertyDocumentsQuery(Guid PropertyId, AttributeGroup? RelatedGroup)
  : IQuery<Result<GetPropertyDocumentsQueryResult>>;
