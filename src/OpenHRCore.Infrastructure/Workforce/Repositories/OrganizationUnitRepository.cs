using Microsoft.Extensions.Logging;
using OpenHRCore.SharedKernel.Utilities;

namespace OpenHRCore.Infrastructure.Workforce.Repositories
{
    /// <summary>
    /// Repository for managing organization unit entities
    /// </summary>
    public class OrganizationUnitRepository : OpenHRCoreEfBaseRepository<OrganizationUnit>, IOrganizationUnitRepository
    {
        private readonly ILogger<OrganizationUnitRepository> _logger;
        private readonly OpenHRCoreDbContext _context;

        /// <summary>
        /// Initializes a new instance of the OrganizationUnitRepository class
        /// </summary>
        /// <param name="context">The database context</param>
        /// <param name="logger">The logger instance</param>
        public OrganizationUnitRepository(OpenHRCoreDbContext context, ILogger<OrganizationUnitRepository> logger) : base(context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Retrieves all organization units with their hierarchical structure
        /// </summary>
        /// <param name="parentId">Optional parent organization unit ID to filter by</param>
        /// <returns>A list of organization units with their sub-units populated</returns>
        public async Task<List<OrganizationUnit>> GetAllOrganizationUnitsWithHierarchyAsync(Guid? parentId)
        {
            _logger.LogLayerInfo("Retrieving organization units with hierarchy. Parent ID: {ParentId}", parentId);

            try
            {
                var organizationUnitList = await _context.OrganizationUnits
                    .Include(organizationUnit => organizationUnit.ParentOrganizationUnit)
                    .Where(organizationUnit => organizationUnit.ParentOrganizationUnitId == parentId)
                    .OrderBy(organizationUnit => organizationUnit.SortOrder)
                    .ToListAsync();

                foreach (var organizationUnit in organizationUnitList)
                {
                    var hasSubUnits = await _context.OrganizationUnits
                        .AnyAsync(unit => unit.ParentOrganizationUnitId == organizationUnit.Id);

                    if (hasSubUnits)
                    {
                        _logger.LogLayerInfo("Retrieving sub-units for organization unit {OrganizationUnitId}", organizationUnit.Id);
                        organizationUnit.SubOrganizationUnits = await GetAllOrganizationUnitsWithHierarchyAsync(organizationUnit.Id);
                    }
                }

                _logger.LogLayerInfo("Successfully retrieved {Count} organization units", organizationUnitList.Count);
                return organizationUnitList;
            }
            catch (Exception ex)
            {
                _logger.LogLayerError(ex, "Error retrieving organization units with hierarchy. Parent ID: {ParentId}", parentId);
                throw;
            }
        }
    }
}
