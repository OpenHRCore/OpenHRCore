namespace OpenHRCore.Infrastructure.CareerConnect.Repositories
{
    public class InterviewRepository : OpenHRCoreEfBaseRepository<Interview> , IInterviewRepository
    {
        private readonly ILogger<InterviewRepository> _logger;
        private readonly OpenHRCoreDbContext _dbContext;

        public InterviewRepository(OpenHRCoreDbContext dbContext, ILogger<InterviewRepository> logger) :base(dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _logger = logger ?? throw new ArgumentNullException(nameof(_logger));
        }
    }
}
