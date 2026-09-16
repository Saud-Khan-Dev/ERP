using Microsoft.EntityFrameworkCore;

public class UpdatePropertyDocumentHandler(IApplicationDbContext context)
  : ICommandHandler<UpdatePropertyDocumentCommand, Result<UpdatePropertyDocumentCommandResult>>
{
  public async Task<Result<UpdatePropertyDocumentCommandResult>> Handle(UpdatePropertyDocumentCommand command, CancellationToken cancellationToken)
  {
    var documentId = PropertyDocumentId.Of(command.Id);
    var document = await context.PropertyDocuments
        .FirstOrDefaultAsync(d => d.Id == documentId, cancellationToken);

    if (document is null)
      throw new PropertyDocumentNotFoundException($"Property document {command.Id} was not found.");

    var input = command.Document;

    document.Update(
      documentType: input.DocumentType,
      relatedGroup: input.RelatedGroup,
      fileUrl: FileUrl.Of(input.FileUrl),
      fileName: input.FileName,
      fileSizeBytes: input.FileSizeBytes,
      remarks: input.Remarks
    );

    await context.SaveChangesAsync(cancellationToken);

    return Result<UpdatePropertyDocumentCommandResult>.Success(new UpdatePropertyDocumentCommandResult(true));
  }
}
