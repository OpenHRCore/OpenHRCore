using OpenHRCore.Application.Workforce.DTOs.JobLevelDtos;

namespace OpenHRCore.Application.Workforce.Interfaces
{
    /// <summary>
    /// Service interface for managing job levels, providing CRUD operations.
    /// Handles creation, retrieval, update and deletion of job levels.
    /// </summary>
    public interface IJobLevelService
    {
        /// <summary>
        /// Creates a new job level in the system
        /// </summary>
        /// <param name="request">The job level creation request details</param>
        /// <returns>Response containing the created job level information</returns>
        Task<OpenHRCoreServiceResponse<GetJobLevelResponse>> CreateJobLevelAsync(CreateJobLevelRequest request);

        /// <summary>
        /// Updates an existing job level's information
        /// </summary>
        /// <param name="request">The job level update request details</param>
        /// <returns>Response containing the updated job level information</returns>
        Task<OpenHRCoreServiceResponse<GetJobLevelResponse>> UpdateJobLevelAsync(UpdateJobLevelRequest request);

        /// <summary>
        /// Deletes a job level from the system
        /// </summary>
        /// <param name="id">The unique identifier of the job level to delete</param>
        /// <returns>Response indicating the result of the deletion operation</returns>
        Task<OpenHRCoreServiceResponse<GetJobLevelResponse>> DeleteJobLevelAsync(Guid id);

        /// <summary>
        /// Retrieves a specific job level by its unique identifier
        /// </summary>
        /// <param name="id">The unique identifier of the job level to retrieve</param>
        /// <returns>Response containing the requested job level information</returns>
        Task<OpenHRCoreServiceResponse<GetJobLevelResponse>> GetJobLevelByIdAsync(Guid id);

        /// <summary>
        /// Retrieves all job levels from the system
        /// </summary>
        /// <returns>Response containing a collection of all job levels</returns>
        Task<OpenHRCoreServiceResponse<IEnumerable<GetJobLevelResponse>>> GetAllJobLevelsAsync();
    }
}
