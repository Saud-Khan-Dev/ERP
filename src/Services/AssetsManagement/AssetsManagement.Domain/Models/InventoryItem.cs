public class InventoryItem : Entity<InventoryItemId>
{
  public Code ItemCode { get; private set; } = default!;
  public Name Name { get; private set; } = default!;
  public string? Description { get; private set; }
  public InventoryTypeId InventoryTypeId { get; private set; } = default!;
  public InventoryCategoryId InventoryCategoryId { get; private set; } = default!;
  public UnitOfMeasure UnitOfMeasure { get; private set; } = default!;
  public InventoryOwnerShipType InventoryOwnerShipType { get; private set; } = default!;
  public InventoryItemStatus Status { get; private set; } = default!;


  public static InventoryItem Create(InventoryItemId inventoryItemId, Name name, string description, InventoryTypeId inventoryTypeId, InventoryCategoryId inventoryCategoryId, UnitOfMeasure unitOfMeasure, InventoryOwnerShipType inventoryOwnerShipType, InventoryItemStatus inventoryItemStatus)
  {
    return new InventoryItem
    {
      Id = inventoryItemId,
      Name = name,
      Description = description,
      InventoryTypeId = inventoryTypeId,
      InventoryCategoryId = inventoryCategoryId,
      UnitOfMeasure = unitOfMeasure,
      InventoryOwnerShipType = inventoryOwnerShipType,
      Status = inventoryItemStatus
    };
  }

}