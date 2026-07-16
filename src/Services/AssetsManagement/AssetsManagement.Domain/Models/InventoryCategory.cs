public sealed class InventoryCategory : Entity<InventoryCategoryId>
{
  public InventoryTypeId InventoryTypeId { get; private set; } = default!;
  public Code Code { get; private set; } = default!;
  public Name Name { get; private set; } = default!;
  public string? Description { get; private set; }

  public static InventoryCategory Create(InventoryCategoryId inventoryCategoryId, Code code,Name name ,string description)
  {
    return new InventoryCategory
    {
      Id = inventoryCategoryId,
      Code = code,
      Name= name,
      Description = description
    };
  }
}