public sealed record ScrapId
{
  public Guid Value { get; }

  private ScrapId(Guid value) => Value = value;

  public static ScrapId Of(Guid value)
  {
    if (value == Guid.Empty)
    {
      throw new DomainException("Scrap Id cannot be empty");
    }
    return new ScrapId(value);
  }
}