public sealed record InventoryItemDto(
  Guid Id,
  string Code,
  string Name,
  string Description,
  Guid InventoryTypeId,
  UnitOfMeasureDto UnitOfMeasure,
  InventoryOwnerShipType InventoryOwnerShipType,
  InventoryItemStatus InventoryItemStatus
);

public sealed record UnitOfMeasureDto(
    string Unit,
    decimal Value
);