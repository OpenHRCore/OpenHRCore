using OpenHRCore.Application.Workforce.DTOs.JobGradeDtos;

namespace OpenHRCore.Application.Workforce.Interfaces
{
    /// <summary>
    /// Service interface for managing job grades, providing CRUD operations.
    /// Handles creation, retrieval, update and deletion of job grades.
    /// </summary>
    public interface IJobGradeService
    {
        /// <summary>
        /// Creates a new job grade in the system
        /// </summary>
        /// <param name="request">The job grade creation request details</param>
        /// <returns>Response containing the created job grade information</returns>
        Task<OpenHRCoreServiceResponse<GetJobGradeResponse>> CreateJobGradeAsync(CreateJobGradeRequest request);

        /// <summary>
        /// Updates an existing job grade's information
        /// </summary>
        /// <param name="request">The job grade update request details</param>
        /// <returns>Response containing the updated job grade information</returns>
        Task<OpenHRCoreServiceResponse<GetJobGradeResponse>> UpdateJobGradeAsync(UpdateJobGradeRequest request);

        /// <summary>
        /// Deletes a job grade from the system
        /// </summary>
        /// <param name="id">The unique identifier of the job grade to delete</param>
        /// <returns>Response indicating the result of the deletion operation</returns>
        Task<OpenHRCoreServiceResponse<GetJobGradeResponse>> DeleteJobGradeAsync(Guid id);

        /// <summary>
        /// Retrieves a specific job grade by its unique identifier
        /// </summary>
        /// <param name="id">The unique identifier of the job grade to retrieve</param>
        /// <returns>Response containing the requested job grade information</returns>
        Task<OpenHRCoreServiceResponse<GetJobGradeResponse>> GetJobGradeByIdAsync(Guid id);

        /// <summary>
        /// Retrieves all job grades from the system
        /// </summary>
        /// <returns>Response containing a collection of all job grades</returns>
        Task<OpenHRCoreServiceResponse<IEnumerable<GetJobGradeResponse>>> GetAllJobGradesAsync();
    }
}
