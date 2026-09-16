public class PropertyAttributeValue : Aggregate<PropertyAttributeValueId>
{
  public PropertyId PropertyId { get; private set; } = default!;
  public AttributeDefinitionId AttributeDefinitionId { get; private set; } = default!;
  public AttributeDataType DataType { get; private set; }
  public string? Value { get; private set; }

  public static PropertyAttributeValue Create(
      PropertyAttributeValueId propertyAttributeValueId,
      PropertyId propertyId,
      AttributeDefinition definition,
      string? value)
  {
    ArgumentNullException.ThrowIfNull(propertyId);
    ArgumentNullException.ThrowIfNull(definition);

    if (!definition.IsActive)
      throw new DomainException($"'{definition.Label.Value}' is no longer available for data entry.");

    definition.EnsureValueIsValid(value);

    return new PropertyAttributeValue
    {
      Id = propertyAttributeValueId,
      PropertyId = propertyId,
      AttributeDefinitionId = definition.Id,
      DataType = definition.DataType,
      Value = Normalize(value)
    };
  }

  public void UpdateValue(AttributeDefinition definition, string? value)
  {
    ArgumentNullException.ThrowIfNull(definition);

    if (definition.Id != AttributeDefinitionId)
      throw new DomainException("Attribute definition does not match the stored value.");

    if (!definition.IsActive)
      throw new DomainException($"'{definition.Label.Value}' is no longer available for data entry.");

    definition.EnsureValueIsValid(value);

    DataType = definition.DataType;
    Value = Normalize(value);
  }

  private static string? Normalize(string? value) =>
      string.IsNullOrWhiteSpace(value) ? null : value.Trim();
}
