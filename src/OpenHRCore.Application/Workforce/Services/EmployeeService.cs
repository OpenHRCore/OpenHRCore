using AutoMapper;
using Microsoft.Extensions.Logging;
using OpenHRCore.Application.UnitOfWork;
using OpenHRCore.Application.Workforce.DTOs.EmployeeDtos;
using OpenHRCore.Application.Workforce.Interfaces;
using OpenHRCore.Domain.Workforce.Entities;
using OpenHRCore.Domain.Workforce.Interfaces;
using OpenHRCore.SharedKernel.Utilities;

namespace OpenHRCore.Application.Workforce.Services
{
    /// <summary>
    /// Service responsible for managing employee data and operations in the system.
    /// Implements CRUD operations and business logic for employee entities.
    /// </summary>
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IOpenHRCoreUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<EmployeeService> _logger;

        /// <summary>
        /// Initializes a new instance of the EmployeeService class.
        /// </summary>
        /// <param name="employeeRepository">Repository for employee data access operations</param>
        /// <param name="unitOfWork">Unit of work for managing database transactions</param>
        /// <param name="mapper">AutoMapper instance for object-to-object mapping</param>
        /// <param name="logger">Logger for service diagnostics and monitoring</param>
        /// <exception cref="ArgumentNullException">Thrown when any required dependency is null</exception>
        public EmployeeService(
            IEmployeeRepository employeeRepository,
            IOpenHRCoreUnitOfWork unitOfWork,
            IMapper mapper,
            ILogger<EmployeeService> logger)
        {
            _employeeRepository = employeeRepository ?? throw new ArgumentNullException(nameof(employeeRepository));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Creates a new employee in the system.
        /// </summary>
        /// <param name="request">The employee creation request containing required information</param>
        /// <returns>Response containing the created employee details if successful, or error information if failed</returns>
        public async Task<OpenHRCoreServiceResponse<GetEmployeeResponse>> CreateEmployeeAsync(CreateEmployeeRequest request)
        {
            _logger.LogLayerInfo("Beginning employee creation process for: {EmployeeName}", request.FirstName);

            try
            {
                var employee = _mapper.Map<Employee>(request);
                employee.CreatedAt = DateTime.UtcNow;

                await _employeeRepository.AddAsync(employee);
                await _unitOfWork.SaveChangesAsync();

                _logger.LogLayerInfo("Employee creation completed successfully. ID: {EmployeeId}", employee.Id);

                var response = await GetEmployeeResponseById(employee.Id);

                return OpenHRCoreServiceResponse<GetEmployeeResponse>.CreateSuccess(
                    response,
                    "Employee created successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogLayerError(ex, "Employee creation failed for: {EmployeeName}. Error: {ErrorMessage}", 
                    request.FirstName, ex.Message);
                return OpenHRCoreServiceResponse<GetEmployeeResponse>.CreateFailure(
                    ex,
                    "An error occurred while creating the Employee.");
            }
        }

        /// <summary>
        /// Updates an existing employee's information.
        /// </summary>
        /// <param name="request">The employee update request containing modified information</param>
        /// <returns>Response containing the updated employee details if successful, or error information if failed</returns>
        public async Task<OpenHRCoreServiceResponse<GetEmployeeResponse>> UpdateEmployeeAsync(UpdateEmployeeRequest request)
        {
            try
            {
                _logger.LogLayerInfo("Beginning update process for employee ID: {EmployeeId}", request.Id);

                var existingEmployee = await _employeeRepository.GetByIdAsync(request.Id);
                if (existingEmployee == null)
                {
                    _logger.LogLayerWarning("Update failed - Unable to locate employee with ID: {EmployeeId}", request.Id);
                    return OpenHRCoreServiceResponse<GetEmployeeResponse>.CreateFailure("Employee not found.");
                }

                _mapper.Map(request, existingEmployee);
                existingEmployee.UpdatedAt = DateTime.UtcNow;

                _employeeRepository.Update(existingEmployee);
                await _unitOfWork.SaveChangesAsync();

                _logger.LogLayerInfo("Employee update completed successfully for ID: {EmployeeId}", request.Id);

                var response = await GetEmployeeResponseById(request.Id);

                return OpenHRCoreServiceResponse<GetEmployeeResponse>.CreateSuccess(
                    response,
                    "Employee updated successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogLayerError(ex, "Employee update failed for ID: {EmployeeId}. Error: {ErrorMessage}", 
                    request.Id, ex.Message);
                return OpenHRCoreServiceResponse<GetEmployeeResponse>.CreateFailure(
                    ex,
                    "An error occurred while updating the Employee.");
            }
        }

        /// <summary>
        /// Deletes an employee from the system.
        /// </summary>
        /// <param name="id">The unique identifier of the employee to delete</param>
        /// <returns>Response containing the deleted employee details if successful, or error information if failed</returns>
        public async Task<OpenHRCoreServiceResponse<GetEmployeeResponse>> DeleteEmployeeAsync(Guid id)
        {
            try
            {
                _logger.LogLayerInfo("Beginning deletion process for employee ID: {EmployeeId}", id);

                var employee = await _employeeRepository.GetByIdAsync(id);
                if (employee == null)
                {
                    _logger.LogLayerWarning("Deletion failed - Unable to locate employee with ID: {EmployeeId}", id);
                    return OpenHRCoreServiceResponse<GetEmployeeResponse>.CreateFailure("Employee not found.");
                }

                _employeeRepository.Remove(employee);
                await _unitOfWork.SaveChangesAsync();

                _logger.LogLayerInfo("Employee successfully deleted. ID: {EmployeeId}", id);

                var response = _mapper.Map<GetEmployeeResponse>(employee);

                return OpenHRCoreServiceResponse<GetEmployeeResponse>.CreateSuccess(
                    response,
                    "Employee deleted successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogLayerError(ex, "Employee deletion failed for ID: {EmployeeId}. Error: {ErrorMessage}", 
                    id, ex.Message);
                return OpenHRCoreServiceResponse<GetEmployeeResponse>.CreateFailure(
                    ex,
                    "An error occurred while deleting the Employee.");
            }
        }

        /// <summary>
        /// Retrieves an employee by their unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the employee to retrieve</param>
        /// <returns>Response containing the employee details if found, or error information if not found or retrieval fails</returns>
        public async Task<OpenHRCoreServiceResponse<GetEmployeeResponse>> GetEmployeeByIdAsync(Guid id)
        {
            try
            {
                _logger.LogLayerInfo("Initiating retrieval of employee with ID: {EmployeeId}", id);

                var employee = await _employeeRepository.GetFirstOrDefaultAsync(
                    x => x.Id == id,
                    x => x.JobPosition!,
                    x => x.JobGrade!,
                    x => x.JobLevel!);

                if (employee == null)
                {
                    _logger.LogLayerWarning("Retrieval failed - Unable to locate employee with ID: {EmployeeId}", id);
                    return OpenHRCoreServiceResponse<GetEmployeeResponse>.CreateFailure("Employee not found.");
                }

                var response = _mapper.Map<GetEmployeeResponse>(employee);

                _logger.LogLayerInfo("Successfully retrieved employee details for ID: {EmployeeId}", id);

                return OpenHRCoreServiceResponse<GetEmployeeResponse>.CreateSuccess(
                    response,
                    "Employee retrieved successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogLayerError(ex, "Failed to retrieve employee with ID: {EmployeeId}. Error: {ErrorMessage}", 
                    id, ex.Message);
                return OpenHRCoreServiceResponse<GetEmployeeResponse>.CreateFailure(
                    ex,
                    "An error occurred while retrieving the Employee.");
            }
        }

        /// <summary>
        /// Retrieves all employees in the system.
        /// </summary>
        /// <returns>Response containing a collection of all employees if successful, or error information if retrieval fails</returns>
        public async Task<OpenHRCoreServiceResponse<IEnumerable<GetEmployeeResponse>>> GetAllEmployeesAsync()
        {
            try
            {
                _logger.LogLayerInfo("Initiating retrieval of all employees");

                var employees = await _employeeRepository.GetAllAsync(
                    x => x.JobPosition!,
                    x => x.JobGrade!,
                    x => x.JobLevel!,
                    x => x.OrganizationUnit!);

                var response = _mapper.Map<IEnumerable<GetEmployeeResponse>>(employees);

                _logger.LogLayerInfo("Successfully retrieved complete list of employees. Total count: {Count}", 
                    employees.Count());

                return OpenHRCoreServiceResponse<IEnumerable<GetEmployeeResponse>>.CreateSuccess(
                    response,
                    "Employees retrieved successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogLayerError(ex, "Failed to retrieve employees. Error: {ErrorMessage}", ex.Message);
                return OpenHRCoreServiceResponse<IEnumerable<GetEmployeeResponse>>.CreateFailure(
                    ex,
                    "An error occurred while retrieving the Employees.");
            }
        }

        /// <summary>
        /// Helper method to retrieve an employee response by ID with related entities.
        /// </summary>
        /// <param name="id">The unique identifier of the employee</param>
        /// <returns>Mapped employee response with related entity information</returns>
        private async Task<GetEmployeeResponse> GetEmployeeResponseById(Guid id)
        {
            var employee = await _employeeRepository.GetFirstOrDefaultAsync(
                x => x.Id == id,
                x => x.JobPosition!,
                x => x.JobGrade!,
                x => x.JobLevel!,
                x => x.OrganizationUnit!);

            return _mapper.Map<GetEmployeeResponse>(employee);
        }
    }
}
