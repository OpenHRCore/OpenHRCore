using OpenHRCore.Application.UnitOfWork;

namespace OpenHRCore.Infrastructure.UnitOfWork
{
    public class OpenHRCoreUnitOfWork : OpenHRCoreEfBaseUnitOfWork<OpenHRCoreDbContext>, IOpenHRCoreUnitOfWork
    {
        public OpenHRCoreUnitOfWork(OpenHRCoreDbContext dbContext) : base(dbContext)
        {
        }
    }
}
