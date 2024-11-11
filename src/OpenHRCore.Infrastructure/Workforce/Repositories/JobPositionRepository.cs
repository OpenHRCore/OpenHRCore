
using Microsoft.Extensions.Logging;

namespace OpenHRCore.Infrastructure.Workforce.Repositories
{
    public class JobPositionRepository : OpenHRCoreEfBaseRepository<JobPosition>, IJobPositionRepository
    {
        private readonly ILogger<JobPositionRepository> _logger;
        private readonly OpenHRCoreDbContext _context;

        public JobPositionRepository(OpenHRCoreDbContext context, ILogger<JobPositionRepository> logger) : base(context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
    }
}
