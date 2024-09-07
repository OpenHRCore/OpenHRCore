using OpenHRCore.WorkForce.Application.UnitOfWork;

namespace OpenHRCore.WorkForce.Infrastructure.UnitOfWork
{
    public class WorkForceUnitOfWork : OpenHRCoreEfUnitOfWork<OpenHRCoreWorkForceDbContext>, IWorkForceUnitOfWork
    {
        public WorkForceUnitOfWork(OpenHRCoreWorkForceDbContext dbContext) : base(dbContext)
        {
        }
    }
}
