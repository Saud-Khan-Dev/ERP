public sealed record PlantDto(
  Guid Id,
  Guid PhysicalId,
  IReadOnlyList<PlantItemDto> PlantItems
  );