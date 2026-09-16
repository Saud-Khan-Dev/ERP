public class PropertyDocument : Aggregate<PropertyDocumentId>
{
  public PropertyId PropertyId { get; private set; } = default!;
  public DocumentType DocumentType { get; private set; }
  public AttributeGroup? RelatedGroup { get; private set; }
  public FileUrl FileUrl { get; private set; } = default!;
  public string FileName { get; private set; } = default!;
  public long? FileSizeBytes { get; private set; }
  public DateTime UploadedOn { get; private set; }
  public string? UploadedBy { get; private set; }
  public string? Remarks { get; private set; }

  public static PropertyDocument Create(
      PropertyDocumentId propertyDocumentId,
      PropertyId propertyId,
      DocumentType documentType,
      AttributeGroup? relatedGroup,
      FileUrl fileUrl,
      string fileName,
      long? fileSizeBytes,
      string? uploadedBy,
      string? remarks)
  {
    ArgumentNullException.ThrowIfNull(propertyId);
    ArgumentNullException.ThrowIfNull(fileUrl);
    ValidateFile(fileName, fileSizeBytes);

    return new PropertyDocument
    {
      Id = propertyDocumentId,
      PropertyId = propertyId,
      DocumentType = documentType,
      RelatedGroup = relatedGroup,
      FileUrl = fileUrl,
      FileName = fileName.Trim(),
      FileSizeBytes = fileSizeBytes,
      UploadedOn = DateTime.UtcNow,
      UploadedBy = uploadedBy,
      Remarks = remarks
    };
  }

  public void Update(
      DocumentType documentType,
      AttributeGroup? relatedGroup,
      FileUrl fileUrl,
      string fileName,
      long? fileSizeBytes,
      string? remarks)
  {
    ArgumentNullException.ThrowIfNull(fileUrl);
    ValidateFile(fileName, fileSizeBytes);

    DocumentType = documentType;
    RelatedGroup = relatedGroup;
    FileUrl = fileUrl;
    FileName = fileName.Trim();
    FileSizeBytes = fileSizeBytes;
    Remarks = remarks;
  }

  private static void ValidateFile(string fileName, long? fileSizeBytes)
  {
    if (string.IsNullOrWhiteSpace(fileName))
      throw new DomainException("File name is required.");

    if (fileName.Length > 255)
      throw new DomainException("File name cannot exceed 255 characters.");

    if (fileSizeBytes.HasValue && fileSizeBytes.Value < 0)
      throw new DomainException("File size cannot be negative.");
  }
}
