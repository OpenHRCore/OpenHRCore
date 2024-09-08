using OpenHRCore.SharedKernel.Infrastructure;

namespace OpenHRCore.WorkForce.Infrastructure.Repositories
{
    public class JobPositionRepository : OpenHRCoreEfBaseRepository<JobPosition>, IJobPositionRepository
    {
        public JobPositionRepository(OpenHRCoreWorkForceDbContext dbContext) : base(dbContext)
        {
        }
    }
}
