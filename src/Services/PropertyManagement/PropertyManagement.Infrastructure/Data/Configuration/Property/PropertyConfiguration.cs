using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class PropertyConfiguration : EntityConfiguration<Property, PropertyId>
{
  public override void Configure(EntityTypeBuilder<Property> builder)
  {
    base.Configure(builder);

    builder.HasKey(x => x.Id);

    builder.Property(x => x.Id).HasConversion(
      propertyId => propertyId.Value,
      dbId => PropertyId.Of(dbId)
    );

    builder.Property(x => x.PropertyCode)
      .HasConversion(code => code.Value, dbValue => Code.Of(dbValue))
      .IsRequired()
      .HasMaxLength(50);

    builder.Property(x => x.PropertyName)
      .HasConversion(name => name.Value, dbValue => Name.Of(dbValue))
      .IsRequired()
      .HasMaxLength(100);

    builder.Property(x => x.PropertyType)
      .HasConversion(
        propertyType => propertyType.ToString(),
        dbValue => (PropertyType)Enum.Parse(typeof(PropertyType), dbValue))
      .HasMaxLength(30)
      .IsRequired();

    builder.Property(x => x.OwnershipType)
      .HasConversion(
        ownershipType => ownershipType.ToString(),
        dbValue => (OwnershipType)Enum.Parse(typeof(OwnershipType), dbValue))
      .HasMaxLength(30)
      .IsRequired();

    builder.Property(x => x.Status)
      .HasConversion(
        status => status.ToString(),
        dbValue => (PropertyStatus)Enum.Parse(typeof(PropertyStatus), dbValue))
      .HasMaxLength(30)
      .IsRequired();

    builder.Property(x => x.GeoLocationId)
      .HasConversion(
        geoLocationId => geoLocationId.Value,
        dbId => GeoLocationId.Of(dbId))
      .IsRequired();

    builder.Property(x => x.AreaSqFt).HasPrecision(18, 2).IsRequired();
    builder.Property(x => x.CoveredAreaSqFt).HasPrecision(18, 2).IsRequired();
    builder.Property(x => x.LandAreaSqFt).HasPrecision(18, 2).IsRequired();

    builder.HasIndex(x => x.PropertyCode).IsUnique();
    builder.HasIndex(x => x.GeoLocationId).IsUnique();

    builder.HasOne<GeoLocation>()
      .WithMany()
      .HasForeignKey(x => x.GeoLocationId)
      .IsRequired()
      .OnDelete(DeleteBehavior.Restrict);
  }
}
