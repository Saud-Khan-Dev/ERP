using Microsoft.EntityFrameworkCore;

public class UploadPropertyDocumentHandler(IApplicationDbContext context)
  : ICommandHandler<UploadPropertyDocumentCommand, Result<UploadPropertyDocumentCommandResult>>
{
  public async Task<Result<UploadPropertyDocumentCommandResult>> Handle(UploadPropertyDocumentCommand command, CancellationToken cancellationToken)
  {
    var propertyId = PropertyId.Of(command.PropertyId);

    var propertyExists = await context.Properties
        .AnyAsync(p => p.Id == propertyId, cancellationToken);

    if (!propertyExists)
      throw new PropertyNotFoundException($"Property {command.PropertyId} was not found.");

    var input = command.Document;

    var document = PropertyDocument.Create(
      propertyDocumentId: PropertyDocumentId.Of(Guid.NewGuid()),
      propertyId: propertyId,
      documentType: input.DocumentType,
      relatedGroup: input.RelatedGroup,
      fileUrl: FileUrl.Of(input.FileUrl),
      fileName: input.FileName,
      fileSizeBytes: input.FileSizeBytes,
      uploadedBy: input.UploadedBy,
      remarks: input.Remarks
    );

    await context.PropertyDocuments.AddAsync(document, cancellationToken);
    await context.SaveChangesAsync(cancellationToken);

    return Result<UploadPropertyDocumentCommandResult>.Success(
        new UploadPropertyDocumentCommandResult(document.Id.Value));
  }
}
