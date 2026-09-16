public sealed record PropertyDocumentId
{
  public Guid Value { get; }

  private PropertyDocumentId(Guid value) => Value = value;

  public static PropertyDocumentId Of(Guid value)
  {
    if (value == Guid.Empty)
    {
      throw new DomainException("Property Document Id cannot be empty");
    }
    return new PropertyDocumentId(value);
  }
}
