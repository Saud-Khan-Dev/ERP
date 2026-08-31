using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class ScrapConfigurations : EntityConfiguration<Scrap, ScrapId>
{
  public override void Configure(EntityTypeBuilder<Scrap> builder)
  {
    base.Configure(builder);
    builder.HasKey(x => x.Id);
    builder.Property(x => x.Id)
    .HasConversion(scrapId => scrapId.Value, dbValue => ScrapId.Of(dbValue));

    builder.HasKey(x => x.InventoryItemId);
    builder.Property(x => x.InventoryItemId)
    .HasConversion(inventoryItemId => inventoryItemId.Value, dbValue => InventoryItemId.Of(dbValue));

    builder.ComplexProperty(x => x.Price, p =>
    {
      p.Property(x => x.Amount)
     .HasPrecision(18, 2)
       .IsRequired();
      p.Property(x => x.Currency)
      .HasConversion(currency => currency.Value, dbValue => Currency.Of(dbValue));
    });


    builder.Property(x => x.Total)
     .HasPrecision(18, 2)
       .IsRequired();

  }
}