using Microsoft.Extensions.Logging;

namespace OpenHRCore.Infrastructure.Workforce.Repositories
{
    public class EmployeeRepository : OpenHRCoreEfBaseRepository<Employee>, IEmployeeRepository
    {
        private readonly ILogger<EmployeeRepository> _logger;
        private readonly OpenHRCoreDbContext _context;


        public EmployeeRepository(OpenHRCoreDbContext context, ILogger<EmployeeRepository> logger) : base(context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public IQueryable<Employee> GetQueryable()
        {
            return _context.Set<Employee>()
           .AsNoTracking() 
           .Where(e => !e.IsDeleted); 
        }
    }
}
