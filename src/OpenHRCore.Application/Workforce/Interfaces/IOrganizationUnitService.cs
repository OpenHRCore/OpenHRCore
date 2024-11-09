using OpenHRCore.Application.Workforce.DTOs.OU;

namespace OpenHRCore.Application.Workforce.Interfaces
{
    public interface IOrganizationUnitService
    {
        Task<OpenHRCoreServiceResponse<GetOrganizationUnitResponse>> CreateOrganizationUnitAsync(CreateOrganizationUnitRequest request);

        Task<OpenHRCoreServiceResponse<GetOrganizationUnitResponse>> UpdateOrganizationUnitAsync(UpdateOrganizationUnitRequest request);

        Task<OpenHRCoreServiceResponse<GetOrganizationUnitResponse>> DeleteOrganizationUnitAsync(Guid id);

        Task<OpenHRCoreServiceResponse<GetOrganizationUnitResponse>> GetOrganizationUnitByIdAsync(Guid id);

        Task<OpenHRCoreServiceResponse<IEnumerable<GetOrganizationUnitsWithHierarchyResponse>>> GetAllOrganizationUnitsByParentId(Guid parentId);

        Task<OpenHRCoreServiceResponse<IEnumerable<GetOrganizationUnitsWithHierarchyResponse>>> GetAllOrganizationUnitsWithHierarchyAsync();

        Task<OpenHRCoreServiceResponse<IEnumerable<GetOrganizationUnitResponse>>> GetAllOrganizationUnitsAsync();

    }
}
