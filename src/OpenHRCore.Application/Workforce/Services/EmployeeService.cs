using AutoMapper;
using Microsoft.Extensions.Logging;
using OpenHRCore.Application.Common;
using OpenHRCore.Application.UnitOfWork;
using OpenHRCore.Application.Workforce.DTOs.EmployeeDtos;
using OpenHRCore.Application.Workforce.Interfaces;
using OpenHRCore.Domain.Workforce.Entities;
using OpenHRCore.Domain.Workforce.Interfaces;
using OpenHRCore.SharedKernel.Utilities;
using System.Linq.Expressions;

namespace OpenHRCore.Application.Workforce.Services
{
    /// <summary>
    /// Service responsible for managing employee data and operations in the system.
    /// Implements CRUD operations and search capabilities for employee records.
    /// </summary>
    public class EmployeeService : IEmployeeService
    {
        private readonly IOpenHRCoreUnitOfWork _unitOfWork;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<EmployeeService> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="EmployeeService"/> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work for managing database transactions.</param>
        /// <param name="employeeRepository">The repository for employee data access.</param>
        /// <param name="mapper">The AutoMapper instance for object mapping.</param>
        /// <param name="logger">The logger for service diagnostics.</param>
        /// <exception cref="ArgumentNullException">Thrown when any required dependency is null.</exception>
        public EmployeeService(
            IOpenHRCoreUnitOfWork unitOfWork,
            IEmployeeRepository employeeRepository,
            IMapper mapper,
            ILogger<EmployeeService> logger)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _employeeRepository = employeeRepository ?? throw new ArgumentNullException(nameof(employeeRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Creates a new employee in the system.
        /// </summary>
        /// <param name="request">The employee creation request containing required information.</param>
        /// <returns>Response containing the created employee details if successful, or error information if failed.</returns>
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

                var response = await GetEmployeeResponseByIdAsync(employee.Id);

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
                    "An error occurred while creating the employee.");
            }
        }

        /// <summary>
        /// Updates an existing employee's information.
        /// </summary>
        /// <param name="request">The employee update request containing modified information.</param>
        /// <returns>Response containing the updated employee details if successful, or error information if failed.</returns>
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

                var response = await GetEmployeeResponseByIdAsync(request.Id);

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
                    "An error occurred while updating the employee.");
            }
        }

        /// <summary>
        /// Deletes an employee from the system.
        /// </summary>
        /// <param name="id">The unique identifier of the employee to delete.</param>
        /// <returns>Response containing the deleted employee details if successful, or error information if failed.</returns>
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
                    "An error occurred while deleting the employee.");
            }
        }

        /// <summary>
        /// Retrieves an employee by their unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the employee to retrieve.</param>
        /// <returns>Response containing the employee details if found, or error information if not found or retrieval fails.</returns>
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
                    "An error occurred while retrieving the employee.");
            }
        }

        /// <summary>
        /// Searches for employees based on specified criteria with pagination and sorting.
        /// </summary>
        /// <param name="request">The search request containing pagination, filtering and sorting parameters.</param>
        /// <returns>A paginated response containing matching employees if successful, or error information if failed.</returns>
        public async Task<OpenHRCorePaginatedResponse<IEnumerable<GetEmployeeResponse>>> SearchEmployeesAsync(
            SearchRequest request)
        {
            try
            {
                _logger.LogLayerInfo("Initiating employee search with filters: {@Request}", request);

                var includes = GetEmployeeIncludes();
                var searchCriteria = BuildSearchCriteria(request.Filters);
                var sortExpression = BuildSortExpression(request.Sorts.FirstOrDefault());

                var (totalCount, employees) = await _employeeRepository.GetPagedAsync(
                    pageNumber: request.Pagination.PageNumber,
                    pageSize: request.Pagination.PageSize,
                    orderBy: sortExpression.Expression,
                    ascending: sortExpression.IsAscending,
                    searchCriteria: searchCriteria,
                    includeProperties: includes);

                var mappedEmployees = _mapper.Map<IEnumerable<GetEmployeeResponse>>(employees);

                _logger.LogLayerInfo("Successfully retrieved {Count} employees matching search criteria", employees.Count());

                return OpenHRCorePaginatedResponse<IEnumerable<GetEmployeeResponse>>.CreateSuccess(
                    mappedEmployees,
                    totalCount,
                    request.Pagination.PageNumber,
                    request.Pagination.PageSize,
                    searchCriteria,
                    sortExpression.Field,
                    sortExpression.IsAscending,
                    "Employees retrieved successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogLayerError(ex, "Failed to search employees. Error: {ErrorMessage}", ex.Message);
                return OpenHRCorePaginatedResponse<IEnumerable<GetEmployeeResponse>>.CreateFailure(
                    ex,
                    "An error occurred while searching employees.");
            }
        }

        #region Private Methods

        /// <summary>
        /// Retrieves an employee response by ID with related entities.
        /// </summary>
        /// <param name="id">The unique identifier of the employee.</param>
        /// <returns>Mapped employee response with related entity information.</returns>
        private async Task<GetEmployeeResponse> GetEmployeeResponseByIdAsync(Guid id)
        {
            var employee = await _employeeRepository.GetFirstOrDefaultAsync(
                x => x.Id == id,
                x => x.JobPosition!,
                x => x.JobGrade!,
                x => x.JobLevel!,
                x => x.OrganizationUnit!);

            return _mapper.Map<GetEmployeeResponse>(employee);
        }

        /// <summary>
        /// Builds search criteria from the provided filters.
        /// </summary>
        /// <param name="filters">List of search filters to convert.</param>
        /// <returns>Dictionary of search criteria.</returns>
        private Dictionary<string, string> BuildSearchCriteria(List<SearchFilter> filters)
        {
            return filters.ToDictionary(
                filter => $"{filter.Field}:{filter.Operator}",
                filter => FormatFilterValue(filter));
        }

        /// <summary>
        /// Formats the filter value based on the operator.
        /// </summary>
        /// <param name="filter">The search filter to format.</param>
        /// <returns>Formatted filter value.</returns>
        private string FormatFilterValue(SearchFilter filter)
        {
            if (filter.Operator.Value == SearchOperator.In.Value)
                return filter.Value; // Comma-separated values
            if (filter.Operator.Value == SearchOperator.Between.Value)
                return filter.Value; // Comma-separated range
            if (filter.Operator.Value == SearchOperator.Contains.Value)
                return filter.Value; // Add wildcards
            return filter.Value;
        }

        /// <summary>
        /// Builds the sort expression for the query.
        /// </summary>
        /// <param name="sort">The sort criteria.</param>
        /// <returns>Sort expression details.</returns>
        private (Expression<Func<Employee, object>>? Expression, string? Field, bool IsAscending) BuildSortExpression(SortFilter? sort)
        {
            if (sort is null)
            {
                return (e => e.CreatedAt, "createdat", true);
            }

            var expression = sort.Field?.ToLower() switch
            {
                "code" => (Expression<Func<Employee, object>>)(e => e.Code),
                "firstname" => e => e.FirstName,
                "lastname" => e => e.LastName,
                "dateofbirth" => e => e.DateOfBirth!,
                "gender" => e => e.Gender,
                "email" => e => e.Email,
                "phone" => e => e.Phone,
                "address" => e => e.Address!,
                "joblevelname" => e => e.JobLevel!.LevelName,
                "jobgradename" => e => e.JobGrade!.GradeName,
                "jobtitlename" => e => e.JobPosition!.JobTitle,
                "organizationunitname" => e => e.OrganizationUnit!.Name,
                "createdat" => e => e.CreatedAt,
                "updatedat" => e => e.UpdatedAt!,
                _ => (Expression<Func<Employee, object>>)(e => e.CreatedAt)
            };

            return (expression, sort.Field, sort.Direction == SortDirection.Ascending);
        }

        /// <summary>
        /// Gets the include expressions for loading related entities.
        /// </summary>
        /// <returns>Array of include expressions.</returns>
        private Expression<Func<Employee, object>>[] GetEmployeeIncludes()
        {
            return new Expression<Func<Employee, object>>[]
            {
                e => e.JobPosition!,
                e => e.JobGrade!,
                e => e.JobLevel!,
                e => e.OrganizationUnit!
            };
        }

        #endregion
    }
}
