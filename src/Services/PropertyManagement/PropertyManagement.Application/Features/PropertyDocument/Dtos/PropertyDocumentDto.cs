public sealed record PropertyDocumentInput(
  DocumentType DocumentType,
  AttributeGroup? RelatedGroup,
  string FileUrl,
  string FileName,
  long? FileSizeBytes,
  string? UploadedBy,
  string? Remarks
);

public sealed record PropertyDocumentDto(
  Guid Id,
  Guid PropertyId,
  DocumentType DocumentType,
  AttributeGroup? RelatedGroup,
  string FileUrl,
  string FileName,
  long? FileSizeBytes,
  DateTime UploadedOn,
  string? UploadedBy,
  string? Remarks
);
