using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OpenHRCore.Application.UnitOfWork;
using OpenHRCore.Infrastructure.Data;
using OpenHRCore.Infrastructure.Repositories;
using OpenHRCore.Infrastructure.UnitOfWork;
using OpenHRCore.SharedKernel.Domain;

namespace OpenHRCore.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddOpenHRCoreWorkForceDbContext(this IServiceCollection services, IConfiguration _configuration)
        {
            var connectionString = _configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<OpenHRCoreDbContext>(options => options.UseSqlServer(connectionString));
            return services;
        }

        public static IServiceCollection AddOpenHRCoreWorkForceInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<IWorkForceUnitOfWork, WorkForceUnitOfWork>();
            services.AddScoped(typeof(IOpenHRCoreBaseRepository<>), typeof(OpenHRCoreEfBaseRepository<>));
            services.AddTransient<IJobGradeRepository, JobGradeRepository>();
            services.AddTransient<JobLevelRepository, JobLevelRepository>();
            services.AddTransient<JobPositionRepository, JobPositionRepository>();

            return services;
        }
    }
}
