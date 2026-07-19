public class InventoryItem : Aggregate<InventoryItemId>
{
  public Code Code { get; private set; } = default!;
  public Name Name { get; private set; } = default!;
  public string? Description { get; private set; }
  public InventoryCategoryId InventoryCategoryId { get; private set; } = default!;
  public UnitOfMeasure UnitOfMeasure { get; private set; } = default!;
  public InventoryOwnerShipType InventoryOwnerShipType { get; private set; } = default!;
  public InventoryItemStatus Status { get; private set; } = default!;


  public static InventoryItem Create(InventoryItemId inventoryItemId, Name name,Code code ,string description,  InventoryCategoryId inventoryCategoryId, UnitOfMeasure unitOfMeasure, InventoryOwnerShipType inventoryOwnerShipType, InventoryItemStatus inventoryItemStatus)
  {
    return new InventoryItem
    {
      Id = inventoryItemId,
      Name = name,
      Code=code,
      Description = description,
      InventoryCategoryId = inventoryCategoryId,
      UnitOfMeasure = unitOfMeasure,
      InventoryOwnerShipType = inventoryOwnerShipType,
      Status = inventoryItemStatus
    };
  }

  public void Update(InventoryItemId inventoryItemId, Name name, string description,  InventoryCategoryId inventoryCategoryId, UnitOfMeasure unitOfMeasure, InventoryOwnerShipType inventoryOwnerShipType, InventoryItemStatus inventoryItemStatus)
  {
    Id = inventoryItemId;
    Name = name;
    Description = description;
    InventoryCategoryId = inventoryCategoryId;
    UnitOfMeasure = unitOfMeasure;
    InventoryOwnerShipType = inventoryOwnerShipType;
    Status = inventoryItemStatus;
  }

  public void ChangeStatus(InventoryItemStatus status)
  {
    Status = status;
  }

}