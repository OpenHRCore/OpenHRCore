namespace OpenHRCore.Infrastructure.CareerConnect.Repositories
{
    public class ApplicantRepository : OpenHRCoreEfBaseRepository<Applicant>, IApplicantRepository
    {
        private readonly ILogger<ApplicantRepository> _logger;
        private readonly OpenHRCoreDbContext _context;

        public ApplicantRepository(
            OpenHRCoreDbContext dbContext,
            ILogger<ApplicantRepository> logger) : base(dbContext)
        {
            _context = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
    }
}
