using Microsoft.EntityFrameworkCore;

public class DeletePropertyDocumentHandler(IApplicationDbContext context)
  : ICommandHandler<DeletePropertyDocumentCommand, Result<DeletePropertyDocumentCommandResult>>
{
  public async Task<Result<DeletePropertyDocumentCommandResult>> Handle(DeletePropertyDocumentCommand command, CancellationToken cancellationToken)
  {
    var documentId = PropertyDocumentId.Of(command.Id);
    var document = await context.PropertyDocuments
        .FirstOrDefaultAsync(d => d.Id == documentId, cancellationToken);

    if (document is null)
      throw new PropertyDocumentNotFoundException($"Property document {command.Id} was not found.");

    context.PropertyDocuments.Remove(document);
    await context.SaveChangesAsync(cancellationToken);

    return Result<DeletePropertyDocumentCommandResult>.Success(new DeletePropertyDocumentCommandResult(true));
  }
}
