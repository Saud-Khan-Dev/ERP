public sealed class InventoryType : Entity<InventoryTypeId>
{
  public Code Code { get; private set; } = default!;
  public Name Name { get; private set; } = default!;
  public string? Description { get; private set; }

  public static InventoryType Create(InventoryTypeId inventoryTypeId, Code code, Name name, string description)
  {
    return new InventoryType
    {
      Id = inventoryTypeId,
      Code = code,
      Name = name,
      Description = description
    };
  }

}