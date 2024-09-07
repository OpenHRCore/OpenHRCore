using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace OpenHRCore.WorkForce.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddOpenHRCoreWorkForceDbContext(this IServiceCollection services, IConfiguration _configuration)
        {
            var connectionString = _configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<OpenHRCoreWorkForceDbContext>(options => options.UseSqlServer(connectionString));
            return services;
        }
    }
}
