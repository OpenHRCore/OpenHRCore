using OpenHRCore.SharedKernel.Infrastructure;

namespace OpenHRCore.WorkForce.Infrastructure.Repositories
{
    public class JobLevelRepository : OpenHRCoreEfBaseRepository<JobLevel>, IJobLevelRepository
    {
        public JobLevelRepository(OpenHRCoreWorkForceDbContext dbContext) : base(dbContext)
        {
        }
    }
}
