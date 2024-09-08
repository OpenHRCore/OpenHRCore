using Microsoft.Extensions.DependencyInjection;
using OpenHRCore.WorkForce.Application.Interfaces;
using OpenHRCore.WorkForce.Application.Mapping;
using OpenHRCore.WorkForce.Application.Services;

namespace OpenHRCore.WorkForce.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddOpenHRCoreWorkForceApplication(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(WorkForceMappingProfile));
            services.AddScoped<IJobPositionService, JobPositionService>();
            return services;
        }
    }
}
