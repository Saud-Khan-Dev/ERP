public sealed record GetAttributeDefinitionsQueryResult(IReadOnlyList<AttributeDefinitionDto> AttributeDefinitions);

/// Omit <paramref name="Group"/> to list every group's attributes; supply it to render one data-entry form.
public sealed record GetAttributeDefinitionsQuery(AttributeGroup? Group, bool IncludeInactive)
  : IQuery<Result<GetAttributeDefinitionsQueryResult>>;
