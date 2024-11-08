using OpenHRCore.Application.Workforce.DTOs.OU;

namespace OpenHRCore.Application.Workforce.Interfaces
{
    public interface IOrganizationUnitService
    {
        Task<OpenHRCoreServiceResponse<GetOrganizationUnitResponse>> CreateOrganizationUnitAsync(CreateOrganizationUnitRequest request);

        Task<OpenHRCoreServiceResponse<IEnumerable<GetOrganizationUnitResponse>>> GetAllOrganizationUnitAsync();
    }
}
