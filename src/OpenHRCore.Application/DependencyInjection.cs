using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using OpenHRCore.Application.Interfaces;
using OpenHRCore.Application.Mapping;
using OpenHRCore.Application.Services;
using System.Reflection;

namespace OpenHRCore.Application
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
