public sealed class PurchaseLine : Entity<PurchaseLineId>
{
  public InventoryItemId ItemId { get; private set; } = default!;
  public PurchaseId PurchaseId { get; private set; } = default!;
  public decimal OrderedQuantity { get; private set; }
  public decimal ReceivedQuantity { get; private set; }
  public UnitOfMeasure UnitOfMeasure { get; private set; } = default!;
  public Currency Currency { get; private set; } = default!;
  public Money UnitPrice { get; private set; } = default!;

  public Money DiscountAmount { get; private set; } = default!;

  public Money TaxAmount { get; private set; } = default!;

  public Money LineTotal { get; private set; } = default!;

  public string? Remarks { get; private set; }


  public static PurchaseLine Create(PurchaseLineId purchaseLineId, PurchaseId purchaseId, InventoryItemId inventoryItemId, decimal orderedQuantity, decimal receivedQuantity, UnitOfMeasure unitOfMeasure, Money unitPrice, Money discountAmount, Money taxAmount, Currency currency, string? remarks)
  {
    return new PurchaseLine
    {
      Id = purchaseLineId,
      PurchaseId = purchaseId,
      ItemId = inventoryItemId,
      OrderedQuantity = orderedQuantity,
      ReceivedQuantity = receivedQuantity,
      UnitOfMeasure = unitOfMeasure,
      UnitPrice = unitPrice,
      Currency = currency,
      DiscountAmount = discountAmount,
      TaxAmount = taxAmount,
      LineTotal = CalculateLineTotal(
                orderedQuantity,
                unitPrice,
                discountAmount,
                taxAmount,
                currency
                ),
      Remarks = remarks
    };
  }

  private static Money CalculateLineTotal(
    decimal quantity,
    Money unitPrice,
    Money discountAmount,
    Money taxAmount,
    Currency currency
    )
  {
    decimal amount = (unitPrice.Amount * quantity) - discountAmount.Amount + taxAmount.Amount;
    return Money.Of(
      amount
      , currency
    );
  }
}