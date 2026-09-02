public sealed record PlantItemDto(
Guid Id,
string Code,
string Name,
string Description,
Guid PlantId,
Guid ManufacturerId,
string Model,
string SerialNumber,
DateTime InstallationDateTime,
decimal UsefulLifeYears,
string Unit,
decimal Capacity,
DateTime WarrantyExpirationDate,
Guid SupplierId
);