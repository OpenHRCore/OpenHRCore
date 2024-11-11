using OpenHRCore.Application.Workforce.DTOs.EmployeeDtos;

namespace OpenHRCore.Application.Workforce.Interfaces
{
    public interface IEmployeeService
    {
        Task<OpenHRCoreServiceResponse<GetEmployeeResponse>> CreateEmployeeAsync(CreateEmployeeRequest request);
        Task<OpenHRCoreServiceResponse<GetEmployeeResponse>> UpdateEmployeeAsync(UpdateEmployeeRequest request);
        Task<OpenHRCoreServiceResponse<GetEmployeeResponse>> DeleteEmployeeAsync(Guid id);
        Task<OpenHRCoreServiceResponse<GetEmployeeResponse>> GetEmployeeByIdAsync(Guid id);
        Task<OpenHRCoreServiceResponse<IEnumerable<GetEmployeeResponse>>> GetAllEmployeesAsync();
    }
}
