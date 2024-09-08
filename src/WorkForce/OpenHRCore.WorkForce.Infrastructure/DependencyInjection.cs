﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OpenHRCore.SharedKernel.Domain;
using OpenHRCore.WorkForce.Application.UnitOfWork;
using OpenHRCore.WorkForce.Infrastructure.Repositories;
using OpenHRCore.WorkForce.Infrastructure.UnitOfWork;

namespace OpenHRCore.WorkForce.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddOpenHRCoreWorkForceDbContext(this IServiceCollection services, IConfiguration _configuration)
        {
            var connectionString = _configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<OpenHRCoreWorkForceDbContext>(options => options.UseSqlServer(connectionString));
            return services;
        }

        public static IServiceCollection AddOpenHRCoreWorkForceInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<IWorkForceUnitOfWork, WorkForceUnitOfWork>();
            services.AddScoped(typeof(IOpenHRCoreBaseRepository<>), typeof(OpenHRCoreEfBaseRepository<>));
            services.AddTransient<IJobGradeRepository,JobGradeRepository>();
            services.AddTransient<JobLevelRepository,JobLevelRepository>();
            services.AddTransient<JobPositionRepository,JobPositionRepository>();

            return services;
        }
    }
}
