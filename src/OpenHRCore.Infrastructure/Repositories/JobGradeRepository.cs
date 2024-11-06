using OpenHRCore.Domain.EmployeeModule.Entities;
using OpenHRCore.Domain.EmployeeModule.Interfaces;
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
