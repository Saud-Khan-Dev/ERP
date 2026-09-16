using Microsoft.EntityFrameworkCore;

public interface IApplicationDbContext
{
  DbSet<Property> Properties { get; }
  DbSet<GeoLocation> GeoLocations { get; }
  DbSet<AttributeDefinition> AttributeDefinitions { get; }
  DbSet<PropertyAttributeValue> PropertyAttributeValues { get; }
  DbSet<PropertyDocument> PropertyDocuments { get; }

  Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
