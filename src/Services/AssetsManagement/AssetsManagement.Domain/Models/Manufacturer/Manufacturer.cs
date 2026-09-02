public class Manufacturer : Entity<ManufacturerId>
{
  public Name Name { get; private set; } = default!;
  public string? Description { get; private set; }
  public string? ContactNumber { get; private set; }
  public Email? Email { get; private set; }
  public Address? Address { get; private set; }

  private Manufacturer() { }

  public static Manufacturer Create(
      Guid manufacturerId,
      Name name,
      string? description,
      string? contactNumber,
      Email? email,
      Address? address)
  {
    return new Manufacturer
    {
      Id = ManufacturerId.Of(manufacturerId),
      Name = name,
      Description = description,
      ContactNumber = contactNumber,
      Email = email,
      Address = address
    };
  }

  public void Update(
      Name name,
      string? description,
      string? contactNumber,
      Email? email,
      Address? address)
  {
    Name = name;
    Description = description;
    ContactNumber = contactNumber;
    Email = email;
    Address = address;
  }
}