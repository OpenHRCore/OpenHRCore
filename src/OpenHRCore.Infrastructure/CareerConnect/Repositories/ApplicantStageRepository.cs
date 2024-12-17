namespace OpenHRCore.Infrastructure.CareerConnect.Repositories
{
    public class ApplicantStageRepository : OpenHRCoreEfBaseRepository<ApplicantStage>, IApplicantStageRepository
    {
        private readonly ILogger<ApplicantStageRepository> _logger;
        private readonly OpenHRCoreDbContext _dbContext;

        public ApplicantStageRepository(
            OpenHRCoreDbContext dbContext,
            ILogger<ApplicantStageRepository> logger) : base(dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

    }
}
