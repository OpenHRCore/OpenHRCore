using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using OpenHRCore.Application.Workforce.DTOs.JobGradeDtos;
using OpenHRCore.Application.Workforce.DTOs.JobLevelDtos;
using OpenHRCore.Application.Workforce.DTOs.JobPositionDtos;
using OpenHRCore.Application.Workforce.DTOs.OUDtos;
using OpenHRCore.Application.Workforce.Interfaces;
using OpenHRCore.Application.Workforce.Services;
using System.Reflection;

namespace OpenHRCore.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddOpenHRCoreApplication(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(OrganizationUnitMappingProfile));
            services.AddAutoMapper(typeof(JobLevelMappingProfile));
            services.AddAutoMapper(typeof(JobGradeMappingProfile));
            services.AddAutoMapper(typeof(JobPositionMappingProfile));

            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddScoped<IOrganizationUnitService, OrganizationUnitService>();
            services.AddScoped<IJobPositionService, JobPositionService>();
            services.AddScoped<IJobGradeService, JobGradeService>();
            services.AddScoped<IJobLevelService, JobLevelService>();

            return services;
        }
    }
}
