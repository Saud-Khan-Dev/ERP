using Microsoft.EntityFrameworkCore;

public class DeleteScrapHandler(IApplicationDbContext context) : ICommandHandler<DeleteScrapCommand, Result<DeleteScrapCommandResult>>
{
  public async Task<Result<DeleteScrapCommandResult>> Handle(DeleteScrapCommand command, CancellationToken cancellationToken)
  {
    var scrap = await context.Scraps.FirstOrDefaultAsync(s => s.Id == ScrapId.Of(command.Id));
    if (scrap == null)
    {
      throw new ScrapNotFoundException("Scrap Not found");
    }
    context.Scraps.Remove(scrap);
    await context.SaveChangesAsync(cancellationToken);
    return Result<DeleteScrapCommandResult>.Success(new DeleteScrapCommandResult(true));
  }
}