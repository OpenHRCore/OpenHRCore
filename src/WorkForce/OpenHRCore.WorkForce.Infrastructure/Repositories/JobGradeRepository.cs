using OpenHRCore.SharedKernel.Infrastructure.Common;
using OpenHRCore.WorkForce.Domain.Entities;
using OpenHRCore.WorkForce.Domain.Interfaces;
using OpenHRCore.WorkForce.Infrastructure.Data;

namespace OpenHRCore.WorkForce.Infrastructure.Repositories
{
    public class JobGradeRepository : OpenHRCoreEfBaseRepository<OpenHRCoreWorkForceDbContext, JobGrade>, IJobGradeRepository
    {
        public JobGradeRepository(OpenHRCoreWorkForceDbContext dbContext) : base(dbContext)
        {
        }
    }
}
