using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
  public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
  {
    var connectionString = configuration.GetConnectionString("Database");

    services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptors>();
    services.AddScoped<ISaveChangesInterceptor, DispatchDomainEventInterceptor>();

    services.AddDbContext<ApplicationDbContext>((sp, opt) =>
    {
      opt.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>()!);
      opt.UseNpgsql(connectionString);
    });

    services.AddScoped<IApplicationDbContext, ApplicationDbContext>();

    return services;
  }
}
