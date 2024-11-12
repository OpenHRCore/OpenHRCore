using OpenHRCore.Application.Common;
using OpenHRCore.Application.Workforce.DTOs.EmployeeDtos;

namespace OpenHRCore.Application.Workforce.Interfaces
{
    /// <summary>
    /// Provides services for managing employee data in the system.
    /// </summary>
    public interface IEmployeeService
    {
        /// <summary>
        /// Creates a new employee in the system.
        /// </summary>
        /// <param name="request">The employee creation request containing required information.</param>
        /// <returns>A response containing the created employee details if successful, or error information if failed.</returns>
        Task<OpenHRCoreServiceResponse<GetEmployeeResponse>> CreateEmployeeAsync(CreateEmployeeRequest request);

        /// <summary>
        /// Updates an existing employee's information.
        /// </summary>
        /// <param name="request">The employee update request containing modified information.</param>
        /// <returns>A response containing the updated employee details if successful, or error information if failed.</returns>
        Task<OpenHRCoreServiceResponse<GetEmployeeResponse>> UpdateEmployeeAsync(UpdateEmployeeRequest request);

        /// <summary>
        /// Deletes an employee from the system.
        /// </summary>
        /// <param name="id">The unique identifier of the employee to delete.</param>
        /// <returns>A response containing the deleted employee details if successful, or error information if failed.</returns>
        Task<OpenHRCoreServiceResponse<GetEmployeeResponse>> DeleteEmployeeAsync(Guid id);

        /// <summary>
        /// Retrieves an employee by their unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the employee to retrieve.</param>
        /// <returns>A response containing the employee details if found, or error information if not found or failed.</returns>
        Task<OpenHRCoreServiceResponse<GetEmployeeResponse>> GetEmployeeByIdAsync(Guid id);

        /// <summary>
        /// Searches for employees based on specified criteria with pagination support.
        /// </summary>
        /// <param name="request">The search request containing pagination, filtering and sorting parameters.</param>
        /// <returns>A paginated response containing matching employee records if successful, or error information if failed.</returns>
        Task<OpenHRCorePaginatedResponse<IEnumerable<GetEmployeeResponse>>> SearchEmployeesAsync(SearchRequest request);
    }
}
