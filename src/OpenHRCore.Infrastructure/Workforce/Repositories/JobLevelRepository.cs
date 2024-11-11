
using Microsoft.Extensions.Logging;

namespace OpenHRCore.Infrastructure.Workforce.Repositories
{
    public class JobLevelRepository : OpenHRCoreEfBaseRepository<JobLevel>, IJobLevelRepository
    {
        private readonly ILogger<JobLevelRepository> _logger;
        private readonly OpenHRCoreDbContext _context;

        public JobLevelRepository(OpenHRCoreDbContext context, ILogger<JobLevelRepository> logger) : base(context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
    }
}
