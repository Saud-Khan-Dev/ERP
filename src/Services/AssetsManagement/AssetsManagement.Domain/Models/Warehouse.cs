public class Warehouse : Entity<WarehouseId>
{

  public Code Code { get; private set; } = default!;

  public Name Name { get; private set; } = default!;

  public string Description { get; private set; } = default!;

  public Address? Address { get; private set; }

  public WarehouseType WarehouseType { get; private set; }

  public WarehouseStatus Status { get; private set; }

  public bool IsDefault { get; private set; }

  public PersonId? ManagerId { get; private set; }

  public ContactNumber? ContactNumber { get; private set; }

  public Email? Email { get; private set; }

}