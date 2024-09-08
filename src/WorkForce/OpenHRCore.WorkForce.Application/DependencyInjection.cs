using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using OpenHRCore.WorkForce.Application.Interfaces;
using OpenHRCore.WorkForce.Application.Mapping;
using OpenHRCore.WorkForce.Application.Services;
using System.Reflection;

namespace OpenHRCore.WorkForce.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddOpenHRCoreWorkForceApplication(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(WorkForceMappingProfile));
            //services.AddValidatorsFromAssemblyContaining<CreateJobGradeRequestValidator>();
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddScoped<IJobPositionService, JobPositionService>();
            return services;
        }
    }
}
