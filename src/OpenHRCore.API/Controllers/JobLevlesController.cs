using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using OpenHRCore.API.Common;
using OpenHRCore.Application.Workforce.DTOs.JobLevelDtos;
using OpenHRCore.Application.Workforce.Interfaces;

namespace OpenHRCore.API.Controllers
{
    /// <summary>
    /// REST API controller for managing job levels in the system.
    /// Provides endpoints for CRUD operations on job level resources.
    /// </summary>
    [ApiController]
    [Route("api/v1/job-levels")]
    [Produces("application/json")]
    public class JobLevelsController : ControllerBase
    {
        private readonly IValidator<CreateJobLevelRequest> _createJobLevelRequestValidator;
        private readonly IJobLevelService _jobLevelService;
        private readonly ILogger<JobLevelsController> _logger;
        private readonly IStringLocalizer<SharedResource> _sharedLocalizer;

        /// <summary>
        /// Initializes a new instance of the JobLevelsController.
        /// </summary>
        /// <param name="createJobLevelRequestValidator">Validator for job level creation requests</param>
        /// <param name="jobLevelService">Service for job level operations</param>
        /// <param name="logger">Logger for controller diagnostics</param>
        /// <param name="sharedLocalizer">Localizer for internationalization</param>
        /// <exception cref="ArgumentNullException">Thrown when any required dependency is null</exception>
        public JobLevelsController(
            IValidator<CreateJobLevelRequest> createJobLevelRequestValidator,
            IJobLevelService jobLevelService,
            ILogger<JobLevelsController> logger,
            IStringLocalizer<SharedResource> sharedLocalizer)
        {
            _createJobLevelRequestValidator = createJobLevelRequestValidator ?? throw new ArgumentNullException(nameof(createJobLevelRequestValidator));
            _jobLevelService = jobLevelService ?? throw new ArgumentNullException(nameof(jobLevelService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _sharedLocalizer = sharedLocalizer ?? throw new ArgumentNullException(nameof(sharedLocalizer));
        }

        /// <summary>
        /// Creates a new job level.
        /// </summary>
        /// <param name="request">The job level creation request containing required information</param>
        /// <returns>
        /// ActionResult containing:
        /// - 201 Created with the newly created job level details
        /// - 400 Bad Request if validation fails or request is invalid
        /// - 500 Internal Server Error if processing fails
        /// </returns>
        /// <remarks>
        /// Sample request:
        /// POST /api/v1/job-levels
        /// {
        ///     "name": "Senior Level",
        ///     "description": "Level for senior positions",
        ///     "code": "SL-001"
        /// }
        /// </remarks>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateAsync([FromBody] CreateJobLevelRequest request)
        {
            _logger.LogLayerInfo("Creating new job level. Request: {@Request}", request);

            ValidationResult validationResult = await _createJobLevelRequestValidator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                _logger.LogLayerWarning("Job level creation validation failed. Errors: {@Errors}", validationResult.Errors);
                return OpenHRCoreApiResponseHelper.CreateValidationErrorResponse(validationResult);
            }

            try
            {
                var response = await _jobLevelService.CreateJobLevelAsync(request);
                if (!response.IsSuccess)
                {
                    _logger.LogLayerWarning("Job level creation failed. Error: {Error}", response.ErrorMessage ?? "Unknown error");
                    return OpenHRCoreApiResponseHelper.CreateFailureResponse(response);
                }

                _logger.LogLayerInfo("Job level created successfully. Response: {@Response}", response);
                return OpenHRCoreApiResponseHelper.CreateSuccessResponse(response, StatusCodes.Status201Created);
            }
            catch (Exception ex)
            {
                _logger.LogLayerError(ex, "Error creating job level. Request: {@Request}", request);
                return OpenHRCoreApiResponseHelper.CreateErrorResponse(ex);
            }
        }

        /// <summary>
        /// Retrieves all job levels.
        /// </summary>
        /// <returns>
        /// ActionResult containing:
        /// - 200 OK with list of all job levels
        /// - 500 Internal Server Error if retrieval fails
        /// </returns>
        /// <remarks>
        /// Sample request:
        /// GET /api/v1/job-levels
        /// </remarks>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllAsync()
        {
            _logger.LogLayerInfo("Retrieving all job levels");

            try
            {
                var response = await _jobLevelService.GetAllJobLevelsAsync();
                if (!response.IsSuccess)
                {
                    _logger.LogLayerWarning("Job levels retrieval failed. Error: {Error}", response.ErrorMessage ?? "Unknown error");
                    return OpenHRCoreApiResponseHelper.CreateFailureResponse(response);
                }

                _logger.LogLayerInfo("Job levels retrieved successfully. Count: {Count}", response.Data?.Count() ?? 0);
                return OpenHRCoreApiResponseHelper.CreateSuccessResponse(response);
            }
            catch (Exception ex)
            {
                _logger.LogLayerError(ex, "Error retrieving job levels");
                return OpenHRCoreApiResponseHelper.CreateErrorResponse(ex);
            }
        }

        /// <summary>
        /// Updates a specific job level.
        /// </summary>
        /// <param name="id">The unique identifier of the job level to update</param>
        /// <param name="request">The job level update request containing modified information</param>
        /// <returns>
        /// ActionResult containing:
        /// - 200 OK with updated job level details
        /// - 400 Bad Request if validation fails or ID mismatch
        /// - 404 Not Found if job level doesn't exist
        /// - 500 Internal Server Error if update fails
        /// </returns>
        /// <remarks>
        /// Sample request:
        /// PUT /api/v1/job-levels/{id}
        /// {
        ///     "id": "guid",
        ///     "name": "Updated Senior Level",
        ///     "description": "Updated level description",
        ///     "code": "USL-001"
        /// }
        /// </remarks>
        [HttpPut("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] UpdateJobLevelRequest request)
        {
            _logger.LogLayerInfo("Updating job level {Id}. Request: {@Request}", id, request);

            if (id != request.Id)
            {
                return BadRequest(_sharedLocalizer["IdMismatch"]);
            }

            try
            {
                var response = await _jobLevelService.UpdateJobLevelAsync(request);
                if (!response.IsSuccess)
                {
                    _logger.LogLayerWarning("Job level update failed. Error: {Error}", response.ErrorMessage ?? "Unknown error");
                    return OpenHRCoreApiResponseHelper.CreateFailureResponse(response);
                }

                _logger.LogLayerInfo("Job level {Id} updated successfully", id);
                return OpenHRCoreApiResponseHelper.CreateSuccessResponse(response);
            }
            catch (Exception ex)
            {
                _logger.LogLayerError(ex, "Error updating job level {Id}", id);
                return OpenHRCoreApiResponseHelper.CreateErrorResponse(ex);
            }
        }

        /// <summary>
        /// Deletes a specific job level.
        /// </summary>
        /// <param name="id">The unique identifier of the job level to delete</param>
        /// <returns>
        /// ActionResult containing:
        /// - 204 No Content on successful deletion
        /// - 404 Not Found if job level doesn't exist
        /// - 500 Internal Server Error if deletion fails
        /// </returns>
        /// <remarks>
        /// Sample request:
        /// DELETE /api/v1/job-levels/{id}
        /// </remarks>
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            _logger.LogLayerInfo("Deleting job level {Id}", id);

            try
            {
                var response = await _jobLevelService.DeleteJobLevelAsync(id);
                if (!response.IsSuccess)
                {
                    _logger.LogLayerWarning("Job level deletion failed. Error: {Error}", response.ErrorMessage ?? "Unknown error");
                    return OpenHRCoreApiResponseHelper.CreateFailureResponse(response);
                }

                _logger.LogLayerInfo("Job level {Id} deleted successfully", id);
                return OpenHRCoreApiResponseHelper.CreateSuccessResponse(response);
            }
            catch (Exception ex)
            {
                _logger.LogLayerError(ex, "Error deleting job level {Id}", id);
                return OpenHRCoreApiResponseHelper.CreateErrorResponse(ex);
            }
        }

        /// <summary>
        /// Retrieves a specific job level by ID.
        /// </summary>
        /// <param name="id">The unique identifier of the job level to retrieve</param>
        /// <returns>
        /// ActionResult containing:
        /// - 200 OK with the requested job level details
        /// - 404 Not Found if job level doesn't exist
        /// - 500 Internal Server Error if retrieval fails
        /// </returns>
        /// <remarks>
        /// Sample request:
        /// GET /api/v1/job-levels/{id}
        /// </remarks>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            _logger.LogLayerInfo("Retrieving job level {Id}", id);

            try
            {
                var response = await _jobLevelService.GetJobLevelByIdAsync(id);
                if (!response.IsSuccess)
                {
                    _logger.LogLayerWarning("Job level retrieval failed. Error: {Error}", response.ErrorMessage ?? "Unknown error");
                    return OpenHRCoreApiResponseHelper.CreateFailureResponse(response);
                }

                _logger.LogLayerInfo("Job level {Id} retrieved successfully", id);
                return OpenHRCoreApiResponseHelper.CreateSuccessResponse(response);
            }
            catch (Exception ex)
            {
                _logger.LogLayerError(ex, "Error retrieving job level {Id}", id);
                return OpenHRCoreApiResponseHelper.CreateErrorResponse(ex);
            }
        }
    }
}
