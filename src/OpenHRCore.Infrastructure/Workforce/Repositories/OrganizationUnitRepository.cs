

namespace OpenHRCore.Infrastructure.Workforce.Repositories
{
    public class OrganizationUnitRepository : OpenHRCoreEfBaseRepository<OrganizationUnit>, IOrganizationUnitRepository
    {
        public OrganizationUnitRepository(OpenHRCoreDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<OrganizationUnit>> GetAllOrganizationUnitsAsync()
        {
            return await _dbContext.Set<OrganizationUnit>()
               .Include(ou => ou.ParentOrganizationUnit)
               .Include(ou => ou.SubOrganizationUnits)
               .OrderBy(ou => ou.SortOrder) 
               .ToListAsync();
        }
    }
}
