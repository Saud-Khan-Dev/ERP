public sealed record GeoLocationId
{
  public Guid Value { get; }

  private GeoLocationId(Guid value) => Value = value;

  public static GeoLocationId Of(Guid value)
  {
    if (value == Guid.Empty)
    {
      throw new DomainException("GeoLocation Id cannot be empty");
    }
    return new GeoLocationId(value);
  }
}
