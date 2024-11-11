using AutoMapper;
using Microsoft.Extensions.Logging;
using OpenHRCore.Application.UnitOfWork;
using OpenHRCore.Application.Workforce.DTOs.JobLevelDtos;
using OpenHRCore.Application.Workforce.Interfaces;
using OpenHRCore.Domain.Workforce.Entities;
using OpenHRCore.Domain.Workforce.Interfaces;
using OpenHRCore.SharedKernel.Utilities;

namespace OpenHRCore.Application.Workforce.Services
{
    /// <summary>
    /// Service responsible for managing job levels in the system.
    /// Implements CRUD operations and business logic for job level entities.
    /// </summary>
    public class JobLevelService : IJobLevelService
    {
        private readonly IJobLevelRepository _jobLevelRepository;
        private readonly IOpenHRCoreUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<JobLevelService> _logger; // Fixed logger type to match service

        /// <summary>
        /// Initializes a new instance of the JobLevelService class.
        /// </summary>
        /// <param name="jobLevelRepository">Repository for job level data access operations</param>
        /// <param name="unitOfWork">Unit of work for managing database transactions</param>
        /// <param name="mapper">AutoMapper instance for object-to-object mapping</param>
        /// <param name="logger">Logger for service diagnostics and monitoring</param>
        /// <exception cref="ArgumentNullException">Thrown when any required dependency is null</exception>
        public JobLevelService(
            IJobLevelRepository jobLevelRepository,
            IOpenHRCoreUnitOfWork unitOfWork,
            IMapper mapper,
            ILogger<JobLevelService> logger) // Fixed logger type
        {
            _jobLevelRepository = jobLevelRepository ?? throw new ArgumentNullException(nameof(jobLevelRepository));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Creates a new job level in the system.
        /// </summary>
        /// <param name="request">The job level creation request containing required information</param>
        /// <returns>A response containing the created job level details if successful, or error information if failed</returns>
        public async Task<OpenHRCoreServiceResponse<GetJobLevelResponse>> CreateJobLevelAsync(CreateJobLevelRequest request)
        {
            _logger.LogLayerInfo("Beginning job level creation process for: {JobLevelName}", request.LevelName);

            try
            {
                var jobLevel = _mapper.Map<JobLevel>(request);
                jobLevel.CreatedAt = DateTime.UtcNow;
                jobLevel.SortOrder = await GetNextSortOrderAsync();

                await _jobLevelRepository.AddAsync(jobLevel);
                await _unitOfWork.SaveChangesAsync();

                _logger.LogLayerInfo("Job level creation completed successfully. ID: {JobLevelId}", jobLevel.Id);

                var response = _mapper.Map<GetJobLevelResponse>(jobLevel);

                return OpenHRCoreServiceResponse<GetJobLevelResponse>.CreateSuccess(
                    response,
                    "Job Level created successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogLayerError(ex, "Job level creation failed for: {JobLevelName}. Error: {ErrorMessage}", 
                    request.LevelName, ex.Message);
                return OpenHRCoreServiceResponse<GetJobLevelResponse>.CreateFailure(
                    ex,
                    "An error occurred while creating the Job Level.");
            }
        }

        /// <summary>
        /// Updates an existing job level in the system.
        /// </summary>
        /// <param name="request">The job level update request containing modified information</param>
        /// <returns>A response containing the updated job level details if successful, or error information if failed</returns>
        public async Task<OpenHRCoreServiceResponse<GetJobLevelResponse>> UpdateJobLevelAsync(UpdateJobLevelRequest request)
        {
            try
            {
                _logger.LogLayerInfo("Beginning update process for job level ID: {JobLevelId}", request.Id);

                var existingJobLevel = await _jobLevelRepository.GetByIdAsync(request.Id);
                if (existingJobLevel == null)
                {
                    _logger.LogLayerWarning("Update failed - Unable to locate job level with ID: {JobLevelId}", request.Id);
                    return OpenHRCoreServiceResponse<GetJobLevelResponse>.CreateFailure("Job Level not found.");
                }

                _mapper.Map(request, existingJobLevel);
                existingJobLevel.UpdatedAt = DateTime.UtcNow;

                _jobLevelRepository.Update(existingJobLevel);
                await _unitOfWork.SaveChangesAsync();

                _logger.LogLayerInfo("Job level update completed successfully for ID: {JobLevelId}", request.Id);

                var response = _mapper.Map<GetJobLevelResponse>(existingJobLevel);

                return OpenHRCoreServiceResponse<GetJobLevelResponse>.CreateSuccess(
                    response,
                    "Job Level updated successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogLayerError(ex, "Job level update failed for ID: {JobLevelId}. Error: {ErrorMessage}", 
                    request.Id, ex.Message);
                return OpenHRCoreServiceResponse<GetJobLevelResponse>.CreateFailure(
                    ex,
                    "An error occurred while updating the Job Level.");
            }
        }

        /// <summary>
        /// Deletes a job level from the system.
        /// </summary>
        /// <param name="id">The unique identifier of the job level to delete</param>
        /// <returns>A response containing the deleted job level details if successful, or error information if failed</returns>
        public async Task<OpenHRCoreServiceResponse<GetJobLevelResponse>> DeleteJobLevelAsync(Guid id)
        {
            try
            {
                _logger.LogLayerInfo("Beginning deletion process for job level ID: {JobLevelId}", id);

                var jobLevel = await _jobLevelRepository.GetByIdAsync(id);
                if (jobLevel == null)
                {
                    _logger.LogLayerWarning("Deletion failed - Unable to locate job level with ID: {JobLevelId}", id);
                    return OpenHRCoreServiceResponse<GetJobLevelResponse>.CreateFailure("Job Level not found.");
                }

                _jobLevelRepository.Remove(jobLevel);
                await _unitOfWork.SaveChangesAsync();

                _logger.LogLayerInfo("Job level successfully deleted. ID: {JobLevelId}", id);

                var response = _mapper.Map<GetJobLevelResponse>(jobLevel);

                return OpenHRCoreServiceResponse<GetJobLevelResponse>.CreateSuccess(
                    response,
                    "Job Level deleted successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogLayerError(ex, "Job level deletion failed for ID: {JobLevelId}. Error: {ErrorMessage}", 
                    id, ex.Message);
                return OpenHRCoreServiceResponse<GetJobLevelResponse>.CreateFailure(
                    ex,
                    "An error occurred while deleting the Job Level.");
            }
        }

        /// <summary>
        /// Retrieves a specific job level by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the job level to retrieve</param>
        /// <returns>A response containing the requested job level details if found, or error information if not found</returns>
        public async Task<OpenHRCoreServiceResponse<GetJobLevelResponse>> GetJobLevelByIdAsync(Guid id)
        {
            try
            {
                _logger.LogLayerInfo("Initiating retrieval of job level with ID: {JobLevelId}", id);

                var jobLevel = await _jobLevelRepository.GetByIdAsync(id);
                if (jobLevel == null)
                {
                    _logger.LogLayerWarning("Retrieval failed - Unable to locate job level with ID: {JobLevelId}", id);
                    return OpenHRCoreServiceResponse<GetJobLevelResponse>.CreateFailure("Job Level not found.");
                }

                var response = _mapper.Map<GetJobLevelResponse>(jobLevel);

                _logger.LogLayerInfo("Successfully retrieved job level details for ID: {JobLevelId}", id);

                return OpenHRCoreServiceResponse<GetJobLevelResponse>.CreateSuccess(
                    response,
                    "Job Level retrieved successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogLayerError(ex, "Failed to retrieve job level with ID: {JobLevelId}. Error: {ErrorMessage}", 
                    id, ex.Message);
                return OpenHRCoreServiceResponse<GetJobLevelResponse>.CreateFailure(
                    ex,
                    "An error occurred while retrieving the Job Level.");
            }
        }

        /// <summary>
        /// Retrieves all job levels from the system.
        /// </summary>
        /// <returns>A response containing a collection of all job levels if any exist, or error information if none found</returns>
        public async Task<OpenHRCoreServiceResponse<IEnumerable<GetJobLevelResponse>>> GetAllJobLevelsAsync()
        {
            try
            {
                _logger.LogLayerInfo("Initiating retrieval of all job levels");

                var jobLevels = await _jobLevelRepository.GetAllAsync();

                var response = _mapper.Map<IEnumerable<GetJobLevelResponse>>(jobLevels);

                _logger.LogLayerInfo("Successfully retrieved complete list of job levels. Total count: {Count}", 
                    jobLevels.Count());

                return OpenHRCoreServiceResponse<IEnumerable<GetJobLevelResponse>>.CreateSuccess(
                    response,
                    "Job Levels retrieved successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogLayerError(ex, "Failed to retrieve job levels. Error: {ErrorMessage}", ex.Message);
                return OpenHRCoreServiceResponse<IEnumerable<GetJobLevelResponse>>.CreateFailure(
                    ex,
                    "An error occurred while retrieving the Job Levels.");
            }
        }

        /// <summary>
        /// Calculates the next available sort order value for a new job level.
        /// </summary>
        /// <returns>The next available sort order value, calculated as maximum existing sort order plus one</returns>
        private async Task<int> GetNextSortOrderAsync()
        {
            _logger.LogLayerInfo("Calculating next available sort order for new job level");

            var maxSortOrder = await _jobLevelRepository.MaxAsync(jl => jl.SortOrder); // Changed parameter name for clarity
            _logger.LogLayerInfo("Current maximum sort order is: {MaxSortOrder}. Next available will be: {NextSortOrder}",
                maxSortOrder, maxSortOrder + 1);

            return maxSortOrder + 1;
        }
    }
}
