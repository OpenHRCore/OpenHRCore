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
            _logger.LogLayerInfo("Retrieving all organization units.");

            try
            {
                var response = await _organizationUnitService.GetAllOrganizationUnitAsync();

                if (!response.IsSuccess)
                {
                    _logger.LogLayerWarning("Organization Units retrieval failed. Error: {Error}", response.ErrorMessage ?? "Unknown error");
                    return OpenHRCoreApiResponseHelper.CreateFailureResponse(response);
                }

                _logger.LogLayerInfo("OrganizationUnits retrieved successfully. Count: {Count}", response.Data?.Count() ?? 0);
                return OpenHRCoreApiResponseHelper.CreateSuccessResponse(response);
            }
            catch (Exception ex)
            {
                _logger.LogLayerError(ex, "Error retrieving all OrganizationUnits");
                return OpenHRCoreApiResponseHelper.CreateErrorResponse(ex);
            }
        }
    }
}
