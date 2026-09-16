using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class AttributeDefinitionConfiguration : EntityConfiguration<AttributeDefinition, AttributeDefinitionId>
{
  public override void Configure(EntityTypeBuilder<AttributeDefinition> builder)
  {
    base.Configure(builder);

    builder.HasKey(x => x.Id);

    builder.Property(x => x.Id).HasConversion(
      attributeDefinitionId => attributeDefinitionId.Value,
      dbId => AttributeDefinitionId.Of(dbId)
    );

    builder.Property(x => x.Group)
      .HasConversion(
        group => group.ToString(),
        dbValue => (AttributeGroup)Enum.Parse(typeof(AttributeGroup), dbValue))
      .HasMaxLength(50)
      .IsRequired();

    builder.Property(x => x.Code)
      .HasConversion(code => code.Value, dbValue => Code.Of(dbValue))
      .IsRequired()
      .HasMaxLength(50);

    builder.Property(x => x.Label)
      .HasConversion(label => label.Value, dbValue => Name.Of(dbValue))
      .IsRequired()
      .HasMaxLength(100);

    builder.Property(x => x.DataType)
      .HasConversion(
        dataType => dataType.ToString(),
        dbValue => (AttributeDataType)Enum.Parse(typeof(AttributeDataType), dbValue))
      .HasMaxLength(30)
      .IsRequired();

    builder.Property(x => x.IsRequired).IsRequired();
    builder.Property(x => x.OptionsCsv).HasMaxLength(2000).IsRequired(false);
    builder.Property(x => x.DefaultValue).HasMaxLength(4000).IsRequired(false);
    builder.Property(x => x.DisplayOrder).IsRequired();
    builder.Property(x => x.IsActive).IsRequired();

    builder.Ignore(x => x.Options);

    builder.HasIndex(x => new { x.Group, x.Code }).IsUnique();
  }
}
