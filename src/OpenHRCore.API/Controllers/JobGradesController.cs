using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using OpenHRCore.API.Common;
using OpenHRCore.Application.DTOs.JobGrade;
using OpenHRCore.Application.Interfaces;
using OpenHRCore.SharedKernel.Utilities;

namespace OpenHRCore.API.Controllers
{
    /// <summary>
    /// Controller for managing job grades.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class JobGradesController : ControllerBase
    {
        private readonly IValidator<CreateJobGradeRequest> _createJobGradeRequestValidator;
        private readonly IValidator<UpdateJobGradeRequest> _updateJobGradeRequestValidator;
        private readonly IValidator<DeleteJobGradeRequest> _deleteJobGradeRequestValidator;
        private readonly IJobPositionService _jobPositionService;
        private readonly ILogger<JobGradesController> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="JobGradesController"/> class.
        /// </summary>
        /// <param name="createJobGradeRequestValidator">Validator for create job grade requests.</param>
        /// <param name="updateJobGradeRequestValidator">Validator for update job grade requests.</param>
        /// <param name="deleteJobGradeRequestValidator">Validator for delete job grade requests.</param>
        /// <param name="jobPositionService">Service for job position operations.</param>
        /// <param name="logger">Logger for the controller.</param>
        public JobGradesController(
            IValidator<CreateJobGradeRequest> createJobGradeRequestValidator,
            IValidator<UpdateJobGradeRequest> updateJobGradeRequestValidator,
            IValidator<DeleteJobGradeRequest> deleteJobGradeRequestValidator,
            IJobPositionService jobPositionService,
            ILogger<JobGradesController> logger)
        {
            _createJobGradeRequestValidator = createJobGradeRequestValidator;
            _updateJobGradeRequestValidator = updateJobGradeRequestValidator;
            _deleteJobGradeRequestValidator = deleteJobGradeRequestValidator;
            _jobPositionService = jobPositionService;
            _logger = logger;
        }

        /// <summary>
        /// Creates a new job grade.
        /// </summary>
        /// <param name="request">The create job grade request.</param>
        /// <returns>An action result containing the created job grade.</returns>
        [HttpPost]
        public async Task<IActionResult> CreateJobGradeAsync(CreateJobGradeRequest request)
        {
            _logger.LogApiInfo("CreateJobGradeAsync started. Request: {@Request}", request);

            ValidationResult validationResult = await _createJobGradeRequestValidator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                _logger.LogApiWarning("CreateJobGradeAsync validation failed. Request: {@Request}, Errors: {@Errors}", request, validationResult.Errors);
                return ApiResponseHelper.CreateValidationErrorResponse(validationResult);
            }

            try
            {
                var response = await _jobPositionService.CreateJobGradeAsync(request);

                if (!response.IsSuccess)
                {
                    _logger.LogApiWarning("CreateJobGradeAsync failed. Request: {@Request}, Errors: {@Errors}", request, new List<string> { response.ErrorMessage ?? "Unknown error" });
                    return ApiResponseHelper.CreateFailureResponse(response);
                }

                _logger.LogApiInfo("CreateJobGradeAsync succeeded. Created JobGrade: {@Response}", response);
                return ApiResponseHelper.CreateSuccessResponse(response);
            }
            catch (Exception ex)
            {
                _logger.LogApiError(ex, "Error in CreateJobGradeAsync. Request: {@Request}", request);
                return ApiResponseHelper.CreateErrorResponse(ex);
            }
        }

        /// <summary>
        /// Updates an existing job grade.
        /// </summary>
        /// <param name="id">The ID of the job grade to update.</param>
        /// <param name="request">The update job grade request.</param>
        /// <returns>An action result containing the updated job grade.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateJobGradeAsync(string id, UpdateJobGradeRequest request)
        {
            _logger.LogApiInfo("UpdateJobGradeAsync started. ID: {JobGradeId}, Request: {@Request}", id, request);

            ValidationResult validationResult = await _updateJobGradeRequestValidator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                _logger.LogApiWarning("UpdateJobGradeAsync validation failed. ID: {JobGradeId}, Request: {@Request}, Errors: {@Errors}", id, request, validationResult.Errors);
                return ApiResponseHelper.CreateValidationErrorResponse(validationResult);
            }

            try
            {
                var response = await _jobPositionService.UpdateJobGradeAsync(request);

                if (!response.IsSuccess)
                {
                    _logger.LogApiWarning("UpdateJobGradeAsync failed. ID: {JobGradeId}, Errors: {@Errors}", id, new List<string> { response.ErrorMessage ?? "Unknown error" });
                    return ApiResponseHelper.CreateFailureResponse(response);
                }

                _logger.LogApiInfo("UpdateJobGradeAsync succeeded. ID: {JobGradeId}, Updated Data: {@Response}", id, response);
                return ApiResponseHelper.CreateSuccessResponse(response);
            }
            catch (Exception ex)
            {
                _logger.LogApiError(ex, "Error in UpdateJobGradeAsync. ID: {JobGradeId}, Request: {@Request}", id, request);
                return ApiResponseHelper.CreateErrorResponse(ex);
            }
        }

        /// <summary>
        /// Deletes a job grade.
        /// </summary>
        /// <param name="request">The delete job grade request.</param>
        /// <returns>An action result indicating the CreateSuccessResponse of the deletion.</returns>
        [HttpDelete]
        public async Task<IActionResult> DeleteJobGradeAsync(DeleteJobGradeRequest request)
        {
            _logger.LogApiInfo("DeleteJobGradeAsync started. Request: {@Request}", request);

            ValidationResult validationResult = await _deleteJobGradeRequestValidator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                _logger.LogApiWarning("DeleteJobGradeAsync validation failed. Request: {@Request}, Errors: {@Errors}", request, validationResult.Errors);
                return ApiResponseHelper.CreateValidationErrorResponse(validationResult);
            }

            try
            {
                var response = await _jobPositionService.DeleteJobGradeAsync(request);

                if (!response.IsSuccess)
                {
                    _logger.LogApiWarning("DeleteJobGradeAsync failed. Request: {@Request}, Errors: {@Errors}", request, new List<string> { response.ErrorMessage ?? "Unknown error" });
                    return ApiResponseHelper.CreateFailureResponse(response);
                }

                _logger.LogApiInfo("DeleteJobGradeAsync succeeded. Request: {@Request}", request);
                return ApiResponseHelper.CreateSuccessResponse(response);
            }
            catch (Exception ex)
            {
                _logger.LogApiError(ex, "Error in DeleteJobGradeAsync. Request: {@Request}", request);
                return ApiResponseHelper.CreateErrorResponse(ex);
            }
        }

        /// <summary>
        /// Retrieves a job grade by its ID.
        /// </summary>
        /// <param name="id">The ID of the job grade to retrieve.</param>
        /// <returns>An action result containing the requested job grade.</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetJobGradeByIdAsync(string id)
        {
            _logger.LogApiInfo("GetJobGradeByIdAsync started. JobGradeId: {JobGradeId}", id);

            try
            {
                var response = await _jobPositionService.GetJobGradeByIdAsync(Guid.Parse(id));

                if (!response.IsSuccess)
                {
                    _logger.LogApiWarning("GetJobGradeByIdAsync failed. JobGradeId: {JobGradeId}, Errors: {@Errors}", id, new List<string> { response.ErrorMessage ?? "Unknown error" });
                    return ApiResponseHelper.CreateFailureResponse(response);
                }

                _logger.LogApiInfo("GetJobGradeByIdAsync succeeded. JobGradeId: {JobGradeId}, Data: {@Response}", id, response);
                return ApiResponseHelper.CreateSuccessResponse(response);
            }
            catch (Exception ex)
            {
                _logger.LogApiError(ex, "Error in GetJobGradeByIdAsync. JobGradeId: {JobGradeId}", id);
                return ApiResponseHelper.CreateErrorResponse(ex);
            }
        }

        /// <summary>
        /// Retrieves all job grades.
        /// </summary>
        /// <returns>An action result containing all job grades.</returns>
        [HttpGet]
        public async Task<IActionResult> GetAllJobGradesAsync()
        {
            _logger.LogApiInfo("GetAllJobGradesAsync started");

            try
            {
                var response = await _jobPositionService.GetAllJobGradesAsync();

                if (!response.IsSuccess)
                {
                    _logger.LogApiWarning("GetAllJobGradesAsync failed. Errors: {@Errors}", new List<string> { response.ErrorMessage ?? "Unknown error" });
                    return ApiResponseHelper.CreateFailureResponse(response);
                }

                _logger.LogApiInfo("GetAllJobGradesAsync succeeded. Data: {@Response}", response);
                return ApiResponseHelper.CreateSuccessResponse(response);
            }
            catch (Exception ex)
            {
                _logger.LogApiError(ex, "Error in GetAllJobGradesAsync");
                return ApiResponseHelper.CreateErrorResponse(ex);
            }
        }
    }
}
