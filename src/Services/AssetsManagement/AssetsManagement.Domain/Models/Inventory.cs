public class Inventory : Entity<InventoryItemId>
{
  public Code InventoryCode { get; private set; } = default!;
  public Name InventoryName { get; private set; } = default!;
  public string Description { get; private set; } = default!;
  public InventoryTypeId InventoryType { get; private set; } = default!;
  public InventoryCategoryId InventoryCategoryId { get; private set; } = default!;
  public UnitOfMeasure UnitOfMeasure { get; private set; } = default!;
  public WarehouseId WarehouseId { get; private set; } = default!;
  public InventoryOwnerShipType InventoryOwnerShipType { get; private set; } = default!;
  public PersonId SupplierId { get; private set; } = default!;
  public InventoryStatus Status { get; private set; } = default!;
  
}