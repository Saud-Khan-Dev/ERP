public sealed record AttributeValueInput(
  Guid AttributeDefinitionId,
  string? Value
);

/// One row of a dynamic data-entry form: the attribute as configured, plus the value captured for this property (null when not yet entered).
public sealed record PropertyAttributeValueDto(
  Guid? Id,
  Guid AttributeDefinitionId,
  AttributeGroup Group,
  string Code,
  string Label,
  AttributeDataType DataType,
  bool IsRequired,
  IReadOnlyList<string> Options,
  int DisplayOrder,
  string? Value
);
