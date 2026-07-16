public sealed record Address
{
  public string Street { get; }
  public string? Building { get; }
  public string City { get; }
  public string? State { get; }
  public string PostalCode { get; }
  public string Country { get; }

  private Address(
      string street,
      string? building,
      string city,
      string? state,
      string postalCode,
      string country)
  {
    Street = street;
    Building = building;
    City = city;
    State = state;
    PostalCode = postalCode;
    Country = country;
  }


  public static Address Of(
      string street,
      string? building,
      string city,
      string? state,
      string postalCode,
      string country)
  {
    if (string.IsNullOrWhiteSpace(street))
      throw new DomainException("Street is required.");

    if (street.Length > 200)
      throw new DomainException("Street cannot exceed 200 characters.");


    if (string.IsNullOrWhiteSpace(city))
      throw new DomainException("City is required.");

    if (city.Length > 100)
      throw new DomainException("City cannot exceed 100 characters.");


    if (string.IsNullOrWhiteSpace(postalCode))
      throw new DomainException("Postal code is required.");

    if (postalCode.Length > 20)
      throw new DomainException("Postal code cannot exceed 20 characters.");


    if (string.IsNullOrWhiteSpace(country))
      throw new DomainException("Country is required.");

    if (country.Length > 100)
      throw new DomainException("Country cannot exceed 100 characters.");


    return new Address(
        street.Trim(),
        building?.Trim(),
        city.Trim(),
        state?.Trim(),
        postalCode.Trim(),
        country.Trim()
    );
  }
}