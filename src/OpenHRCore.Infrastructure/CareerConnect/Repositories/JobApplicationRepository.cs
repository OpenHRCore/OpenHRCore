namespace OpenHRCore.Infrastructure.CareerConnect.Repositories
{
    public class JobApplicationRepository : OpenHRCoreEfBaseRepository<JobApplication>, IJobApplicationRepository
    {
        private readonly OpenHRCoreDbContext _dbContext;
        private readonly ILogger<JobApplicationRepository> _logger;

        public JobApplicationRepository(OpenHRCoreDbContext dbContext, ILogger<JobApplicationRepository> logger) : base(dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _logger = logger ?? throw new ArgumentNullException(nameof(_logger));
        }
    }
}
