using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class PropertyAttributeValueConfiguration : EntityConfiguration<PropertyAttributeValue, PropertyAttributeValueId>
{
  public override void Configure(EntityTypeBuilder<PropertyAttributeValue> builder)
  {
    base.Configure(builder);

    builder.HasKey(x => x.Id);

    builder.Property(x => x.Id).HasConversion(
      propertyAttributeValueId => propertyAttributeValueId.Value,
      dbId => PropertyAttributeValueId.Of(dbId)
    );

    builder.Property(x => x.PropertyId)
      .HasConversion(propertyId => propertyId.Value, dbId => PropertyId.Of(dbId))
      .IsRequired();

    builder.Property(x => x.AttributeDefinitionId)
      .HasConversion(attributeDefinitionId => attributeDefinitionId.Value, dbId => AttributeDefinitionId.Of(dbId))
      .IsRequired();

    builder.Property(x => x.DataType)
      .HasConversion(
        dataType => dataType.ToString(),
        dbValue => (AttributeDataType)Enum.Parse(typeof(AttributeDataType), dbValue))
      .HasMaxLength(30)
      .IsRequired();

    builder.Property(x => x.Value).HasMaxLength(4000).IsRequired(false);

    builder.HasIndex(x => new { x.PropertyId, x.AttributeDefinitionId }).IsUnique();

    builder.HasOne<Property>()
      .WithMany()
      .HasForeignKey(x => x.PropertyId)
      .IsRequired()
      .OnDelete(DeleteBehavior.Cascade);

    builder.HasOne<AttributeDefinition>()
      .WithMany()
      .HasForeignKey(x => x.AttributeDefinitionId)
      .IsRequired()
      .OnDelete(DeleteBehavior.Restrict);
  }
}
