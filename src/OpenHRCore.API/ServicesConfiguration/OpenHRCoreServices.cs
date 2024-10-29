
namespace OpenHRCore.API.ServicesConfiguration
{
    public static class OpenHRCoreServices
    {
        public static IServiceCollection AddOpenHRCoreServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddOpenHRCoreDbContext(configuration);
            services.AddOpenHRCoreInfrastructure();
            services.AddOpenHRCoreApplication();

            return services;
        }
    }
}
