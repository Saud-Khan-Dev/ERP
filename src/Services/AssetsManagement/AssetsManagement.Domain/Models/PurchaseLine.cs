public class PurchaseLine : Entity<PurchaseLineId>
{
  public PurchaseId PurchaseId { get; private set; } = default!;

  public InventoryItemId ItemId { get; private set; } = default!;

  public decimal Quantity { get; private set; }

  public UnitOfMeasure Unit { get; private set; } = default!;

  public Money UnitPrice { get; private set; } = default!;

  public Money TotalPrice { get; private set; } = default!;
}