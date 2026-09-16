using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public sealed class ManufacturerConfiguration
    : EntityConfiguration<Manufacturer, ManufacturerId>
{
  public override void Configure(EntityTypeBuilder<Manufacturer> builder)
  {
        base.Configure(builder);

        builder.Property(x => x.Id).HasConversion(
               personId => personId.Value, dbId => ManufacturerId.Of(dbId)
             );


                builder.Property(x => x.Name)
    .HasConversion(name => name.Value, dbValue => Name.Of(dbValue)).HasMaxLength(100).IsRequired();

    builder.Property(x => x.Description)
    .IsRequired(false).HasMaxLength(1000);

    builder.Property(x => x.ContactNumber)
        .HasMaxLength(50);

    builder.Property(x => x.Email)
        .HasConversion(
            email => email == null ? null : email.Value,
            value => value == null ? null : Email.Of(value))
        .HasMaxLength(255);

    builder.ComplexProperty(x => x.Address, address =>
    {
      address.Property(x => x.Street)
              .HasMaxLength(250)
              .IsRequired();

      address.Property(x => x.Building)
              .HasMaxLength(100);

      address.Property(x => x.City)
              .HasMaxLength(100)
              .IsRequired();

      address.Property(x => x.State)
              .HasMaxLength(100);

      address.Property(x => x.PostalCode)
              .HasMaxLength(20)
              .IsRequired();

      address.Property(x => x.Country)
              .HasMaxLength(100)
              .IsRequired();

      address.Property(x => x.Longitude)
              .HasPrecision(10, 7);

      address.Property(x => x.Latitude)
              .HasPrecision(10, 7);
    });

    builder.HasIndex(x => x.Name);
  }
}