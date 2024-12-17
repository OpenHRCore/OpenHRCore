namespace OpenHRCore.Infrastructure.CareerConnect.Repositories
{
    public class JobPostRepository : OpenHRCoreEfBaseRepository<JobPost>, IJobPostRepository
    {
        private readonly ILogger<JobPostRepository> _logger;
        private readonly OpenHRCoreDbContext _context;

        public JobPostRepository(
            OpenHRCoreDbContext dbContext,
            ILogger<JobPostRepository> logger) : base(dbContext)
        {
            _context = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
    }
}
