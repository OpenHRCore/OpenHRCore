using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OpenHRCore.Application.UnitOfWork;
using OpenHRCore.Infrastructure.CareerConnect.Repositories;
using OpenHRCore.Infrastructure.UnitOfWork;
using OpenHRCore.SharedKernel.Domain;

namespace OpenHRCore.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddOpenHRCoreDbContext(this IServiceCollection services, IConfiguration _configuration, ILogger _logger)
        {
            var connectionString = _configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<OpenHRCoreDbContext>(options => options.UseNpgsql(connectionString));

            return services;
        }

        public static IServiceCollection AddOpenHRCoreInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<IOpenHRCoreUnitOfWork, OpenHRCoreUnitOfWork>();
            services.AddScoped(typeof(IOpenHRCoreBaseRepository<>), typeof(OpenHRCoreEfBaseRepository<>));
            services.AddScoped<IOrganizationUnitRepository, OrganizationUnitRepository>();
            services.AddScoped<IJobLevelRepository, JobLevelRepository>();
            services.AddScoped<IJobGradeRepository, JobGradeRepository>();
            services.AddScoped<IJobPositionRepository, JobPositionRepository>();
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();

            services.AddScoped<IApplicantRepository, ApplicantRepository>();
            //services.AddScoped<IApplicantStageRepository, ApplicantStageRepository>();
            services.AddScoped<ICoverLetterRepository, CoverLetterRepository>();
            services.AddScoped<IInterviewRepository, InterviewRepository>();
            services.AddScoped<IJobApplicationRepository, JobApplicationRepository>();
            services.AddScoped<IJobOfferRepository, JobOfferRepository>();
            services.AddScoped<IJobPostRepository, JobPostRepository>();
            services.AddScoped<IResumeRepository, ResumeRepository>();

            return services;
        }
    }
}
