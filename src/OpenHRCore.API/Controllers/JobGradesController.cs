using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using OpenHRCore.API.Common;
using OpenHRCore.Application.DTOs.JobGrade;
using OpenHRCore.Application.Interfaces;

namespace OpenHRCore.API.Controllers
{
    /// <summary>
    /// Controller for managing job grades in the system.
    /// Provides endpoints for CRUD operations on job grade entities.
    /// </summary>
    [ApiController]
    [Route("api/v1/job-grades")]
    [Produces("application/json")]
    public class JobGradesController : ControllerBase
    {
        private readonly IValidator<CreateJobGradeRequest> _createJobGradeRequestValidator;
        private readonly IValidator<UpdateJobGradeRequest> _updateJobGradeRequestValidator;
        private readonly IValidator<DeleteJobGradeRequest> _deleteJobGradeRequestValidator;
        private readonly IJobPositionService _jobPositionService;
        private readonly ILogger<JobGradesController> _logger;
        private readonly IStringLocalizer<SharedResource> _sharedLocalizer;

        /// <summary>
        /// Initializes a new instance of the JobGradesController.
        /// </summary>
        /// <param name="createJobGradeRequestValidator">Validator for job grade creation requests</param>
        /// <param name="updateJobGradeRequestValidator">Validator for job grade update requests</param>
        /// <param name="deleteJobGradeRequestValidator">Validator for job grade deletion requests</param>
        /// <param name="jobPositionService">Service for managing job positions and grades</param>
        /// <param name="logger">Logger instance for the controller</param>
        /// <param name="sharedLocalizer">Localizer for internationalization</param>
        public JobGradesController(
            IValidator<CreateJobGradeRequest> createJobGradeRequestValidator,
            IValidator<UpdateJobGradeRequest> updateJobGradeRequestValidator,
            IValidator<DeleteJobGradeRequest> deleteJobGradeRequestValidator,
            IJobPositionService jobPositionService,
            ILogger<JobGradesController> logger,
            IStringLocalizer<SharedResource> sharedLocalizer)
        {
            _createJobGradeRequestValidator = createJobGradeRequestValidator ?? throw new ArgumentNullException(nameof(createJobGradeRequestValidator));
            _updateJobGradeRequestValidator = updateJobGradeRequestValidator ?? throw new ArgumentNullException(nameof(updateJobGradeRequestValidator));
            _deleteJobGradeRequestValidator = deleteJobGradeRequestValidator ?? throw new ArgumentNullException(nameof(deleteJobGradeRequestValidator));
            _jobPositionService = jobPositionService ?? throw new ArgumentNullException(nameof(jobPositionService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _sharedLocalizer = sharedLocalizer ?? throw new ArgumentNullException(nameof(sharedLocalizer));
        }

        /// <summary>
        /// Creates a new job grade.
        /// </summary>
        /// <param name="request">The job grade creation request details</param>
        /// <returns>The created job grade information</returns>
        /// <response code="201">Job grade successfully created</response>
        /// <response code="400">Invalid request data</response>
        /// <response code="500">Internal server error</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateJobGradeAsync([FromBody] CreateJobGradeRequest request)
        {
            _logger.LogApiInfo("Creating new job grade. Request: {@Request}", request);

            ValidationResult validationResult = await _createJobGradeRequestValidator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                _logger.LogApiWarning("Job grade creation validation failed. Request: {@Request}, Errors: {@Errors}", request, validationResult.Errors);
                return OpenHRCoreApiResponseHelper.CreateValidationErrorResponse(validationResult);
            }

            try
            {
                var response = await _jobPositionService.CreateJobGradeAsync(request);

                if (!response.IsSuccess)
                {
                    _logger.LogApiWarning("Job grade creation failed. Request: {@Request}, Error: {Error}", request, response.ErrorMessage ?? "Unknown error");
                    return OpenHRCoreApiResponseHelper.CreateFailureResponse(response);
                }

                _logger.LogApiInfo("Job grade created successfully. Created grade: {@Response}", response);
                return OpenHRCoreApiResponseHelper.CreateSuccessResponse(response, StatusCodes.Status201Created);
            }
            catch (Exception ex)
            {
                _logger.LogApiError(ex, "Error creating job grade. Request: {@Request}", request);
                return OpenHRCoreApiResponseHelper.CreateErrorResponse(ex);
            }
        }

        /// <summary>
        /// Updates an existing job grade.
        /// </summary>
        /// <param name="id">The unique identifier of the job grade</param>
        /// <param name="request">The job grade update request details</param>
        /// <returns>The updated job grade information</returns>
        /// <response code="200">Job grade successfully updated</response>
        /// <response code="400">Invalid request data</response>
        /// <response code="404">Job grade not found</response>
        /// <response code="500">Internal server error</response>
        [HttpPut("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateJobGradeAsync(Guid id, [FromBody] UpdateJobGradeRequest request)
        {
            _logger.LogApiInfo("Updating job grade. ID: {JobGradeId}, Request: {@Request}", id, request);

            if (request.Id != id)
            {
                return OpenHRCoreApiResponseHelper.CreateValidationErrorResponse("ID in URL does not match ID in request body");
            }

            ValidationResult validationResult = await _updateJobGradeRequestValidator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                _logger.LogApiWarning("Job grade update validation failed. ID: {JobGradeId}, Request: {@Request}, Errors: {@Errors}", id, request, validationResult.Errors);
                return OpenHRCoreApiResponseHelper.CreateValidationErrorResponse(validationResult);
            }

            try
            {
                var response = await _jobPositionService.UpdateJobGradeAsync(request);

                if (!response.IsSuccess)
                {
                    _logger.LogApiWarning("Job grade update failed. ID: {JobGradeId}, Error: {Error}", id, response.ErrorMessage ?? "Unknown error");
                    return OpenHRCoreApiResponseHelper.CreateFailureResponse(response);
                }

                _logger.LogApiInfo("Job grade updated successfully. ID: {JobGradeId}, Updated Data: {@Response}", id, response);
                return OpenHRCoreApiResponseHelper.CreateSuccessResponse(response);
            }
            catch (Exception ex)
            {
                _logger.LogApiError(ex, "Error updating job grade. ID: {JobGradeId}, Request: {@Request}", id, request);
                return OpenHRCoreApiResponseHelper.CreateErrorResponse(ex);
            }
        }

        /// <summary>
        /// Deletes a job grade.
        /// </summary>
        /// <param name="id">The unique identifier of the job grade to delete</param>
        /// <returns>A response indicating the success of the deletion</returns>
        /// <response code="204">Job grade successfully deleted</response>
        /// <response code="404">Job grade not found</response>
        /// <response code="500">Internal server error</response>
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteJobGradeAsync(string id)
        {
            _logger.LogApiInfo("Deleting job grade. ID: {JobGradeId}", id);

            var request = new DeleteJobGradeRequest(id);
            ValidationResult validationResult = await _deleteJobGradeRequestValidator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                _logger.LogApiWarning("Job grade deletion validation failed. ID: {JobGradeId}, Errors: {@Errors}", id, validationResult.Errors);
                return OpenHRCoreApiResponseHelper.CreateValidationErrorResponse(validationResult);
            }

            try
            {
                var response = await _jobPositionService.DeleteJobGradeAsync(request);

                if (!response.IsSuccess)
                {
                    _logger.LogApiWarning("Job grade deletion failed. ID: {JobGradeId}, Error: {Error}", id, response.ErrorMessage ?? "Unknown error");
                    return OpenHRCoreApiResponseHelper.CreateFailureResponse(response);
                }

                _logger.LogApiInfo("Job grade deleted successfully. ID: {JobGradeId}", id);
                return OpenHRCoreApiResponseHelper.CreateSuccessResponse(response, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                _logger.LogApiError(ex, "Error deleting job grade. ID: {JobGradeId}", id);
                return OpenHRCoreApiResponseHelper.CreateErrorResponse(ex);
            }
        }

        /// <summary>
        /// Retrieves a specific job grade by its identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the job grade</param>
        /// <returns>The requested job grade information</returns>
        /// <response code="200">Job grade successfully retrieved</response>
        /// <response code="404">Job grade not found</response>
        /// <response code="500">Internal server error</response>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetJobGradeByIdAsync(Guid id)
        {
            _logger.LogApiInfo("Retrieving job grade. ID: {JobGradeId}", id);

            try
            {
                var response = await _jobPositionService.GetJobGradeByIdAsync(id);

                if (!response.IsSuccess)
                {
                    _logger.LogApiWarning("Job grade retrieval failed. ID: {JobGradeId}, Error: {Error}", id, response.ErrorMessage ?? "Unknown error");
                    return OpenHRCoreApiResponseHelper.CreateFailureResponse(response);
                }

                _logger.LogApiInfo("Job grade retrieved successfully. ID: {JobGradeId}, Data: {@Response}", id, response);
                return OpenHRCoreApiResponseHelper.CreateSuccessResponse(response);
            }
            catch (Exception ex)
            {
                _logger.LogApiError(ex, "Error retrieving job grade. ID: {JobGradeId}", id);
                return OpenHRCoreApiResponseHelper.CreateErrorResponse(ex);
            }
        }

        /// <summary>
        /// Retrieves all job grades.
        /// </summary>
        /// <returns>A list of all job grades</returns>
        /// <response code="200">Job grades successfully retrieved</response>
        /// <response code="500">Internal server error</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllJobGradesAsync()
        {
            _logger.LogApiInfo("Retrieving all job grades");

            try
            {
                var response = await _jobPositionService.GetAllJobGradesAsync();

                if (!response.IsSuccess)
                {
                    _logger.LogApiWarning("Job grades retrieval failed. Error: {Error}", response.ErrorMessage ?? "Unknown error");
                    return OpenHRCoreApiResponseHelper.CreateFailureResponse(response);
                }

                _logger.LogApiInfo("Job grades retrieved successfully. Count: {Count}", response.Data?.Count() ?? 0);
                return OpenHRCoreApiResponseHelper.CreateSuccessResponse(response);
            }
            catch (Exception ex)
            {
                _logger.LogApiError(ex, "Error retrieving all job grades");
                return OpenHRCoreApiResponseHelper.CreateErrorResponse(ex);
            }
        }
    }
}
