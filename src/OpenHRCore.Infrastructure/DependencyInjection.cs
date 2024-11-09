using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OpenHRCore.Application.UnitOfWork;
using OpenHRCore.Infrastructure.UnitOfWork;
using OpenHRCore.SharedKernel.Domain;
using OpenHRCore.SharedKernel.Utilities;

namespace OpenHRCore.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddOpenHRCoreDbContext(this IServiceCollection services, IConfiguration _configuration, ILogger _logger)
        {
            var connectionString = _configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<OpenHRCoreDbContext>(options => options.UseNpgsql(connectionString));
                        //.LogTo(message => _logger.LogLayerInfo(message), LogLevel.Information)
                        //.EnableSensitiveDataLogging()
                        //.EnableDetailedErrors());

            return services;
        }

        public static IServiceCollection AddOpenHRCoreInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<IOpenHRCoreUnitOfWork, OpenHRCoreUnitOfWork>();
            services.AddScoped(typeof(IOpenHRCoreBaseRepository<>), typeof(OpenHRCoreEfBaseRepository<>));
            services.AddScoped<IOrganizationUnitRepository, OrganizationUnitRepository>();

            return services;
        }
    }
}
