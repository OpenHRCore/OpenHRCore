namespace OpenHRCore.Domain.Workforce.Interfaces
{
    public interface IOrganizationUnitRepository : IOpenHRCoreBaseRepository<OrganizationUnit>
    {
        Task<List<OrganizationUnit>> GetAllOrganizationUnitsWithHierarchyAsync(Guid? parentId);
    }
}
