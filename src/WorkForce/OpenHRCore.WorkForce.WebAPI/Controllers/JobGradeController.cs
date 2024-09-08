using Microsoft.AspNetCore.Mvc;
using OpenHRCore.WorkForce.Application.DTOs.JobGrade;
using OpenHRCore.WorkForce.Application.Interfaces;

namespace OpenHRCore.WorkForce.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobGradeController : ControllerBase
    {
        private readonly IJobPositionService _jobPositionService;

        public JobGradeController(IJobPositionService jobPositionService)
        {
            _jobPositionService = jobPositionService;
        }

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
    }
}
