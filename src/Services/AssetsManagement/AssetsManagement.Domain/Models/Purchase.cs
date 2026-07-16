public class Purchase : Entity<PurchaseId>
{
  public PersonId SupplierId { get; private set; } = default!;

  public DateTime PurchaseDate { get; private set; }

  public PurchaseStatus Status { get; private set; } = default!;

  public Currency Currency { get; private set; } = default!;

  public Address DeliveryAddress { get; private set; } = default!;

  public DateTime? ExpectedDeliveryDate { get; private set; }

  public PaymentTerm PaymentTerm { get; private set; } = default!;

  public Money SubTotal { get; private set; } = default!;

  public Money TaxAmount { get; private set; } = default!;

  public Money DiscountAmount { get; private set; } = default!;

  public Money TotalAmount { get; private set; } = default!;

  public string? Remarks { get; private set; }

}