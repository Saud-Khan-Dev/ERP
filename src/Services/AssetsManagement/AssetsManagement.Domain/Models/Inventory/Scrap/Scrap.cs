public class Scrap : Entity<ScrapId>
{
  public InventoryItemId InventoryItemId { get; private set; } = default!;
  public Money Price { get; private set; } = default!;
  public decimal Total { get; private set; } = default!;
  public string Description { get; private set; } = default!;


  public static Scrap Create(ScrapId scrapId, InventoryItemId inventoryItemId, Money price, decimal total, string description)
  {
    return new Scrap
    {
      Id = scrapId,
      InventoryItemId = inventoryItemId,
      Price = price,
      Total = total,
      Description = description
    };
  }

  public void Update(InventoryItemId inventoryItemId, Money price, decimal total, string description)
  {
    InventoryItemId = inventoryItemId;
    Price = price;
    Total = total;
    Description = description;
  }

}