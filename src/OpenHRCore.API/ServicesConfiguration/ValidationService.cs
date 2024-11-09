using FluentValidation;
using System.Reflection;

namespace OpenHRCore.API.ServicesConfiguration
{
    public static class ValidationService
    {
        public static IServiceCollection AddValidatorsService(this IServiceCollection services)
        {
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            return services;
        }
    }
}
