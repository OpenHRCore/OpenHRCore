namespace OpenHRCore.Domain.Workforce.Interfaces
{
    public interface IOrganizationUnitRepository : IOpenHRCoreBaseRepository<OrganizationUnit>
    {
        Task<IEnumerable<OrganizationUnit>> GetAllOrganizationUnitsAsync();
    }
}
