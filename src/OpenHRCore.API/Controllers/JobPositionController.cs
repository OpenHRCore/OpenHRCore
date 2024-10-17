using Microsoft.AspNetCore.Mvc;
using OpenHRCore.Application.DTOs.JobGrade;
using OpenHRCore.Application.Interfaces;
using OpenHRCore.SharedKernel.Utilities;

namespace OpenHRCore.API.Controllers
{
    /// <summary>
    /// Controller for managing job positions.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class JobPositionController : ControllerBase
    {
        private readonly IJobPositionService _jobPositionService;
        private readonly ILogger<JobPositionController> _logger;

        public JobPositionController(IJobPositionService jobPositionService, ILogger<JobPositionController> logger)
        {
            _jobPositionService = jobPositionService;
            _logger = logger;
        }

        /// <summary>
        /// Creates a new job grade.
        /// </summary>
        /// <param name="request">The request to create a job grade.</param>
        /// <returns>The created job grade.</returns>
        [HttpPost]
        public async Task<IActionResult> CreateJobGradeAsync(CreateJobGradeRequest request)
        {
            _logger.LogApiInfo("CreateJobGradeAsync started. Request: {@Request}", request);

            try
            {
                var response = await _jobPositionService.CreateJobGradeAsync(request);

                if (!response.IsSuccess)
                {
                    _logger.LogApiWarning("CreateJobGradeAsync failed. Request: {@Request}, Errors: {@Errors}", request, response.ErrorMessage);
                    return BadRequest(response);
                }

                _logger.LogApiInfo("CreateJobGradeAsync succeeded. Created JobGrade: {@Response}", response);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogApiError(ex, "Error in CreateJobGradeAsync. Request: {@Request}", request);
                return StatusCode(500, "An internal error occurred.");
            }
        }

        /// <summary>
        /// Updates an existing job grade.
        /// </summary>
        /// <param name="id">The ID of the job grade to update.</param>
        /// <param name="request">The request to update the job grade.</param>
        /// <returns>The updated job grade.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateJobGradeAsync(string id, UpdateJobGradeRequest request)
        {
            _logger.LogApiInfo("UpdateJobGradeAsync started. ID: {JobGradeId}, Request: {@Request}", id, request);

            try
            {
                var response = await _jobPositionService.UpdateJobGradeAsync(request);

                if (!response.IsSuccess)
                {
                    _logger.LogApiWarning("UpdateJobGradeAsync failed. ID: {JobGradeId}, Errors: {@Errors}", id, response.ErrorMessage);
                    return BadRequest(response);
                }

                _logger.LogApiInfo("UpdateJobGradeAsync succeeded. ID: {JobGradeId}, Updated Data: {@Response}", id, response);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogApiError(ex, "Error in UpdateJobGradeAsync. ID: {JobGradeId}, Request: {@Request}", id, request);
                return StatusCode(500, "An internal error occurred.");
            }
        }

        /// <summary>
        /// Deletes a job grade.
        /// </summary>
        /// <param name="id">The ID of the job grade to delete.</param>
        /// <returns>The deleted job grade.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteJobGradeAsync(string id)
        {
            _logger.LogApiInfo("DeleteJobGradeAsync started. JobGradeId: {JobGradeId}", id);

            try
            {
                var request = new DeleteJobGradeRequest(Guid.Parse(id));
                var response = await _jobPositionService.DeleteJobGradeAsync(request);

                if (!response.IsSuccess)
                {
                    _logger.LogApiWarning("DeleteJobGradeAsync failed. JobGradeId: {JobGradeId}, Errors: {@Errors}", id, response.ErrorMessage);
                    return BadRequest(response);
                }

                _logger.LogApiInfo("DeleteJobGradeAsync succeeded. JobGradeId: {JobGradeId}", id);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogApiError(ex, "Error in DeleteJobGradeAsync. JobGradeId: {JobGradeId}", id);
                return StatusCode(500, "An internal error occurred.");
            }
        }

        /// <summary>
        /// Gets a job grade by ID.
        /// </summary>
        /// <param name="id">The ID of the job grade to retrieve.</param>
        /// <returns>The job grade.</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetJobGradeByIdAsync(string id)
        {
            _logger.LogApiInfo("GetJobGradeByIdAsync started. JobGradeId: {JobGradeId}", id);

            try
            {
                var response = await _jobPositionService.GetJobGradeByIdAsync(Guid.Parse(id));

                if (!response.IsSuccess)
                {
                    _logger.LogApiWarning("GetJobGradeByIdAsync failed. JobGradeId: {JobGradeId}, Errors: {@Errors}", id, response.ErrorMessage);
                    return BadRequest(response);
                }

                _logger.LogApiInfo("GetJobGradeByIdAsync succeeded. JobGradeId: {JobGradeId}, Data: {@Response}", id, response);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogApiError(ex, "Error in GetJobGradeByIdAsync. JobGradeId: {JobGradeId}", id);
                return StatusCode(500, "An internal error occurred.");
            }
        }

        /// <summary>
        /// Gets all job grades.
        /// </summary>
        /// <returns>The list of job grades.</returns>
        [HttpGet]
        public async Task<IActionResult> GetAllJobGradesAsync()
        {
            _logger.LogApiInfo("GetAllJobGradesAsync started");

            try
            {
                var response = await _jobPositionService.GetAllJobGradesAsync();

                if (!response.IsSuccess)
                {
                    _logger.LogApiWarning("GetAllJobGradesAsync failed. Errors: {@Errors}", response.ErrorMessage);
                    return BadRequest(response);
                }

                _logger.LogApiInfo("GetAllJobGradesAsync succeeded. Data: {@Response}", response);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogApiError(ex, "Error in GetAllJobGradesAsync");
                return StatusCode(500, "An internal error occurred.");
            }
        }
    }
}
