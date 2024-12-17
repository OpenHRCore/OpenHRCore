namespace OpenHRCore.Infrastructure.CareerConnect.Repositories
{
    public class JobOfferRepository : OpenHRCoreEfBaseRepository<JobOffer>, IJobOfferRepository
    {
        private readonly OpenHRCoreDbContext _dbContext;
        private readonly ILogger<JobOfferRepository> _logger;

        public JobOfferRepository(OpenHRCoreDbContext dbContext, ILogger<JobOfferRepository> logger) : base(dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _logger = logger ?? throw new ArgumentNullException(nameof(_logger));
        }
    }
}
