namespace OpenHRCore.Infrastructure.CareerConnect.Repositories
{
    public class ResumeRepository : OpenHRCoreEfBaseRepository<Resume>, IResumeRepository
    {
        private readonly ILogger<ResumeRepository> _logger;
        private readonly OpenHRCoreDbContext _dbContext;

        public ResumeRepository(OpenHRCoreDbContext dbContext, ILogger<ResumeRepository> logger) : base(dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _logger = logger ?? throw new ArgumentNullException(nameof(_logger));
        }
    }
}
