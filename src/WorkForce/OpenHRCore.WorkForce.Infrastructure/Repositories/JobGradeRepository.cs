namespace OpenHRCore.WorkForce.Infrastructure.Repositories
{
    public class JobGradeRepository : OpenHRCoreEfBaseRepository<OpenHRCoreWorkForceDbContext, JobGrade>, IJobGradeRepository
    {
        public JobGradeRepository(OpenHRCoreWorkForceDbContext dbContext) : base(dbContext)
        {
        }
    }
}
