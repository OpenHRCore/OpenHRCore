using OpenHRCore.Domain.Workforce.Entities;
using OpenHRCore.Domain.Workforce.Interfaces;
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
