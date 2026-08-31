public record ScrapDto(
  Guid Id,
  Guid InventoryItemId,
  MoneyDto Price,
  decimal Total,
  string Description
);

