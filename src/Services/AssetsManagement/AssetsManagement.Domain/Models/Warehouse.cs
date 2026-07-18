public class Warehouse : Aggregate<WarehouseId>
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

  public static Warehouse Create(
      WarehouseId warehouseId,
      Code code,
      Name name,
      string description,
      Address? address,
      WarehouseType warehouseType,
      bool isDefault,
      PersonId? managerId,
      ContactNumber? contactNumber,
      Email? email)
  {
    ArgumentNullException.ThrowIfNull(code);
    ArgumentNullException.ThrowIfNull(name);

    return new Warehouse
    {
      Id = warehouseId,
      Code = code,
      Name = name,
      Description = description,
      Address = address,
      WarehouseType = warehouseType,
      Status = WarehouseStatus.Active, 
      IsDefault = isDefault,
      ManagerId = managerId,
      ContactNumber = contactNumber,
      Email = email
    };
  }
}