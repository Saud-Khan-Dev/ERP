public sealed record ManufacturerDto(
Guid Id,
string Name,
string? Description,
string? ContactNumber,
string? Email,
AddressDto? Address
);