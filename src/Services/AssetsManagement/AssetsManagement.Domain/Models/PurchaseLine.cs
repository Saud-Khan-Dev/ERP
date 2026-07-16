public class PurchaseLine : Entity<PurchaseLineId>
{
  public InventoryItemId ItemId { get; private set; } = default!;

  public decimal OrderedQuantity { get; private set; }

  public decimal ReceivedQuantity { get; private set; }

  public UnitOfMeasure UnitOfMeasure { get; private set; } = default!;

  public Money UnitPrice { get; private set; } = default!;

  public Money DiscountAmount { get; private set; } = default!;

  public Money TaxAmount { get; private set; } = default!;

  public Money LineTotal { get; private set; } = default!;

  public string? Remarks { get; private set; }
}