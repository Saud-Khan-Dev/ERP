using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class PlantItemConfiguration : EntityConfiguration<PlantItem, PlantItemId>
{
    public override void Configure(EntityTypeBuilder<PlantItem> builder)
    {
        base.Configure(builder);
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).HasConversion(
          PlantItemId => PlantItemId.Value, dbId => PlantItemId.Of(dbId)
        );
        builder.Property(x => x.Code).HasConversion(code => code.Value, dbValue => Code.Of(dbValue)).IsRequired().HasMaxLength(50);

        builder.Property(x => x.Name)
        .HasConversion(name => name.Value, dbValue => Name.Of(dbValue)).HasMaxLength(100).IsRequired();

        builder.Property(x => x.Description)
        .IsRequired(false).HasMaxLength(1000);

        builder.Property(x => x.PlantId).HasConversion(
          plantId => plantId.Value, dbId => PlantId.Of(dbId)
        );

        builder.Property(x => x.ManufacturerId).HasConversion(
          manufacturerId => manufacturerId.Value, dbId => ManufacturerId.Of(dbId)
        );
        builder.Property(x => x.Model)
            .IsRequired();

        builder.Property(x => x.SerialNumber)
            .IsRequired();

        builder.Property(x => x.InstallationDateTime)
            .IsRequired();

        builder.Property(x => x.UsefulLifeYears)
            .HasPrecision(18, 2)
            .IsRequired();

        builder.ComplexProperty(
                x => x.Unit,
                unitBuilder =>
                {
                    unitBuilder
                        .Property(x => x.Value)
                        .IsRequired()
                        .HasMaxLength(100);
                });

        builder.Property(x => x.Capacity)
            .HasPrecision(18, 2)
            .IsRequired();

        builder.Property(x => x.WarrantyExpirationDate)
            .IsRequired();


        builder.Property(x => x.SupplierId).HasConversion(
           supplierId => supplierId.Value, dbId => PersonId.Of(dbId)
         );


        // Relationships
        builder.HasOne<Plant>()
            .WithMany()
            .HasForeignKey(x => x.PlantId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne<Manufacturer>()
            .WithMany()
            .HasForeignKey(x => x.ManufacturerId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne<Person>()
            .WithMany()
            .HasForeignKey(x => x.SupplierId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}