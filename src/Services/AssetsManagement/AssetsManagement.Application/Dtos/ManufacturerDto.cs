public sealed record Manufacturer(
Guid Id,
string Name,
string? Description,
string? ContactNumber,
string? Email,
AddressDto? Address
);