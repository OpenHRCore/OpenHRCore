
using Microsoft.Extensions.Logging;

namespace OpenHRCore.Infrastructure.Workforce.Repositories
{
    public class JobGradeRepository : OpenHRCoreEfBaseRepository<JobGrade>, IJobGradeRepository
    {
        private readonly ILogger<JobGradeRepository> _logger;
        private readonly OpenHRCoreDbContext _context;


        public JobGradeRepository(OpenHRCoreDbContext context, ILogger<JobGradeRepository> logger) : base(context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
    }
}
