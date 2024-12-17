namespace OpenHRCore.Infrastructure.CareerConnect.Repositories
{
    public class CoverLetterRepository : OpenHRCoreEfBaseRepository<CoverLetter> , ICoverLetterRepository
    {
        private readonly ILogger<CoverLetterRepository> _logger;
        private readonly OpenHRCoreDbContext _dbContext;

        public CoverLetterRepository(
            OpenHRCoreDbContext dbContext,
            ILogger<CoverLetterRepository> logger) : base(dbContext)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }
    }
}
