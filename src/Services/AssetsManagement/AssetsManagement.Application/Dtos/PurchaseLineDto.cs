public record PurchaseLineDto(
    Guid Id,
    Guid ItemId,
    Guid PurchaseId,
    decimal OrderedQuantity,
    decimal ReceivedQuantity,
    UnitOfMeasureDto UnitOfMeasure,
    string Currency,
    MoneyDto UnitPrice,
    decimal DiscountAmount,
    decimal TaxAmount,
    string? Remarks
);



public sealed record MoneyDto(
    decimal Amount,
    CurrencyDto Currency
);