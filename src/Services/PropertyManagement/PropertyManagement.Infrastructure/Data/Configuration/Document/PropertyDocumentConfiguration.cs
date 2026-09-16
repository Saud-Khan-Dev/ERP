using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class PropertyDocumentConfiguration : EntityConfiguration<PropertyDocument, PropertyDocumentId>
{
  public override void Configure(EntityTypeBuilder<PropertyDocument> builder)
  {
    base.Configure(builder);

    builder.HasKey(x => x.Id);

    builder.Property(x => x.Id).HasConversion(
      propertyDocumentId => propertyDocumentId.Value,
      dbId => PropertyDocumentId.Of(dbId)
    );

    builder.Property(x => x.PropertyId)
      .HasConversion(propertyId => propertyId.Value, dbId => PropertyId.Of(dbId))
      .IsRequired();

    builder.Property(x => x.DocumentType)
      .HasConversion(
        documentType => documentType.ToString(),
        dbValue => (DocumentType)Enum.Parse(typeof(DocumentType), dbValue))
      .HasMaxLength(50)
      .IsRequired();

    builder.Property(x => x.RelatedGroup)
      .HasConversion<string>()
      .HasMaxLength(50)
      .IsRequired(false);

    builder.Property(x => x.FileUrl)
      .HasConversion(fileUrl => fileUrl.Value, dbValue => FileUrl.Of(dbValue))
      .IsRequired()
      .HasMaxLength(1000);

    builder.Property(x => x.FileName).HasMaxLength(255).IsRequired();
    builder.Property(x => x.FileSizeBytes).IsRequired(false);
    builder.Property(x => x.UploadedOn).IsRequired();
    builder.Property(x => x.UploadedBy).HasMaxLength(100).IsRequired(false);
    builder.Property(x => x.Remarks).HasMaxLength(1000).IsRequired(false);

    builder.HasIndex(x => x.PropertyId);

    builder.HasOne<Property>()
      .WithMany()
      .HasForeignKey(x => x.PropertyId)
      .IsRequired()
      .OnDelete(DeleteBehavior.Cascade);
  }
}
