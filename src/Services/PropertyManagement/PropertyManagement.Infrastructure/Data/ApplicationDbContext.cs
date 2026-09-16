using System.Reflection;
using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{
  public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

  public DbSet<Property> Properties => Set<Property>();

  public DbSet<GeoLocation> GeoLocations => Set<GeoLocation>();

  public DbSet<AttributeDefinition> AttributeDefinitions => Set<AttributeDefinition>();

  public DbSet<PropertyAttributeValue> PropertyAttributeValues => Set<PropertyAttributeValue>();

  public DbSet<PropertyDocument> PropertyDocuments => Set<PropertyDocument>();

  protected override void OnModelCreating(ModelBuilder builder)
  {
    builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    base.OnModelCreating(builder);
  }
}
