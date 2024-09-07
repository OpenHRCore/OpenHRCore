using OpenHRCore.WorkForce.Application.DTOs.JobGrade;

namespace OpenHRCore.WorkForce.Application.Interfaces
{
    /// <summary>
    /// Defines the contract for job position-related operations.
    /// </summary>
    public interface IJobPositionService
    {
        /// <summary>
        /// Asynchronously creates a new job grade.
        /// </summary>
        /// <param name="request">The request containing the details of the job grade to be created.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the response of the service operation.</returns>
        Task<OpenHRCoreServiceResponse<CreateJobGradeResponse>> CreateJobGradeAsync(CreateJobGradeRequest request);
    }
}
