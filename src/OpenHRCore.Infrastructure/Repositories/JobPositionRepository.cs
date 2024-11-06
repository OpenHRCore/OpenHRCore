using OpenHRCore.Domain.EmployeeModule.Entities;
using OpenHRCore.Domain.EmployeeModule.Interfaces;
using OpenHRCore.Infrastructure.Data;
using OpenHRCore.SharedKernel.Infrastructure;

namespace OpenHRCore.Infrastructure.Repositories
{
    public class JobPositionRepository : OpenHRCoreEfBaseRepository<JobPosition>, IJobPositionRepository
    {
        public JobPositionRepository(OpenHRCoreDbContext dbContext) : base(dbContext)
        {
        }
    }
}
