using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using OpenHRCore.API.Common;
using OpenHRCore.Application.Workforce.DTOs.OUDtos;
using OpenHRCore.Application.Workforce.Interfaces;

namespace OpenHRCore.API.Controllers
{
    /// <summary>
    /// REST API controller for managing organization units in the system.
    /// Provides endpoints for CRUD operations and hierarchy management of organization unit resources.
    /// </summary>
    [ApiController]
    [Route("api/v1/organization-units")]
    [Produces("application/json")]
    public class OrganizationUnitsController : ControllerBase
    {
        private readonly IValidator<CreateOrganizationUnitRequest> _createOrganizationUnitRequestValidator;
        private readonly IOrganizationUnitService _organizationUnitService;
        private readonly ILogger<OrganizationUnitsController> _logger;
        private readonly IStringLocalizer<SharedResource> _sharedLocalizer;

        /// <summary>
        /// Initializes a new instance of the OrganizationUnitsController.
        /// </summary>
        /// <param name="createOrganizationUnitRequestValidator">Validator for organization unit creation requests</param>
        /// <param name="organizationUnitService">Service for organization unit operations</param>
        /// <param name="logger">Logger for controller diagnostics</param>
        /// <param name="sharedLocalizer">Localizer for internationalization</param>
        /// <exception cref="ArgumentNullException">Thrown when any required dependency is null</exception>
        public OrganizationUnitsController(
            IValidator<CreateOrganizationUnitRequest> createOrganizationUnitRequestValidator,
            IOrganizationUnitService organizationUnitService,
            ILogger<OrganizationUnitsController> logger,
            IStringLocalizer<SharedResource> sharedLocalizer)
        {
            _createOrganizationUnitRequestValidator = createOrganizationUnitRequestValidator ?? throw new ArgumentNullException(nameof(createOrganizationUnitRequestValidator));
            _organizationUnitService = organizationUnitService ?? throw new ArgumentNullException(nameof(organizationUnitService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _sharedLocalizer = sharedLocalizer ?? throw new ArgumentNullException(nameof(sharedLocalizer));
        }

        /// <summary>
        /// Creates a new organization unit.
        /// </summary>
        /// <param name="request">The organization unit creation request containing required information</param>
        /// <returns>
        /// ActionResult containing:
        /// - 201 Created with the newly created organization unit details
        /// - 400 Bad Request if validation fails or request is invalid
        /// - 500 Internal Server Error if processing fails
        /// </returns>
        /// <remarks>
        /// Sample request:
        /// POST /api/v1/organization-units
        /// {
        ///     "name": "HR Department",
        ///     "code": "HR-001",
        ///     "description": "Human Resources Department",
        ///     "parentId": "00000000-0000-0000-0000-000000000000"
        /// }
        /// </remarks>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateAsync([FromBody] CreateOrganizationUnitRequest request)
        {
            _logger.LogLayerInfo("Creating new organization unit. Request: {@Request}", request);

            ValidationResult validationResult = await _createOrganizationUnitRequestValidator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                _logger.LogLayerWarning("Organization unit creation validation failed. Errors: {@Errors}", validationResult.Errors);
                return OpenHRCoreApiResponseHelper.CreateValidationErrorResponse(validationResult);
            }

            try
            {
                var response = await _organizationUnitService.CreateOrganizationUnitAsync(request);
                if (!response.IsSuccess)
                {
                    _logger.LogLayerWarning("Organization unit creation failed. Error: {Error}", response.ErrorMessage ?? "Unknown error");
                    return OpenHRCoreApiResponseHelper.CreateFailureResponse(response);
                }

                _logger.LogLayerInfo("Organization unit created successfully");
                return OpenHRCoreApiResponseHelper.CreateSuccessResponse(response, StatusCodes.Status201Created);
            }
            catch (Exception ex)
            {
                _logger.LogLayerError(ex, "Error creating organization unit");
                return OpenHRCoreApiResponseHelper.CreateErrorResponse(ex);
            }
        }

        /// <summary>
        /// Retrieves all organization units.
        /// </summary>
        /// <returns>
        /// ActionResult containing:
        /// - 200 OK with list of all organization units
        /// - 500 Internal Server Error if retrieval fails
        /// </returns>
        /// <remarks>
        /// Sample request:
        /// GET /api/v1/organization-units
        /// </remarks>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllAsync()
        {
            _logger.LogLayerInfo("Retrieving all organization units");

            try
            {
                var response = await _organizationUnitService.GetAllOrganizationUnitsAsync();
                if (!response.IsSuccess)
                {
                    _logger.LogLayerWarning("Organization units retrieval failed. Error: {Error}", response.ErrorMessage ?? "Unknown error");
                    return OpenHRCoreApiResponseHelper.CreateFailureResponse(response);
                }

                _logger.LogLayerInfo("Organization units retrieved successfully. Count: {Count}", response.Data?.Count() ?? 0);
                return OpenHRCoreApiResponseHelper.CreateSuccessResponse(response);
            }
            catch (Exception ex)
            {
                _logger.LogLayerError(ex, "Error retrieving organization units");
                return OpenHRCoreApiResponseHelper.CreateErrorResponse(ex);
            }
        }

        /// <summary>
        /// Retrieves the organization unit hierarchy.
        /// </summary>
        /// <returns>
        /// ActionResult containing:
        /// - 200 OK with hierarchical tree of organization units
        /// - 500 Internal Server Error if retrieval fails
        /// </returns>
        /// <remarks>
        /// Sample request:
        /// GET /api/v1/organization-units/hierarchy
        /// </remarks>
        [HttpGet("hierarchy")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetHierarchyAsync()
        {
            _logger.LogLayerInfo("Retrieving organization unit hierarchy");

            try
            {
                var response = await _organizationUnitService.GetAllOrganizationUnitsWithHierarchyAsync();
                if (!response.IsSuccess)
                {
                    _logger.LogLayerWarning("Organization unit hierarchy retrieval failed. Error: {Error}", response.ErrorMessage ?? "Unknown error");
                    return OpenHRCoreApiResponseHelper.CreateFailureResponse(response);
                }

                _logger.LogLayerInfo("Organization unit hierarchy retrieved successfully");
                return OpenHRCoreApiResponseHelper.CreateSuccessResponse(response);
            }
            catch (Exception ex)
            {
                _logger.LogLayerError(ex, "Error retrieving organization unit hierarchy");
                return OpenHRCoreApiResponseHelper.CreateErrorResponse(ex);
            }
        }

        /// <summary>
        /// Updates a specific organization unit.
        /// </summary>
        /// <param name="id">The unique identifier of the organization unit to update</param>
        /// <param name="request">The organization unit update request containing updated information</param>
        /// <returns>
        /// ActionResult containing:
        /// - 200 OK with updated organization unit details
        /// - 400 Bad Request if validation fails or ID mismatch
        /// - 404 Not Found if organization unit doesn't exist
        /// - 500 Internal Server Error if update fails
        /// </returns>
        /// <remarks>
        /// Sample request:
        /// PUT /api/v1/organization-units/{id}
        /// {
        ///     "id": "123e4567-e89b-12d3-a456-426614174000",
        ///     "name": "Updated HR Department",
        ///     "code": "HR-001",
        ///     "description": "Updated Human Resources Department",
        ///     "parentId": "00000000-0000-0000-0000-000000000000"
        /// }
        /// </remarks>
        [HttpPut("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] UpdateOrganizationUnitRequest request)
        {
            _logger.LogLayerInfo("Updating organization unit {Id}", id);

            if (id != request.Id)
            {
                return BadRequest(_sharedLocalizer["IdMismatch"]);
            }

            try
            {
                var response = await _organizationUnitService.UpdateOrganizationUnitAsync(request);
                if (!response.IsSuccess)
                {
                    _logger.LogLayerWarning("Organization unit update failed. Error: {Error}", response.ErrorMessage ?? "Unknown error");
                    return OpenHRCoreApiResponseHelper.CreateFailureResponse(response);
                }

                _logger.LogLayerInfo("Organization unit {Id} updated successfully", id);
                return OpenHRCoreApiResponseHelper.CreateSuccessResponse(response);
            }
            catch (Exception ex)
            {
                _logger.LogLayerError(ex, "Error updating organization unit {Id}", id);
                return OpenHRCoreApiResponseHelper.CreateErrorResponse(ex);
            }
        }

        /// <summary>
        /// Deletes a specific organization unit.
        /// </summary>
        /// <param name="id">The unique identifier of the organization unit to delete</param>
        /// <returns>
        /// ActionResult containing:
        /// - 204 No Content on successful deletion
        /// - 404 Not Found if organization unit doesn't exist
        /// - 500 Internal Server Error if deletion fails
        /// </returns>
        /// <remarks>
        /// Sample request:
        /// DELETE /api/v1/organization-units/{id}
        /// </remarks>
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            _logger.LogLayerInfo("Deleting organization unit {Id}", id);

            try
            {
                var response = await _organizationUnitService.DeleteOrganizationUnitAsync(id);
                if (!response.IsSuccess)
                {
                    _logger.LogLayerWarning("Organization unit deletion failed. Error: {Error}", response.ErrorMessage ?? "Unknown error");
                    return OpenHRCoreApiResponseHelper.CreateFailureResponse(response);
                }

                _logger.LogLayerInfo("Organization unit {Id} deleted successfully", id);
                return OpenHRCoreApiResponseHelper.CreateSuccessResponse(response);
            }
            catch (Exception ex)
            {
                _logger.LogLayerError(ex, "Error deleting organization unit {Id}", id);
                return OpenHRCoreApiResponseHelper.CreateErrorResponse(ex);
            }
        }

        /// <summary>
        /// Retrieves a specific organization unit by ID.
        /// </summary>
        /// <param name="id">The unique identifier of the organization unit to retrieve</param>
        /// <returns>
        /// ActionResult containing:
        /// - 200 OK with the requested organization unit details
        /// - 404 Not Found if organization unit doesn't exist
        /// - 500 Internal Server Error if retrieval fails
        /// </returns>
        /// <remarks>
        /// Sample request:
        /// GET /api/v1/organization-units/{id}
        /// </remarks>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            _logger.LogLayerInfo("Retrieving organization unit {Id}", id);

            try
            {
                var response = await _organizationUnitService.GetOrganizationUnitByIdAsync(id);
                if (!response.IsSuccess)
                {
                    _logger.LogLayerWarning("Organization unit retrieval failed. Error: {Error}", response.ErrorMessage ?? "Unknown error");
                    return OpenHRCoreApiResponseHelper.CreateFailureResponse(response);
                }

                _logger.LogLayerInfo("Organization unit {Id} retrieved successfully", id);
                return OpenHRCoreApiResponseHelper.CreateSuccessResponse(response);
            }
            catch (Exception ex)
            {
                _logger.LogLayerError(ex, "Error retrieving organization unit {Id}", id);
                return OpenHRCoreApiResponseHelper.CreateErrorResponse(ex);
            }
        }

        /// <summary>
        /// Retrieves all child organization units for a specific parent.
        /// </summary>
        /// <param name="id">The unique identifier of the parent organization unit</param>
        /// <returns>
        /// ActionResult containing:
        /// - 200 OK with list of child organization units
        /// - 404 Not Found if parent organization unit doesn't exist
        /// - 500 Internal Server Error if retrieval fails
        /// </returns>
        /// <remarks>
        /// Sample request:
        /// GET /api/v1/organization-units/{id}/children
        /// </remarks>
        [HttpGet("{id:guid}/children")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetChildrenAsync(Guid id)
        {
            _logger.LogLayerInfo("Retrieving child organization units for parent {Id}", id);

            try
            {
                var response = await _organizationUnitService.GetAllOrganizationUnitsByParentId(id);
                if (!response.IsSuccess)
                {
                    _logger.LogLayerWarning("Child organization units retrieval failed. Error: {Error}", response.ErrorMessage ?? "Unknown error");
                    return OpenHRCoreApiResponseHelper.CreateFailureResponse(response);
                }

                _logger.LogLayerInfo("Child organization units for parent {Id} retrieved successfully", id);
                return OpenHRCoreApiResponseHelper.CreateSuccessResponse(response);
            }
            catch (Exception ex)
            {
                _logger.LogLayerError(ex, "Error retrieving child organization units for parent {Id}", id);
                return OpenHRCoreApiResponseHelper.CreateErrorResponse(ex);
            }
        }
    }
}
