public sealed record GetAttributeDefinitionQueryResult(AttributeDefinitionDto AttributeDefinition);

public sealed record GetAttributeDefinitionQuery(Guid Id) : IQuery<Result<GetAttributeDefinitionQueryResult>>;
