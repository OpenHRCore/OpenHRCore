using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using OpenHRCore.API.Common;
using OpenHRCore.Application.Common;
using OpenHRCore.Application.Workforce.DTOs.EmployeeDtos;
using OpenHRCore.Application.Workforce.Interfaces;
using OpenHRCore.SharedKernel.Application;

namespace OpenHRCore.API.Controllers
{
    /// <summary>
    /// REST API controller for managing employees in the system.
    /// Provides endpoints for CRUD operations on employee resources.
    /// </summary>
    /// <remarks>
    /// REST API Conventions:
    /// - Uses standard HTTP methods (GET, POST, PUT, DELETE)
    /// - Returns appropriate HTTP status codes
    /// - Uses plural nouns for resource endpoints
    /// - Supports pagination, filtering and sorting
    /// - Provides consistent error responses
    /// - Uses proper content negotiation
    /// </remarks>
    [ApiController]
    [Route("api/v1/employees")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public class EmployeesController : ControllerBase
    {
        private readonly IValidator<CreateEmployeeRequest> _createEmployeeRequestValidator;
        private readonly IEmployeeService _employeeService;
        private readonly ILogger<EmployeesController> _logger;
        private readonly IStringLocalizer<SharedResource> _sharedLocalizer;

        /// <summary>
        /// Initializes a new instance of the EmployeesController.
        /// </summary>
        /// <param name="createEmployeeRequestValidator">Validator for employee creation requests</param>
        /// <param name="employeeService">Service for employee operations</param>
        /// <param name="logger">Logger for controller diagnostics</param>
        /// <param name="sharedLocalizer">Localizer for internationalization</param>
        /// <exception cref="ArgumentNullException">Thrown when any required dependency is null</exception>
        public EmployeesController(
            IValidator<CreateEmployeeRequest> createEmployeeRequestValidator,
            IEmployeeService employeeService,
            ILogger<EmployeesController> logger,
            IStringLocalizer<SharedResource> sharedLocalizer)
        {
            _createEmployeeRequestValidator = createEmployeeRequestValidator ?? throw new ArgumentNullException(nameof(createEmployeeRequestValidator));
            _employeeService = employeeService ?? throw new ArgumentNullException(nameof(employeeService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _sharedLocalizer = sharedLocalizer ?? throw new ArgumentNullException(nameof(sharedLocalizer));
        }

        /// <summary>
        /// Creates a new employee.
        /// </summary>
        /// <param name="request">The employee creation request containing required information</param>
        /// <returns>
        /// ActionResult containing:
        /// - 201 Created with the newly created employee details
        /// - 400 Bad Request if validation fails or request is invalid
        /// - 500 Internal Server Error if processing fails
        /// </returns>
        /// <remarks>
        /// Sample request:
        /// POST /api/v1/employees
        /// {
        ///     "firstName": "John",
        ///     "lastName": "Doe",
        ///     "email": "john.doe@example.com",
        ///     "departmentId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        ///     "jobPositionId": "3fa85f64-5717-4562-b3fc-2c963f66afa6"
        /// }
        /// </remarks>
        [HttpPost]
        [ProducesResponseType(typeof(OpenHRCoreServiceResponse<CreateEmployeeRequest>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(OpenHRCoreServiceResponse<object>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateAsync([FromBody] CreateEmployeeRequest request)
        {
            _logger.LogLayerInfo("Creating new employee. Request: {@Request}", request);

            ValidationResult validationResult = await _createEmployeeRequestValidator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                _logger.LogLayerWarning("Employee creation validation failed. Errors: {@Errors}", validationResult.Errors);
                return OpenHRCoreApiResponseHelper.CreateValidationErrorResponse(validationResult);
            }

            try
            {
                var response = await _employeeService.CreateEmployeeAsync(request);
                if (!response.IsSuccess)
                {
                    _logger.LogLayerWarning("Employee creation failed. Error: {Error}", response.ErrorMessage ?? "Unknown error");
                    return OpenHRCoreApiResponseHelper.CreateFailureResponse(response);
                }

                _logger.LogLayerInfo("Employee created successfully");
                return OpenHRCoreApiResponseHelper.CreateSuccessResponse(response, StatusCodes.Status201Created);
            }
            catch (Exception ex)
            {
                _logger.LogLayerError(ex, "Error creating employee");
                return OpenHRCoreApiResponseHelper.CreateErrorResponse(ex);
            }
        }

        /// <summary>
        /// Updates a specific employee resource.
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     PUT /api/v1/employees/{id}
        ///     {
        ///         "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        ///         "firstName": "John",
        ///         "lastName": "Doe",
        ///         "email": "john.doe@example.com"
        ///     }
        /// </remarks>
        /// <param name="id">The unique identifier of the employee to update</param>
        /// <param name="request">The employee update request containing modified information</param>
        /// <returns>
        /// ActionResult containing:
        /// - 200 OK with updated employee details
        /// - 400 Bad Request if validation fails or ID mismatch
        /// - 404 Not Found if employee doesn't exist
        /// - 500 Internal Server Error if update fails
        /// </returns>
        [HttpPut("{id:guid}")]
        [ProducesResponseType(typeof(OpenHRCoreServiceResponse<UpdateEmployeeRequest>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(OpenHRCoreServiceResponse<object>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] UpdateEmployeeRequest request)
        {
            _logger.LogLayerInfo("Updating employee {Id}", id);

            if (id != request.Id)
            {
                return BadRequest(_sharedLocalizer["IdMismatch"]);
            }

            try
            {
                var response = await _employeeService.UpdateEmployeeAsync(request);
                if (!response.IsSuccess)
                {
                    _logger.LogLayerWarning("Employee update failed. Error: {Error}", response.ErrorMessage ?? "Unknown error");
                    return OpenHRCoreApiResponseHelper.CreateFailureResponse(response);
                }

                _logger.LogLayerInfo("Employee {Id} updated successfully", id);
                return OpenHRCoreApiResponseHelper.CreateSuccessResponse(response);
            }
            catch (Exception ex)
            {
                _logger.LogLayerError(ex, "Error updating employee {Id}", id);
                return OpenHRCoreApiResponseHelper.CreateErrorResponse(ex);
            }
        }

        /// <summary>
        /// Deletes a specific employee resource.
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     DELETE /api/v1/employees/{id}
        /// </remarks>
        /// <param name="id">The unique identifier of the employee to delete</param>
        /// <returns>
        /// ActionResult containing:
        /// - 204 No Content on successful deletion
        /// - 404 Not Found if employee doesn't exist
        /// - 500 Internal Server Error if deletion fails
        /// </returns>
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            _logger.LogLayerInfo("Deleting employee {Id}", id);

            try
            {
                var response = await _employeeService.DeleteEmployeeAsync(id);
                if (!response.IsSuccess)
                {
                    _logger.LogLayerWarning("Employee deletion failed. Error: {Error}", response.ErrorMessage ?? "Unknown error");
                    return OpenHRCoreApiResponseHelper.CreateFailureResponse(response);
                }

                _logger.LogLayerInfo("Employee {Id} deleted successfully", id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogLayerError(ex, "Error deleting employee {Id}", id);
                return OpenHRCoreApiResponseHelper.CreateErrorResponse(ex);
            }
        }

        /// <summary>
        /// Retrieves a specific employee resource by ID.
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     GET /api/v1/employees/{id}
        /// </remarks>
        /// <param name="id">The unique identifier of the employee to retrieve</param>
        /// <returns>
        /// ActionResult containing:
        /// - 200 OK with the requested employee details
        /// - 404 Not Found if employee doesn't exist
        /// - 500 Internal Server Error if retrieval fails
        /// </returns>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(OpenHRCoreServiceResponse<GetEmployeeResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            _logger.LogLayerInfo("Retrieving employee {Id}", id);

            try
            {
                var response = await _employeeService.GetEmployeeByIdAsync(id);
                if (!response.IsSuccess)
                {
                    _logger.LogLayerWarning("Employee retrieval failed. Error: {Error}", response.ErrorMessage ?? "Unknown error");
                    return OpenHRCoreApiResponseHelper.CreateFailureResponse(response);
                }

                _logger.LogLayerInfo("Employee {Id} retrieved successfully", id);
                return OpenHRCoreApiResponseHelper.CreateSuccessResponse(response);
            }
            catch (Exception ex)
            {
                _logger.LogLayerError(ex, "Error retrieving employee {Id}", id);
                return OpenHRCoreApiResponseHelper.CreateErrorResponse(ex);
            }
        }

        /// <summary>
        /// Retrieves all employees with optional filtering, sorting, and pagination.
        /// </summary>
        /// <param name="request">The search request containing filtering, sorting, and pagination parameters</param>
        /// <returns>
        /// ActionResult containing:
        /// - 200 OK with paginated list of employees
        /// - 400 Bad Request if search parameters are invalid
        /// - 500 Internal Server Error if retrieval fails
        /// </returns>
        /// <remarks>
        /// Sample request:
        /// POST /api/v1/employees/search
        /// {
        ///     "pagination": {
        ///         "pageNumber": 1,
        ///         "pageSize": 10
        ///     },
        ///     "filters": [
        ///         {
        ///             "field": "firstName",
        ///             "operator": "Contains",
        ///             "value": "John"
        ///         }
        ///     ],
        ///     "sorts": [
        ///         {
        ///             "field": "lastName",
        ///             "direction": "Ascending"
        ///         }
        ///     ]
        /// }
        /// </remarks>
        [HttpPost("search")]
        [ProducesResponseType(typeof(OpenHRCorePaginatedResponse<IEnumerable<GetEmployeeResponse>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(OpenHRCoreServiceResponse<object>), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<OpenHRCorePaginatedResponse<IEnumerable<GetEmployeeResponse>>>> SearchAsync(
            [FromBody] SearchRequest request)
        {
            try
            {
                _logger.LogLayerInfo("Searching employees with filters: {@Request}", request);
                var result = await _employeeService.SearchEmployeesAsync(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogLayerError(ex, "Error searching employees");
                return BadRequest(new OpenHRCoreServiceResponse<object>
                {
                    IsSuccess = false,
                    ErrorMessage = "Error searching employees"
                });
            }
        }
    }
}
