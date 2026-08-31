public class CreateScrapHandler(IApplicationDbContext context) : ICommandHandler<CreateScrapCommand, Result<CreateScrapCommandResult>>
{
  public async Task<Result<CreateScrapCommandResult>> Handle(CreateScrapCommand command, CancellationToken cancellationToken)
  {
    var scrap = CreateScrap(command.Scrap);
    await context.Scraps.AddAsync(scrap);
    await context.SaveChangesAsync(cancellationToken);
    return Result<CreateScrapCommandResult>.Success(new CreateScrapCommandResult(scrap.Id.Value));
  }

  private Scrap CreateScrap(ScrapDto scrap)
  {
    return Scrap.Create(scrapId: ScrapId.Of(Guid.NewGuid()),
    inventoryItemId: InventoryItemId.Of(scrap.InventoryItemId),
    price: Money.Of(amount: scrap.Price.Amount,
    currency: Currency.Of(scrap.Price.Currency.Code)),
    total: scrap.Total, description: scrap.Description);
  }
}