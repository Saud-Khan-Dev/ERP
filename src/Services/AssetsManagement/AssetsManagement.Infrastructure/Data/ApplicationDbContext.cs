using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : DbContext
{
  public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

  DbSet<InventoryCategory> InventoryCategories => Set<InventoryCategory>();
  DbSet<InventoryItem> InventoryItems => Set<InventoryItem>();
  DbSet<InventoryType> InventoryTypes => Set<InventoryType>();
  DbSet<InventoryStock> InventoryStocks => Set<InventoryStock>();
  DbSet<Purchase> Purchases => Set<Purchase>();
  DbSet<PurchaseLine> PurchaseLines => Set<PurchaseLine>();
  DbSet<Warehouse> Warehouses => Set<Warehouse>();

}