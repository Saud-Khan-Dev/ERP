using Microsoft.EntityFrameworkCore;

public class UpdateScrapHandler(IApplicationDbContext context) : ICommandHandler<UpdateScrapCommand, Result<UpdateScrapCommandResult>>
{
  public async Task<Result<UpdateScrapCommandResult>> Handle(UpdateScrapCommand command, CancellationToken cancellationToken)
  {
    var scrap = await context.Scraps.FirstOrDefaultAsync(s => s.Id == ScrapId.Of(command.Id));
    if (scrap == null)
    {
      throw new ScrapNotFoundException("Scrap Not found");
    }
    UpdateScrap(scrap, command.Scrap);
    await context.SaveChangesAsync(cancellationToken);
    return Result<UpdateScrapCommandResult>.Success(new UpdateScrapCommandResult(true));
  }

  private void UpdateScrap(Scrap scrap, ScrapDto scrapDto)
  {
    scrap.Update(InventoryItemId.Of(scrapDto.InventoryItemId), price: Money.Of(amount: scrap.Price.Amount,
    currency: Currency.Of(scrap.Price.Currency.Value)), total: scrapDto.Total, description: scrapDto.Description);
  }
}