using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using OpenHRCore.API.Common;
using OpenHRCore.Application.Workforce.DTOs.EmployeeDtos;
using OpenHRCore.Application.Workforce.Interfaces;

namespace OpenHRCore.API.Controllers
{
    /// <summary>
    /// REST API controller for managing employees in the system.
    /// Provides endpoints for CRUD operations on employee resources.
    /// </summary>
    [ApiController]
    [Route("api/v1/employees")]
    [Produces("application/json")]
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
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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
        /// Retrieves all employees.
        /// </summary>
        /// <returns>
        /// ActionResult containing:
        /// - 200 OK with list of all employees
        /// - 500 Internal Server Error if retrieval fails
        /// </returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllAsync()
        {
            _logger.LogLayerInfo("Retrieving all employees");

            try
            {
                var response = await _employeeService.GetAllEmployeesAsync();
                if (!response.IsSuccess)
                {
                    _logger.LogLayerWarning("Employees retrieval failed. Error: {Error}", response.ErrorMessage ?? "Unknown error");
                    return OpenHRCoreApiResponseHelper.CreateFailureResponse(response);
                }

                _logger.LogLayerInfo("Employees retrieved successfully. Count: {Count}", response.Data?.Count() ?? 0);
                return OpenHRCoreApiResponseHelper.CreateSuccessResponse(response);
            }
            catch (Exception ex)
            {
                _logger.LogLayerError(ex, "Error retrieving employees");
                return OpenHRCoreApiResponseHelper.CreateErrorResponse(ex);
            }
        }

        /// <summary>
        /// Updates a specific employee.
        /// </summary>
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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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
        /// Deletes a specific employee.
        /// </summary>
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
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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
                return OpenHRCoreApiResponseHelper.CreateSuccessResponse(response);
            }
            catch (Exception ex)
            {
                _logger.LogLayerError(ex, "Error deleting employee {Id}", id);
                return OpenHRCoreApiResponseHelper.CreateErrorResponse(ex);
            }
        }

        /// <summary>
        /// Retrieves a specific employee by ID.
        /// </summary>
        /// <param name="id">The unique identifier of the employee to retrieve</param>
        /// <returns>
        /// ActionResult containing:
        /// - 200 OK with the requested employee details
        /// - 404 Not Found if employee doesn't exist
        /// - 500 Internal Server Error if retrieval fails
        /// </returns>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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
    }
}
