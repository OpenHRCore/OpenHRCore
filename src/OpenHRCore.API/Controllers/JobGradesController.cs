using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using OpenHRCore.API.Common;
using OpenHRCore.Application.Workforce.DTOs.JobGradeDtos;
using OpenHRCore.Application.Workforce.Interfaces;

namespace OpenHRCore.API.Controllers
{
    /// <summary>
    /// REST API controller for managing job grades in the system.
    /// Provides endpoints for CRUD operations on job grade resources.
    /// </summary>
    [ApiController]
    [Route("api/v1/job-grades")]
    [Produces("application/json")]
    public class JobGradesController : ControllerBase
    {
        private readonly IValidator<CreateJobGradeRequest> _createJobGradeValidator;
        private readonly IJobGradeService _jobGradeService;
        private readonly ILogger<JobGradesController> _logger;
        private readonly IStringLocalizer<SharedResource> _localizer;

        /// <summary>
        /// Initializes a new instance of the JobGradesController.
        /// </summary>
        /// <param name="createJobGradeValidator">Validator for job grade creation requests</param>
        /// <param name="jobGradeService">Service for job grade operations</param>
        /// <param name="logger">Logger for controller diagnostics</param>
        /// <param name="localizer">Localizer for internationalization</param>
        /// <exception cref="ArgumentNullException">Thrown when any required dependency is null</exception>
        public JobGradesController(
            IValidator<CreateJobGradeRequest> createJobGradeValidator,
            IJobGradeService jobGradeService,
            ILogger<JobGradesController> logger,
            IStringLocalizer<SharedResource> localizer)
        {
            _createJobGradeValidator = createJobGradeValidator ?? throw new ArgumentNullException(nameof(createJobGradeValidator));
            _jobGradeService = jobGradeService ?? throw new ArgumentNullException(nameof(jobGradeService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _localizer = localizer ?? throw new ArgumentNullException(nameof(localizer));
        }

        /// <summary>
        /// Creates a new job grade.
        /// </summary>
        /// <param name="request">The job grade creation request containing required information</param>
        /// <returns>
        /// ActionResult containing:
        /// - 201 Created with the newly created job grade details
        /// - 400 Bad Request if validation fails or request is invalid
        /// - 500 Internal Server Error if processing fails
        /// </returns>
        /// <remarks>
        /// Sample request:
        /// POST /api/v1/job-grades
        /// {
        ///     "name": "Senior Grade",
        ///     "description": "Grade level for senior positions",
        ///     "code": "SG-001"
        /// }
        /// </remarks>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateAsync([FromBody] CreateJobGradeRequest request)
        {
            _logger.LogLayerInfo("Creating new job grade. Request: {@Request}", request);

            ValidationResult validationResult = await _createJobGradeValidator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                _logger.LogLayerWarning("Job grade creation validation failed. Errors: {@Errors}", validationResult.Errors);
                return OpenHRCoreApiResponseHelper.CreateValidationErrorResponse(validationResult);
            }

            try
            {
                var response = await _jobGradeService.CreateJobGradeAsync(request);
                if (!response.IsSuccess)
                {
                    _logger.LogLayerWarning("Job grade creation failed. Error: {Error}", response.ErrorMessage ?? "Unknown error");
                    return OpenHRCoreApiResponseHelper.CreateFailureResponse(response);
                }

                _logger.LogLayerInfo("Job grade created successfully. Response: {@Response}", response);
                return OpenHRCoreApiResponseHelper.CreateSuccessResponse(response, StatusCodes.Status201Created);
            }
            catch (Exception ex)
            {
                _logger.LogLayerError(ex, "Error creating job grade. Request: {@Request}", request);
                return OpenHRCoreApiResponseHelper.CreateErrorResponse(ex);
            }
        }

        /// <summary>
        /// Retrieves all job grades from the system.
        /// </summary>
        /// <returns>
        /// ActionResult containing:
        /// - 200 OK with list of all job grades
        /// - 500 Internal Server Error if retrieval fails
        /// </returns>
        /// <remarks>
        /// Sample request:
        /// GET /api/v1/job-grades
        /// </remarks>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllAsync()
        {
            _logger.LogLayerInfo("Retrieving all job grades");

            try
            {
                var response = await _jobGradeService.GetAllJobGradesAsync();
                if (!response.IsSuccess)
                {
                    _logger.LogLayerWarning("Job grades retrieval failed. Error: {Error}", response.ErrorMessage ?? "Unknown error");
                    return OpenHRCoreApiResponseHelper.CreateFailureResponse(response);
                }

                _logger.LogLayerInfo("Retrieved {Count} job grades successfully", response.Data?.Count() ?? 0);
                return OpenHRCoreApiResponseHelper.CreateSuccessResponse(response);
            }
            catch (Exception ex)
            {
                _logger.LogLayerError(ex, "Error retrieving job grades");
                return OpenHRCoreApiResponseHelper.CreateErrorResponse(ex);
            }
        }

        /// <summary>
        /// Updates an existing job grade by ID.
        /// </summary>
        /// <param name="id">The unique identifier of the job grade to update</param>
        /// <param name="request">The update request containing modified job grade details</param>
        /// <returns>
        /// ActionResult containing:
        /// - 200 OK with updated job grade details
        /// - 400 Bad Request if ID mismatch or validation fails
        /// - 404 Not Found if job grade doesn't exist
        /// - 500 Internal Server Error if update fails
        /// </returns>
        /// <remarks>
        /// Sample request:
        /// PUT /api/v1/job-grades/{id}
        /// {
        ///     "id": "guid",
        ///     "name": "Updated Grade",
        ///     "description": "Updated description",
        ///     "code": "UG-001"
        /// }
        /// </remarks>
        [HttpPut("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] UpdateJobGradeRequest request)
        {
            _logger.LogLayerInfo("Updating job grade {Id}. Request: {@Request}", id, request);

            if (id != request.Id)
            {
                _logger.LogLayerWarning("ID mismatch in update request. Path ID: {PathId}, Request ID: {RequestId}", id, request.Id);
                return BadRequest(_localizer["IdMismatch"]);
            }

            try
            {
                var response = await _jobGradeService.UpdateJobGradeAsync(request);
                if (!response.IsSuccess)
                {
                    _logger.LogLayerWarning("Job grade update failed. Error: {Error}", response.ErrorMessage ?? "Unknown error");
                    return OpenHRCoreApiResponseHelper.CreateFailureResponse(response);
                }

                _logger.LogLayerInfo("Job grade {Id} updated successfully", id);
                return OpenHRCoreApiResponseHelper.CreateSuccessResponse(response);
            }
            catch (Exception ex)
            {
                _logger.LogLayerError(ex, "Error updating job grade {Id}", id);
                return OpenHRCoreApiResponseHelper.CreateErrorResponse(ex);
            }
        }

        /// <summary>
        /// Deletes a specific job grade by ID.
        /// </summary>
        /// <param name="id">The unique identifier of the job grade to delete</param>
        /// <returns>
        /// ActionResult containing:
        /// - 204 No Content on successful deletion
        /// - 404 Not Found if job grade doesn't exist
        /// - 500 Internal Server Error if deletion fails
        /// </returns>
        /// <remarks>
        /// Sample request:
        /// DELETE /api/v1/job-grades/{id}
        /// </remarks>
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            _logger.LogLayerInfo("Deleting job grade {Id}", id);

            try
            {
                var response = await _jobGradeService.DeleteJobGradeAsync(id);
                if (!response.IsSuccess)
                {
                    _logger.LogLayerWarning("Job grade deletion failed. Error: {Error}", response.ErrorMessage ?? "Unknown error");
                    return OpenHRCoreApiResponseHelper.CreateFailureResponse(response);
                }

                _logger.LogLayerInfo("Job grade {Id} deleted successfully", id);
                return OpenHRCoreApiResponseHelper.CreateSuccessResponse(response);
            }
            catch (Exception ex)
            {
                _logger.LogLayerError(ex, "Error deleting job grade {Id}", id);
                return OpenHRCoreApiResponseHelper.CreateErrorResponse(ex);
            }
        }

        /// <summary>
        /// Retrieves a specific job grade by ID.
        /// </summary>
        /// <param name="id">The unique identifier of the job grade to retrieve</param>
        /// <returns>
        /// ActionResult containing:
        /// - 200 OK with the requested job grade details
        /// - 404 Not Found if job grade doesn't exist
        /// - 500 Internal Server Error if retrieval fails
        /// </returns>
        /// <remarks>
        /// Sample request:
        /// GET /api/v1/job-grades/{id}
        /// </remarks>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            _logger.LogLayerInfo("Retrieving job grade {Id}", id);

            try
            {
                var response = await _jobGradeService.GetJobGradeByIdAsync(id);
                if (!response.IsSuccess)
                {
                    _logger.LogLayerWarning("Job grade retrieval failed. Error: {Error}", response.ErrorMessage ?? "Unknown error");
                    return OpenHRCoreApiResponseHelper.CreateFailureResponse(response);
                }

                _logger.LogLayerInfo("Job grade {Id} retrieved successfully", id);
                return OpenHRCoreApiResponseHelper.CreateSuccessResponse(response);
            }
            catch (Exception ex)
            {
                _logger.LogLayerError(ex, "Error retrieving job grade {Id}", id);
                return OpenHRCoreApiResponseHelper.CreateErrorResponse(ex);
            }
        }
    }
}
