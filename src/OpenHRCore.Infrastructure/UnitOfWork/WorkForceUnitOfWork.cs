using OpenHRCore.Application.UnitOfWork;
using OpenHRCore.Infrastructure.Data;
using OpenHRCore.SharedKernel.Infrastructure;

namespace OpenHRCore.Infrastructure.UnitOfWork
{
    public class WorkForceUnitOfWork : OpenHRCoreEfUnitOfWork<OpenHRCoreDbContext>, IWorkForceUnitOfWork
    {
        public WorkForceUnitOfWork(OpenHRCoreDbContext dbContext) : base(dbContext)
        {
        }
    }
}
