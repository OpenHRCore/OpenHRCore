using Microsoft.AspNetCore.Mvc;
using OpenHRCore.Application.DTOs.JobGrade;
using OpenHRCore.Application.Interfaces;

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

        /// <summary>
        /// Initializes a new instance of the <see cref="JobPositionController"/> class.
        /// </summary>
        /// <param name="jobPositionService">The job position service.</param>
        public JobPositionController(IJobPositionService jobPositionService)
        {
            _jobPositionService = jobPositionService;
        }

        /// <summary>
        /// Creates a new job grade.
        /// </summary>
        /// <param name="request">The request to create a job grade.</param>
        /// <returns>The created job grade.</returns>
        [HttpPost]
        public async Task<IActionResult> CreateJobGradeAsync(CreateJobGradeRequest request)
        {
            var response = await _jobPositionService.CreateJobGradeAsync(request);

            if (!response.IsSuccess)
            {
                return BadRequest(response);
            }

            return Ok(response);
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
            var response = await _jobPositionService.UpdateJobGradeAsync(request);

            if (!response.IsSuccess)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        /// <summary>
        /// Deletes a job grade.
        /// </summary>
        /// <param name="id">The ID of the job grade to delete.</param>
        /// <returns>The deleted job grade.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteJobGradeAsync(string id)
        {
            var request = new DeleteJobGradeRequest(Guid.Parse(id));
            var response = await _jobPositionService.DeleteJobGradeAsync(request);

            if (!response.IsSuccess)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        /// <summary>
        /// Gets a job grade by ID.
        /// </summary>
        /// <param name="id">The ID of the job grade to retrieve.</param>
        /// <returns>The job grade.</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetJobGradeByIdAsync(string id)
        {
            var response = await _jobPositionService.GetJobGradeByIdAsync(Guid.Parse(id));

            if (!response.IsSuccess)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        /// <summary>
        /// Gets all job grades.
        /// </summary>
        /// <returns>The list of job grades.</returns>
        [HttpGet]
        public async Task<IActionResult> GetAllJobGradesAsync()
        {
            var response = await _jobPositionService.GetAllJobGradesAsync();

            if (!response.IsSuccess)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }
    }
}
