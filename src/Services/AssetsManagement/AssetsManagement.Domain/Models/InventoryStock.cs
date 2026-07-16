public sealed class InventoryStock : Entity<InventoryStockId>
{
  public InventoryItemId ItemId { get; private set; } = default!;

  public WarehouseId WarehouseId { get; private set; } = default!;

  public decimal AvailableQuantity { get; private set; }

  public decimal ReservedQuantity { get; private set; }

  public decimal DamagedQuantity { get; private set; }

  public decimal ReorderLevel { get; private set; }

  public decimal ReorderQuantity { get; private set; }
}