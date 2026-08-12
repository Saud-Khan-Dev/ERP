public sealed record InventoryTypeDto(
  Guid id,
  Code Code,
  Name Name,
  string Description,
  InventoryCategoryId InventoryCategoryId
);