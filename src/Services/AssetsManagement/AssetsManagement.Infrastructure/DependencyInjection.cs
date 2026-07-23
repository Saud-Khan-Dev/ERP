using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
  public static IServiceCollection AddInfrastructureService(this IServiceCollection services, IConfiguration configuration)
  {
    var connectionString = configuration.GetConnectionString("Database");
    services.AddDbContext<ApplicationDbContext>(opt =>
    {
      opt.UseSqlServer(connectionString);
    });

    services.AddScoped<IApplicationDbContext, ApplicationDbContext>();
    return services;
  }
}