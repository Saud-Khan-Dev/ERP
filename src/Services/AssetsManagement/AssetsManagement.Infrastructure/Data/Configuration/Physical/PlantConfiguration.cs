using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class PlantConfiguration : EntityConfiguration<Plant, PlantId>
{
  public override void Configure(EntityTypeBuilder<Plant> builder)
  {
    base.Configure(builder);
    base.Configure(builder);
    builder.HasKey(x => x.Id);

    builder.Property(x => x.Id).HasConversion(
      plantId => plantId.Value, dbId => PlantId.Of(dbId)
    );
    builder.Property(x => x.Id).HasConversion(
      plant => plant.Value, dbId => PlantId.Of(dbId)
    );

  }
}