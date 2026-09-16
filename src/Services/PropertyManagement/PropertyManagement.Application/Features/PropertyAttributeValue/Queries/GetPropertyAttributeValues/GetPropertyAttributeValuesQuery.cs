public sealed record GetPropertyAttributeValuesQueryResult(IReadOnlyList<PropertyAttributeValueDto> Attributes);

public sealed record GetPropertyAttributeValuesQuery(Guid PropertyId, AttributeGroup? Group)
  : IQuery<Result<GetPropertyAttributeValuesQueryResult>>;
