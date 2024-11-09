using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using OpenHRCore.API.Common;
using OpenHRCore.Application.Workforce.DTOs.OU;
using OpenHRCore.Application.Workforce.Interfaces;

namespace OpenHRCore.API.Controllers
{
    /// <summary>
    /// Controller for managing organization units, providing CRUD operations and hierarchy management endpoints.
    /// Handles creation, retrieval, update and deletion of organization units as well as managing their hierarchical relationships.
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
        /// Initializes a new instance of the OrganizationUnitsController with required dependencies
        /// </summary>
        /// <param name="createOrganizationUnitRequestValidator">Validator for validating organization unit creation requests</param>
        /// <param name="organizationUnitService">Service for handling organization unit business logic and operations</param>
        /// <param name="logger">Logger for recording diagnostic information</param>
        /// <param name="sharedLocalizer">Localizer for handling internationalization of messages</param>
        public OrganizationUnitsController(
            IValidator<CreateOrganizationUnitRequest> createOrganizationUnitRequestValidator,
            IOrganizationUnitService organizationUnitService,
            ILogger<OrganizationUnitsController> logger,
            IStringLocalizer<SharedResource> sharedLocalizer)
        {
            _createOrganizationUnitRequestValidator = createOrganizationUnitRequestValidator;
            _organizationUnitService = organizationUnitService;
            _logger = logger;
            _sharedLocalizer = sharedLocalizer;
        }

        /// <summary>
        /// Creates a new organization unit in the system
        /// </summary>
        /// <param name="request">The request containing organization unit details to create</param>
        /// <returns>ActionResult containing the newly created organization unit details</returns>
        /// <response code="201">Organization unit created successfully</response>
        /// <response code="400">Invalid request data or validation failure</response>
        /// <response code="500">Internal server error during creation process</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateOrganizationUnitAsync([FromBody] CreateOrganizationUnitRequest request)
        {
            _logger.LogLayerInfo("[OrganizationUnitsController] Initiating creation of Organization Unit. Request: {@Request}", request);

            ValidationResult validationResult = await _createOrganizationUnitRequestValidator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                _logger.LogLayerWarning("[OrganizationUnitsController] Organization Unit creation validation failed. Errors: {@Errors}", validationResult.Errors);
                return OpenHRCoreApiResponseHelper.CreateValidationErrorResponse(validationResult);
            }

            try
            {
                var response = await _organizationUnitService.CreateOrganizationUnitAsync(request);

                if (!response.IsSuccess)
                {
                    _logger.LogLayerWarning("[OrganizationUnitsController] Organization Unit creation failed. Error: {Error}", response.ErrorMessage ?? "Unknown error");
                    return OpenHRCoreApiResponseHelper.CreateFailureResponse(response);
                }

                _logger.LogLayerInfo("[OrganizationUnitsController] Organization Unit created successfully. Response: {@Response}", response);
                return OpenHRCoreApiResponseHelper.CreateSuccessResponse(response, StatusCodes.Status201Created);
            }
            catch (Exception ex)
            {
                _logger.LogLayerError(ex, "[OrganizationUnitsController] An error occurred while creating the Organization Unit. Request: {@Request}", request);
                return OpenHRCoreApiResponseHelper.CreateErrorResponse(ex);
            }
        }

        /// <summary>
        /// Retrieves all organization units from the system
        /// </summary>
        /// <returns>ActionResult containing a list of all organization units</returns>
        /// <response code="200">Organization units retrieved successfully</response>
        /// <response code="500">Internal server error during retrieval process</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllOrganizationUnitsAsync()
        {
            _logger.LogLayerInfo("[OrganizationUnitsController] Retrieving all organization units.");

            try
            {
                var response = await _organizationUnitService.GetAllOrganizationUnitsAsync();

                if (!response.IsSuccess)
                {
                    _logger.LogLayerWarning("[OrganizationUnitsController] Organization Units retrieval failed. Error: {Error}", response.ErrorMessage ?? "Unknown error");
                    return OpenHRCoreApiResponseHelper.CreateFailureResponse(response);
                }

                _logger.LogLayerInfo("[OrganizationUnitsController] OrganizationUnits retrieved successfully. Count: {Count}", response.Data?.Count() ?? 0);
                return OpenHRCoreApiResponseHelper.CreateSuccessResponse(response);
            }
            catch (Exception ex)
            {
                _logger.LogLayerError(ex, "[OrganizationUnitsController] Error retrieving all OrganizationUnits");
                return OpenHRCoreApiResponseHelper.CreateErrorResponse(ex);
            }
        }

        /// <summary>
        /// Retrieves the complete organization unit hierarchy as a tree structure
        /// </summary>
        /// <returns>ActionResult containing hierarchical tree representation of organization units</returns>
        /// <response code="200">Organization unit hierarchy retrieved successfully</response>
        /// <response code="500">Internal server error during hierarchy retrieval</response>
        [HttpGet("hierarchy")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetOrganizationUnitsHierarchyAsync()
        {
            _logger.LogLayerInfo("[OrganizationUnitsController] Retrieving organization units hierarchy.");

            try
            {
                var response = await _organizationUnitService.GetAllOrganizationUnitsWithHierarchyAsync();

                if (!response.IsSuccess)
                {
                    _logger.LogLayerWarning("[OrganizationUnitsController] Organization Units hierarchy retrieval failed. Error: {Error}", response.ErrorMessage ?? "Unknown error");
                    return OpenHRCoreApiResponseHelper.CreateFailureResponse(response);
                }

                _logger.LogLayerInfo("[OrganizationUnitsController] Organization Units hierarchy retrieved successfully. Count: {Count}", response.Data?.Count() ?? 0);
                return OpenHRCoreApiResponseHelper.CreateSuccessResponse(response);
            }
            catch (Exception ex)
            {
                _logger.LogLayerError(ex, "[OrganizationUnitsController] Error retrieving Organization Units hierarchy");
                return OpenHRCoreApiResponseHelper.CreateErrorResponse(ex);
            }
        }

        /// <summary>
        /// Updates an existing organization unit's information
        /// </summary>
        /// <param name="id">The unique identifier of the organization unit to update</param>
        /// <param name="request">The request containing updated organization unit details</param>
        /// <returns>ActionResult containing the updated organization unit information</returns>
        /// <response code="200">Organization unit updated successfully</response>
        /// <response code="400">Invalid request data or ID mismatch</response>
        /// <response code="404">Organization unit not found</response>
        /// <response code="500">Internal server error during update process</response>
        [HttpPut("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateOrganizationUnitAsync(Guid id, [FromBody] UpdateOrganizationUnitRequest request)
        {
            _logger.LogLayerInfo("[OrganizationUnitsController] Initiating update of Organization Unit. ID: {Id}, Request: {@Request}", id, request);

            if (id != request.Id)
            {
                return BadRequest("The ID in the URL does not match the ID in the request body.");
            }

            try
            {
                var response = await _organizationUnitService.UpdateOrganizationUnitAsync(request);

                if (!response.IsSuccess)
                {
                    _logger.LogLayerWarning("[OrganizationUnitsController] Organization Unit update failed. Error: {Error}", response.ErrorMessage ?? "Unknown error");
                    return OpenHRCoreApiResponseHelper.CreateFailureResponse(response);
                }

                _logger.LogLayerInfo("[OrganizationUnitsController] Organization Unit updated successfully. Response: {@Response}", response);
                return OpenHRCoreApiResponseHelper.CreateSuccessResponse(response);
            }
            catch (Exception ex)
            {
                _logger.LogLayerError(ex, "[OrganizationUnitsController] An error occurred while updating the Organization Unit. ID: {Id}, Request: {@Request}", id, request);
                return OpenHRCoreApiResponseHelper.CreateErrorResponse(ex);
            }
        }

        /// <summary>
        /// Deletes an organization unit from the system
        /// </summary>
        /// <param name="id">The unique identifier of the organization unit to delete</param>
        /// <returns>ActionResult indicating the success or failure of the deletion operation</returns>
        /// <response code="204">Organization unit deleted successfully</response>
        /// <response code="404">Organization unit not found</response>
        /// <response code="500">Internal server error during deletion process</response>
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteOrganizationUnitAsync(Guid id)
        {
            _logger.LogLayerInfo("[OrganizationUnitsController] Initiating deletion of Organization Unit. ID: {Id}", id);

            try
            {
                var response = await _organizationUnitService.DeleteOrganizationUnitAsync(id);

                if (!response.IsSuccess)
                {
                    _logger.LogLayerWarning("[OrganizationUnitsController] Organization Unit deletion failed. Error: {Error}", response.ErrorMessage ?? "Unknown error");
                    return OpenHRCoreApiResponseHelper.CreateFailureResponse(response);
                }

                _logger.LogLayerInfo("[OrganizationUnitsController] Organization Unit deleted successfully.");
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogLayerError(ex, "[OrganizationUnitsController] An error occurred while deleting the Organization Unit. ID: {Id}", id);
                return OpenHRCoreApiResponseHelper.CreateErrorResponse(ex);
            }
        }

        /// <summary>
        /// Retrieves a specific organization unit by its unique identifier
        /// </summary>
        /// <param name="id">The unique identifier of the organization unit to retrieve</param>
        /// <returns>ActionResult containing the requested organization unit details</returns>
        /// <response code="200">Organization unit retrieved successfully</response>
        /// <response code="404">Organization unit not found</response>
        /// <response code="500">Internal server error during retrieval process</response>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetOrganizationUnitByIdAsync(Guid id)
        {
            _logger.LogLayerInfo("[OrganizationUnitsController] Retrieving Organization Unit. ID: {Id}", id);

            try
            {
                var response = await _organizationUnitService.GetOrganizationUnitByIdAsync(id);

                if (!response.IsSuccess)
                {
                    _logger.LogLayerWarning("[OrganizationUnitsController] Organization Unit retrieval failed. Error: {Error}", response.ErrorMessage ?? "Unknown error");
                    return OpenHRCoreApiResponseHelper.CreateFailureResponse(response);
                }

                _logger.LogLayerInfo("[OrganizationUnitsController] Organization Unit retrieved successfully. Response: {@Response}", response);
                return OpenHRCoreApiResponseHelper.CreateSuccessResponse(response);
            }
            catch (Exception ex)
            {
                _logger.LogLayerError(ex, "[OrganizationUnitsController] An error occurred while retrieving the Organization Unit. ID: {Id}", id);
                return OpenHRCoreApiResponseHelper.CreateErrorResponse(ex);
            }
        }

        /// <summary>
        /// Retrieves all child organization units for a specified parent organization unit
        /// </summary>
        /// <param name="id">The unique identifier of the parent organization unit</param>
        /// <returns>ActionResult containing a list of child organization units</returns>
        /// <response code="200">Child organization units retrieved successfully</response>
        /// <response code="404">Parent organization unit not found</response>
        /// <response code="500">Internal server error during retrieval process</response>
        [HttpGet("{id:guid}/children")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetChildOrganizationUnitsAsync(Guid id)
        {
            _logger.LogLayerInfo("[OrganizationUnitsController] Retrieving child Organization Units for parent ID: {ParentId}", id);

            try
            {
                var response = await _organizationUnitService.GetAllOrganizationUnitsByParentId(id);

                if (!response.IsSuccess)
                {
                    _logger.LogLayerWarning("[OrganizationUnitsController] Child Organization Units retrieval failed. Error: {Error}", response.ErrorMessage ?? "Unknown error");
                    return OpenHRCoreApiResponseHelper.CreateFailureResponse(response);
                }

                _logger.LogLayerInfo("[OrganizationUnitsController] Child Organization Units retrieved successfully. Response: {@Response}", response);
                return OpenHRCoreApiResponseHelper.CreateSuccessResponse(response);
            }
            catch (Exception ex)
            {
                _logger.LogLayerError(ex, "[OrganizationUnitsController] An error occurred while retrieving Child Organization Units for parent ID: {ParentId}", id);
                return OpenHRCoreApiResponseHelper.CreateErrorResponse(ex);
            }
        }
    }
}
