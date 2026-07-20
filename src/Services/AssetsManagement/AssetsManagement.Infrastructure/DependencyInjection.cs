using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
  public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
  {
    var connectionString = configuration.GetConnectionString("Database");
    services.AddDbContext<ApplicationDbContext>(opt =>
    {
      opt.UseSqlServer(connectionString);
    });
    return services;
  }
}