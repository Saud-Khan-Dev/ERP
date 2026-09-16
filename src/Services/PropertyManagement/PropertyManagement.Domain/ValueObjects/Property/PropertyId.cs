public sealed record PropertyId
{
  public Guid Value { get; }

  private PropertyId(Guid value) => Value = value;

  public static PropertyId Of(Guid value)
  {
    if (value == Guid.Empty)
    {
      throw new DomainException("Property Id cannot be empty");
    }
    return new PropertyId(value);
  }
}
