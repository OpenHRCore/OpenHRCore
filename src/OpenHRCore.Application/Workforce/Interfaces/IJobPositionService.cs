using OpenHRCore.Application.Workforce.DTOs.JobLevelDtos;
using OpenHRCore.Application.Workforce.DTOs.JobPositionDtos;

namespace OpenHRCore.Application.Workforce.Interfaces
{
    /// <summary>
    /// Service interface for managing job positions, providing CRUD operations.
    /// Handles creation, retrieval, update and deletion of job positions.
    /// </summary>
    public interface IJobPositionService
    {
        /// <summary>
        /// Creates a new job position in the system
        /// </summary>
        /// <param name="request">The job position creation request details</param>
        /// <returns>Response containing the created job position information</returns>
        Task<OpenHRCoreServiceResponse<GetJobPositionResponse>> CreateJobPositionAsync(CreateJobPositionRequest request);

        /// <summary>
        /// Updates an existing job position's information
        /// </summary>
        /// <param name="request">The job position update request details</param>
        /// <returns>Response containing the updated job position information</returns>
        Task<OpenHRCoreServiceResponse<GetJobPositionResponse>> UpdateJobPositionAsync(UpdateJobPositionRequest request);

        /// <summary>
        /// Deletes a job position from the system
        /// </summary>
        /// <param name="id">The unique identifier of the job position to delete</param>
        /// <returns>Response indicating the result of the deletion operation</returns>
        Task<OpenHRCoreServiceResponse<GetJobPositionResponse>> DeleteJobPositionAsync(Guid id);

        /// <summary>
        /// Retrieves a specific job position by its unique identifier
        /// </summary>
        /// <param name="id">The unique identifier of the job position to retrieve</param>
        /// <returns>Response containing the requested job position information</returns>
        Task<OpenHRCoreServiceResponse<GetJobPositionResponse>> GetJobPositionByIdAsync(Guid id);

        /// <summary>
        /// Retrieves all job positions from the system
        /// </summary>
        /// <returns>Response containing a collection of all job positions</returns>
        Task<OpenHRCoreServiceResponse<IEnumerable<GetJobPositionResponse>>> GetAllJobPositionsAsync();
    }
}
