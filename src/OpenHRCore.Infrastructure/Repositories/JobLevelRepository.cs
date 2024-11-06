using OpenHRCore.Domain.EmployeeModule.Entities;
using OpenHRCore.Domain.EmployeeModule.Interfaces;
using OpenHRCore.Infrastructure.Data;
using OpenHRCore.SharedKernel.Infrastructure;

namespace OpenHRCore.Infrastructure.Repositories
{
    public class JobLevelRepository : OpenHRCoreEfBaseRepository<JobLevel>, IJobLevelRepository
    {
        public JobLevelRepository(OpenHRCoreDbContext dbContext) : base(dbContext)
        {
        }
    }
}
