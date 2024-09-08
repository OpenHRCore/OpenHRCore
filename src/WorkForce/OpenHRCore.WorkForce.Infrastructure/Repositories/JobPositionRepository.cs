using OpenHRCore.SharedKernel.Infrastructure;

namespace OpenHRCore.WorkForce.Infrastructure.Repositories
{
    public class JobPositionRepository : OpenHRCoreEfBaseRepository<OpenHRCoreWorkForceDbContext, JobPosition>, IJobPositionRepository
    {
        public JobPositionRepository(OpenHRCoreWorkForceDbContext dbContext) : base(dbContext)
        {
        }
    }
}
