using FluentValidation;
using OpenHRCore.Application.DTOs.JobGrade;

namespace OpenHRCore.API.ServicesConfiguration
{
    public static class ValidationService
    {
        public static IServiceCollection AddValidatorsService(this IServiceCollection services)
        {
            services.AddValidatorsFromAssemblyContaining<CreateJobGradeRequestValidator>();
            services.AddValidatorsFromAssemblyContaining<UpdateJobGradeRequestValidator>();
            services.AddValidatorsFromAssemblyContaining<DeleteJobGradeRequestValidator>();

            return services;
        }
    }
}
