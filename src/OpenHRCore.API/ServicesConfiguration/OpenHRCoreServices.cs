
namespace OpenHRCore.API.ServicesConfiguration
{
    public static class OpenHRCoreServices
    {
        public static IServiceCollection AddOpenHRCoreServices(this IServiceCollection services, IConfiguration configuration,ILogger _logger)
        {
            services.AddOpenHRCoreDbContext(configuration, _logger);
            services.AddOpenHRCoreInfrastructure();
            services.AddOpenHRCoreApplication();

            return services;
        }
    }
}
