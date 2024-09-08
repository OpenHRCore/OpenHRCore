using OpenHRCore.SharedKernel.Infrastructure;

namespace OpenHRCore.WorkForce.Infrastructure.Repositories
{
    public class JobGradeRepository : OpenHRCoreEfBaseRepository<JobGrade>, IJobGradeRepository
    {
        public JobGradeRepository(OpenHRCoreWorkForceDbContext dbContext) : base(dbContext)
        {
        }
    }
}
