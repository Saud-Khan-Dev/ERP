using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class PurchaseLineConfiguration : EntityConfiguration<PurchaseLine, PurchaseLineId>
{
  public override void Configure(EntityTypeBuilder<PurchaseLine> builder)
  {
    base.Configure(builder);

    builder.HasKey(x => x.Id);
    builder.Property(x => x.Id)
    .HasConversion(purchaseLineId => purchaseLineId.Value, dbValue => PurchaseLineId.Of(dbValue));

    builder.Property(x => x.OrderedQuantity)
    .HasPrecision(18, 4)
    .IsRequired();
    builder.Property(x => x.ReceivedQuantity)
    .HasPrecision(18, 4)
    .IsRequired();


    builder.ComplexProperty(x => x.Currency, currency =>
           {
             currency.Property(c => c.Value)
              .HasMaxLength(3)
              .IsRequired();
           });

    builder.ComplexProperty(x => x.UnitPrice, money =>
{
  money.Property(m => m.Amount)
       .HasPrecision(18, 2)
       .IsRequired();

  money.Property(m => m.Currency)
       .HasMaxLength(3)
       .IsRequired();
});
    builder.ComplexProperty(x => x.DiscountAmount, money =>
{
  money.Property(m => m.Amount)
       .HasPrecision(18, 2)
       .IsRequired();

  money.Property(m => m.Currency)
       .HasMaxLength(3)
       .IsRequired();
});

    builder.ComplexProperty(x => x.TaxAmount, money =>
   {
     money.Property(m => m.Amount)
          .HasPrecision(18, 2)
          .IsRequired();

     money.Property(m => m.Currency)
          .HasMaxLength(3)
          .IsRequired();
   });


    builder.ComplexProperty(x => x.UnitOfMeasure, uomBuilder =>
    {
      uomBuilder.Property(x => x.Unit).HasMaxLength(10).IsRequired();
      uomBuilder.Property(x => x.Value).IsRequired().HasPrecision(18, 4);
    });
    builder.ComplexProperty(x => x.LineTotal, money =>
{
  money.Property(m => m.Amount)
      .HasPrecision(18, 2)
      .IsRequired();

  money.Property(m => m.Currency)
      .HasMaxLength(3)
      .IsRequired();
});

    builder.Property(x => x.Remarks)
        .IsRequired(false)
        .HasMaxLength(2000);
  }
}