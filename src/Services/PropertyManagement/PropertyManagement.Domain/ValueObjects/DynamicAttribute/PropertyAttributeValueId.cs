public sealed record PropertyAttributeValueId
{
  public Guid Value { get; }

  private PropertyAttributeValueId(Guid value) => Value = value;

  public static PropertyAttributeValueId Of(Guid value)
  {
    if (value == Guid.Empty)
    {
      throw new DomainException("Property Attribute Value Id cannot be empty");
    }
    return new PropertyAttributeValueId(value);
  }
}
