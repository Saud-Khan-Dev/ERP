using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class GeoLocationConfiguration : EntityConfiguration<GeoLocation, GeoLocationId>
{
  public override void Configure(EntityTypeBuilder<GeoLocation> builder)
  {
    base.Configure(builder);

    builder.HasKey(x => x.Id);

    builder.Property(x => x.Id).HasConversion(
      geoLocationId => geoLocationId.Value,
      dbId => GeoLocationId.Of(dbId)
    );

    builder.Property(x => x.Code)
      .HasConversion(code => code.Value, dbValue => Code.Of(dbValue))
      .IsRequired()
      .HasMaxLength(50);

    builder.Property(x => x.Latitude).HasPrecision(9, 6).IsRequired();
    builder.Property(x => x.Longitude).HasPrecision(9, 6).IsRequired();
    builder.Property(x => x.Elevation).HasPrecision(18, 2).IsRequired(false);
    builder.Property(x => x.GeoFenceRadius).HasPrecision(18, 2).IsRequired(false);

    builder.Property(x => x.CoordinateSystem).HasMaxLength(50).IsRequired();

    builder.HasIndex(x => x.Code).IsUnique();
  }
}
