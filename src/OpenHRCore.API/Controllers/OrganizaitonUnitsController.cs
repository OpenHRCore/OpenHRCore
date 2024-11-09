using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using OpenHRCore.API.Common;
using OpenHRCore.Application.Workforce.DTOs.OU;
using OpenHRCore.Application.Workforce.Interfaces;

namespace OpenHRCore.API.Controllers
{
    [ApiController]
    [Route("api/v1/organization-units")]
    [Produces("application/json")]
    public class OrganizationUnitsController : ControllerBase
    {
        private readonly IValidator<CreateOrganizationUnitRequest> _createOrganizationUnitRequestValidator;
        private readonly IOrganizationUnitService _organizationUnitService;
        private readonly ILogger<OrganizationUnitsController> _logger;
        private readonly IStringLocalizer<SharedResource> _sharedLocalizer;

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

        [HttpGet("tree")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetOrganizationUnitsTreeAsync()
        {
            _logger.LogLayerInfo("[OrganizationUnitsController] Retrieving organization units tree.");

            try
            {
                var response = await _organizationUnitService.GetAllOrganizationUnitsWithHierarchyAsync();

                if (!response.IsSuccess)
                {
                    _logger.LogLayerWarning("[OrganizationUnitsController] Organization Units tree retrieval failed. Error: {Error}", response.ErrorMessage ?? "Unknown error");
                    return OpenHRCoreApiResponseHelper.CreateFailureResponse(response);
                }

                _logger.LogLayerInfo("[OrganizationUnitsController] Organization Units tree retrieved successfully. Count: {Count}", response.Data?.Count() ?? 0);
                return OpenHRCoreApiResponseHelper.CreateSuccessResponse(response);
            }
            catch (Exception ex)
            {
                _logger.LogLayerError(ex, "[OrganizationUnitsController] Error retrieving Organization Units tree");
                return OpenHRCoreApiResponseHelper.CreateErrorResponse(ex);
            }
        }

        [HttpPut("{id}")]
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

        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
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

                _logger.LogLayerInfo("[OrganizationUnitsController] Organization Unit deleted successfully. Response: {@Response}", response);
                return OpenHRCoreApiResponseHelper.CreateSuccessResponse(response);
            }
            catch (Exception ex)
            {
                _logger.LogLayerError(ex, "[OrganizationUnitsController] An error occurred while deleting the Organization Unit. ID: {Id}", id);
                return OpenHRCoreApiResponseHelper.CreateErrorResponse(ex);
            }
        }

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

        [HttpGet("{parentId:guid}/sub-units")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetOrganizationUnitsByParentIdAsync(Guid parentId)
        {
            _logger.LogLayerInfo("[OrganizationUnitsController] Retrieving Organization Units by Parent ID: {ParentId}", parentId);

            try
            {
                var response = await _organizationUnitService.GetAllOrganizationUnitsByParentId(parentId);

                if (!response.IsSuccess)
                {
                    _logger.LogLayerWarning("[OrganizationUnitsController] Organization Units retrieval failed. Error: {Error}", response.ErrorMessage ?? "Unknown error");
                    return OpenHRCoreApiResponseHelper.CreateFailureResponse(response);
                }

                _logger.LogLayerInfo("[OrganizationUnitsController] Organization Units retrieved successfully. Response: {@Response}", response);
                return OpenHRCoreApiResponseHelper.CreateSuccessResponse(response);
            }
            catch (Exception ex)
            {
                _logger.LogLayerError(ex, "[OrganizationUnitsController] An error occurred while retrieving Organization Units by Parent ID: {ParentId}", parentId);
                return OpenHRCoreApiResponseHelper.CreateErrorResponse(ex);
            }
        }
    }
}
