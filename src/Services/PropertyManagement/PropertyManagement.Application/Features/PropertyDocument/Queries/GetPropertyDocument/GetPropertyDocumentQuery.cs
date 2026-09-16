public sealed record GetPropertyDocumentQueryResult(PropertyDocumentDto Document);

public sealed record GetPropertyDocumentQuery(Guid Id) : IQuery<Result<GetPropertyDocumentQueryResult>>;
