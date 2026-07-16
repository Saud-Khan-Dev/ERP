public class InventoryType : Entity<InventoryTypeId>
{
  public Code Code { get; private set; } = default!;
  public Name InventoryName { get; private set; } = default!;
  public string Description { get; private set; } = default!;

}