public class Person : Entity<PersonId>
{
  public Name FirstName { get; private set; } = default!;
  public Name LastName { get; private set; } = default!;
  public Email? Email { get; private set; }
  public string? PhoneNumber { get; private set; }
  public Address? Address { get; private set; }

  private Person() { }

  public static Person Create(
      Guid personId,
      Name firstName,
      Name lastName,
      Email? email,
      string? phoneNumber,
      Address? address)
  {
    return new Person
    {
      Id = PersonId.Of(personId),
      FirstName = firstName,
      LastName = lastName,
      Email = email,
      PhoneNumber = phoneNumber,
      Address = address
    };
  }

  public void Update(
      Name firstName,
      Name lastName,
      Email? email,
      string? phoneNumber,
      Address? address)
  {
    FirstName = firstName;
    LastName = lastName;
    Email = email;
    PhoneNumber = phoneNumber;
    Address = address;
  }
}