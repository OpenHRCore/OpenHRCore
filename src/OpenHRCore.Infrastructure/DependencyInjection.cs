using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OpenHRCore.Application.UnitOfWork;
using OpenHRCore.Domain.EmployeeModule.Interfaces;
using OpenHRCore.Infrastructure.Repositories;
using OpenHRCore.Infrastructure.UnitOfWork;
using OpenHRCore.SharedKernel.Domain;

namespace OpenHRCore.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddOpenHRCoreDbContext(this IServiceCollection services, IConfiguration _configuration)
        {
            var connectionString = _configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<OpenHRCoreDbContext>(options => options.UseSqlServer(connectionString));

            return services;
        }

        public static IServiceCollection AddOpenHRCoreInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<IOpenHRCoreUnitOfWork, OpenHRCoreUnitOfWork>();
            services.AddScoped(typeof(IOpenHRCoreBaseRepository<>), typeof(OpenHRCoreEfBaseRepository<>));
            services.AddTransient<IJobGradeRepository, JobGradeRepository>();
            services.AddTransient<IJobLevelRepository, JobLevelRepository>();
            services.AddTransient<IJobPositionRepository, JobPositionRepository>();

            return services;
        }
    }
}
