public static class ScrapExtension
{
  public static IEnumerable<ScrapDto> ToScrapList(this IEnumerable<Scrap> scraps)
  {
    return scraps.Select(s => new ScrapDto(
     Id: s.Id.Value,

     InventoryItemId: s.InventoryItemId.Value,

     Price: new MoneyDto(Amount: s.Price.Amount, new CurrencyDto(Code: s.Price.Currency.Value)),

    Total: s.Total,

    Description: s.Description
    ));
  }
}