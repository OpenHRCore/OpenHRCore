namespace OpenHRCore.Infrastructure.CareerConnect.Repositories
{
    public class ApplicationStageRepository : OpenHRCoreEfBaseRepository<ApplicationStage>, IApplicationStageRepository
    {
        private readonly ILogger<ApplicationStageRepository> _logger;
        private readonly OpenHRCoreDbContext _dbContext;

        public ApplicationStageRepository(
            OpenHRCoreDbContext dbContext,
            ILogger<ApplicationStageRepository> logger) : base(dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

    }
}
