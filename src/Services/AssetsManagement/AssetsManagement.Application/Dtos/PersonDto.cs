public sealed record PersonDto(
Guid Id,
string FirstName,
string LastName,
string? Email,
string? PhoneNumber,
AddressDto? Address
);