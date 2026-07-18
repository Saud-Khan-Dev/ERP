public sealed class InventoryStock : Aggregate<InventoryStockId>
{
  public InventoryItemId ItemId { get; private set; } = default!;

  public WarehouseId WarehouseId { get; private set; } = default!;

  public decimal AvailableQuantity { get; private set; }

  public decimal ReservedQuantity { get; private set; }

  public static InventoryStock Create(InventoryStockId inventoryStockId, InventoryItemId inventoryItemId, WarehouseId warehouseId, decimal availableQuantity, decimal reservedQuantity)
  {
    return new InventoryStock
    {
      Id = inventoryStockId,
      ItemId = inventoryItemId,
      WarehouseId = warehouseId,
      AvailableQuantity = availableQuantity,
      ReservedQuantity = reservedQuantity
    };
  }

}