public sealed record InventoryTypeDto(
  Guid Id,
  string Code,
  string Name,
  string Description,
  string? FileUrl,
  Guid InventoryCategoryId
);