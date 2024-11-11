using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using OpenHRCore.API.Common;
using OpenHRCore.Application.Workforce.DTOs.JobPositionDtos;
using OpenHRCore.Application.Workforce.Interfaces;

namespace OpenHRCore.API.Controllers
{
    /// <summary>
    /// REST API controller for managing job positions in the system.
    /// Provides endpoints for CRUD operations on job position resources.
    /// </summary>
    [ApiController]
    [Route("api/v1/job-positions")]
    [Produces("application/json")]
    public class JobPositionsController : ControllerBase
    {
        private readonly IValidator<CreateJobPositionRequest> _createJobPositionRequestValidator;
        private readonly IJobPositionService _jobPositionService;
        private readonly ILogger<JobPositionsController> _logger;
        private readonly IStringLocalizer<SharedResource> _sharedLocalizer;

        /// <summary>
        /// Initializes a new instance of the JobPositionsController.
        /// </summary>
        /// <param name="createJobPositionRequestValidator">Validator for job position creation requests</param>
        /// <param name="jobPositionService">Service for job position operations</param>
        /// <param name="logger">Logger for controller diagnostics</param>
        /// <param name="sharedLocalizer">Localizer for internationalization</param>
        /// <exception cref="ArgumentNullException">Thrown when any required dependency is null</exception>
        public JobPositionsController(
            IValidator<CreateJobPositionRequest> createJobPositionRequestValidator,
            IJobPositionService jobPositionService,
            ILogger<JobPositionsController> logger,
            IStringLocalizer<SharedResource> sharedLocalizer)
        {
            _createJobPositionRequestValidator = createJobPositionRequestValidator ?? throw new ArgumentNullException(nameof(createJobPositionRequestValidator));
            _jobPositionService = jobPositionService ?? throw new ArgumentNullException(nameof(jobPositionService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _sharedLocalizer = sharedLocalizer ?? throw new ArgumentNullException(nameof(sharedLocalizer));
        }

        /// <summary>
        /// Creates a new job position.
        /// </summary>
        /// <param name="request">The job position creation request containing required information</param>
        /// <returns>
        /// ActionResult containing:
        /// - 201 Created with the newly created job position details
        /// - 400 Bad Request if validation fails or request is invalid
        /// - 500 Internal Server Error if processing fails
        /// </returns>
        /// <remarks>
        /// Sample request:
        /// POST /api/v1/job-positions
        /// {
        ///     "title": "Software Engineer",
        ///     "description": "Senior software engineering position",
        ///     "code": "SE-001",
        ///     "departmentId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        ///     "jobGradeId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        ///     "jobLevelId": "3fa85f64-5717-4562-b3fc-2c963f66afa6"
        /// }
        /// </remarks>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateAsync([FromBody] CreateJobPositionRequest request)
        {
            _logger.LogLayerInfo("Creating new job position. Request: {@Request}", request);

            ValidationResult validationResult = await _createJobPositionRequestValidator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                _logger.LogLayerWarning("Job position creation validation failed. Errors: {@Errors}", validationResult.Errors);
                return OpenHRCoreApiResponseHelper.CreateValidationErrorResponse(validationResult);
            }

            try
            {
                var response = await _jobPositionService.CreateJobPositionAsync(request);
                if (!response.IsSuccess)
                {
                    _logger.LogLayerWarning("Job position creation failed. Error: {Error}", response.ErrorMessage ?? "Unknown error");
                    return OpenHRCoreApiResponseHelper.CreateFailureResponse(response);
                }

                _logger.LogLayerInfo("Job position created successfully");
                return OpenHRCoreApiResponseHelper.CreateSuccessResponse(response, StatusCodes.Status201Created);
            }
            catch (Exception ex)
            {
                _logger.LogLayerError(ex, "Error creating job position");
                return OpenHRCoreApiResponseHelper.CreateErrorResponse(ex);
            }
        }

        /// <summary>
        /// Retrieves all job positions.
        /// </summary>
        /// <returns>
        /// ActionResult containing:
        /// - 200 OK with list of all job positions
        /// - 500 Internal Server Error if retrieval fails
        /// </returns>
        /// <remarks>
        /// Sample request:
        /// GET /api/v1/job-positions
        /// </remarks>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllAsync()
        {
            _logger.LogLayerInfo("Retrieving all job positions");

            try
            {
                var response = await _jobPositionService.GetAllJobPositionsAsync();
                if (!response.IsSuccess)
                {
                    _logger.LogLayerWarning("Job positions retrieval failed. Error: {Error}", response.ErrorMessage ?? "Unknown error");
                    return OpenHRCoreApiResponseHelper.CreateFailureResponse(response);
                }

                _logger.LogLayerInfo("Job positions retrieved successfully. Count: {Count}", response.Data?.Count() ?? 0);
                return OpenHRCoreApiResponseHelper.CreateSuccessResponse(response);
            }
            catch (Exception ex)
            {
                _logger.LogLayerError(ex, "Error retrieving job positions");
                return OpenHRCoreApiResponseHelper.CreateErrorResponse(ex);
            }
        }

        /// <summary>
        /// Updates a specific job position.
        /// </summary>
        /// <param name="id">The unique identifier of the job position to update</param>
        /// <param name="request">The job position update request containing modified information</param>
        /// <returns>
        /// ActionResult containing:
        /// - 200 OK with the updated job position details
        /// - 400 Bad Request if validation fails or request is invalid
        /// - 404 Not Found if job position doesn't exist
        /// - 500 Internal Server Error if update fails
        /// </returns>
        /// <remarks>
        /// Sample request:
        /// PUT /api/v1/job-positions/{id}
        /// {
        ///     "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        ///     "title": "Senior Software Engineer",
        ///     "description": "Updated senior engineering position",
        ///     "code": "SSE-001",
        ///     "departmentId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        ///     "jobGradeId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        ///     "jobLevelId": "3fa85f64-5717-4562-b3fc-2c963f66afa6"
        /// }
        /// </remarks>
        [HttpPut("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] UpdateJobPositionRequest request)
        {
            _logger.LogLayerInfo("Updating job position {Id}", id);

            if (id != request.Id)
            {
                return BadRequest(_sharedLocalizer["IdMismatch"]);
            }

            try
            {
                var response = await _jobPositionService.UpdateJobPositionAsync(request);
                if (!response.IsSuccess)
                {
                    _logger.LogLayerWarning("Job position update failed. Error: {Error}", response.ErrorMessage ?? "Unknown error");
                    return OpenHRCoreApiResponseHelper.CreateFailureResponse(response);
                }

                _logger.LogLayerInfo("Job position {Id} updated successfully", id);
                return OpenHRCoreApiResponseHelper.CreateSuccessResponse(response);
            }
            catch (Exception ex)
            {
                _logger.LogLayerError(ex, "Error updating job position {Id}", id);
                return OpenHRCoreApiResponseHelper.CreateErrorResponse(ex);
            }
        }

        /// <summary>
        /// Deletes a specific job position.
        /// </summary>
        /// <param name="id">The unique identifier of the job position to delete</param>
        /// <returns>
        /// ActionResult containing:
        /// - 204 No Content on successful deletion
        /// - 404 Not Found if job position doesn't exist
        /// - 500 Internal Server Error if deletion fails
        /// </returns>
        /// <remarks>
        /// Sample request:
        /// DELETE /api/v1/job-positions/{id}
        /// </remarks>
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            _logger.LogLayerInfo("Deleting job position {Id}", id);

            try
            {
                var response = await _jobPositionService.DeleteJobPositionAsync(id);
                if (!response.IsSuccess)
                {
                    _logger.LogLayerWarning("Job position deletion failed. Error: {Error}", response.ErrorMessage ?? "Unknown error");
                    return OpenHRCoreApiResponseHelper.CreateFailureResponse(response);
                }

                _logger.LogLayerInfo("Job position {Id} deleted successfully", id);
                return OpenHRCoreApiResponseHelper.CreateSuccessResponse(response);
            }
            catch (Exception ex)
            {
                _logger.LogLayerError(ex, "Error deleting job position {Id}", id);
                return OpenHRCoreApiResponseHelper.CreateErrorResponse(ex);
            }
        }

        /// <summary>
        /// Retrieves a specific job position by ID.
        /// </summary>
        /// <param name="id">The unique identifier of the job position to retrieve</param>
        /// <returns>
        /// ActionResult containing:
        /// - 200 OK with the requested job position details
        /// - 404 Not Found if job position doesn't exist
        /// - 500 Internal Server Error if retrieval fails
        /// </returns>
        /// <remarks>
        /// Sample request:
        /// GET /api/v1/job-positions/{id}
        /// </remarks>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            _logger.LogLayerInfo("Retrieving job position {Id}", id);

            try
            {
                var response = await _jobPositionService.GetJobPositionByIdAsync(id);
                if (!response.IsSuccess)
                {
                    _logger.LogLayerWarning("Job position retrieval failed. Error: {Error}", response.ErrorMessage ?? "Unknown error");
                    return OpenHRCoreApiResponseHelper.CreateFailureResponse(response);
                }

                _logger.LogLayerInfo("Job position {Id} retrieved successfully", id);
                return OpenHRCoreApiResponseHelper.CreateSuccessResponse(response);
            }
            catch (Exception ex)
            {
                _logger.LogLayerError(ex, "Error retrieving job position {Id}", id);
                return OpenHRCoreApiResponseHelper.CreateErrorResponse(ex);
            }
        }
    }
}
