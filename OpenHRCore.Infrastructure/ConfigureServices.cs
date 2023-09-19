using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OpenHRCore.Infrastructure.Data;
using OpenHRCore.Infrastructure.Identity;

namespace OpenHRCore.Infrastructure
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddApplicationDbContext(this IServiceCollection services, IConfiguration _configuration)
        {
            var connectionString = _configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<OpenHRCoreDbContext>(options => options.UseSqlServer(connectionString));
            return services;
        }

        public static IServiceCollection AddIdentityServices(this IServiceCollection services, IConfiguration _configuration)
        {
            services.AddDefaultIdentity<ApplicationUser>()
                    .AddRoles<ApplicationRole>()
                    .AddEntityFrameworkStores<OpenHRCoreDbContext>();

            return services;
        }
       
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {

            return services;
        }
    }
}
