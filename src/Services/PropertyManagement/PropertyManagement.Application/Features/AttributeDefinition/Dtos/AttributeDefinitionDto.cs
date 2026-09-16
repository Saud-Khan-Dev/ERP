public sealed record CreateAttributeDefinitionInput(
  AttributeGroup Group,
  string Code,
  string Label,
  AttributeDataType DataType,
  bool IsRequired,
  string? OptionsCsv,
  string? DefaultValue,
  int DisplayOrder
);

public sealed record UpdateAttributeDefinitionInput(
  string Code,
  string Label,
  AttributeDataType DataType,
  bool IsRequired,
  string? OptionsCsv,
  string? DefaultValue,
  int DisplayOrder
);

public sealed record AttributeDefinitionDto(
  Guid Id,
  AttributeGroup Group,
  string Code,
  string Label,
  AttributeDataType DataType,
  bool IsRequired,
  IReadOnlyList<string> Options,
  string? DefaultValue,
  int DisplayOrder,
  bool IsActive
);
