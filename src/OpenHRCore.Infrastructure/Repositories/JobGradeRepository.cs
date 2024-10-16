using OpenHRCore.Domain.Workforce.Entities;
using OpenHRCore.Domain.Workforce.Interfaces;
using OpenHRCore.Infrastructure.Data;
using OpenHRCore.SharedKernel.Infrastructure;

namespace OpenHRCore.Infrastructure.Repositories
{
    public class JobGradeRepository : OpenHRCoreEfBaseRepository<JobGrade>, IJobGradeRepository
    {
        public JobGradeRepository(OpenHRCoreDbContext dbContext) : base(dbContext)
        {
        }
    }
}
