using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using OpenHRCore.Application.Workforce.DTOs.OU;
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
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddScoped<IOrganizationUnitService, OrganizationUnitService>();

            return services;
        }
    }
}
