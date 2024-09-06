namespace OpenHRCore.WorkForce.Infrastructure.Repositories
{
    public class JobLevelRepository : OpenHRCoreEfBaseRepository<OpenHRCoreWorkForceDbContext, JobLevel>, IJobLevelRepository
    {
        public JobLevelRepository(OpenHRCoreWorkForceDbContext dbContext) : base(dbContext)
        {
        }
    }
}
