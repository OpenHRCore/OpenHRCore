using AutoMapper;
using Microsoft.Extensions.Logging;
using OpenHRCore.Application.UnitOfWork;
using OpenHRCore.Application.Workforce.DTOs.JobPositionDtos;
using OpenHRCore.Application.Workforce.Interfaces;
using OpenHRCore.Domain.Workforce.Entities;
using OpenHRCore.Domain.Workforce.Interfaces;
using OpenHRCore.SharedKernel.Utilities;

namespace OpenHRCore.Application.Workforce.Services
{
    /// <summary>
    /// Service responsible for managing job positions in the system.
    /// Implements CRUD operations and business logic for job position entities.
    /// </summary>
    public class JobPositionService : IJobPositionService
    {
        private readonly IJobPositionRepository _jobPositionRepository;
        private readonly IOpenHRCoreUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<JobPositionService> _logger;

        /// <summary>
        /// Initializes a new instance of the JobPositionService class.
        /// </summary>
        /// <param name="jobPositionRepository">Repository for job position data access operations</param>
        /// <param name="unitOfWork">Unit of work for managing database transactions</param>
        /// <param name="mapper">AutoMapper instance for object-to-object mapping</param>
        /// <param name="logger">Logger for service diagnostics and monitoring</param>
        /// <exception cref="ArgumentNullException">Thrown when any required dependency is null</exception>
        public JobPositionService(
            IJobPositionRepository jobPositionRepository,
            IOpenHRCoreUnitOfWork unitOfWork,
            IMapper mapper,
            ILogger<JobPositionService> logger)
        {
            _jobPositionRepository = jobPositionRepository ?? throw new ArgumentNullException(nameof(jobPositionRepository));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Creates a new job position in the system.
        /// </summary>
        /// <param name="request">The job position creation request containing required information</param>
        /// <returns>Response containing the created job position details if successful, or error information if failed</returns>
        public async Task<OpenHRCoreServiceResponse<GetJobPositionResponse>> CreateJobPositionAsync(CreateJobPositionRequest request)
        {
            _logger.LogLayerInfo("Beginning job position creation process for: {Title}", request.Title);

            try
            {
                var jobPosition = _mapper.Map<JobPosition>(request);
                jobPosition.CreatedAt = DateTime.UtcNow;

                await _jobPositionRepository.AddAsync(jobPosition);
                await _unitOfWork.SaveChangesAsync();

                _logger.LogLayerInfo("Job position creation completed successfully. ID: {JobPositionId}", jobPosition.Id);

                var response = await GetJobPositionResponseById(jobPosition.Id);

                return OpenHRCoreServiceResponse<GetJobPositionResponse>.CreateSuccess(
                    response,
                    "Job Position created successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogLayerError(ex, "Job position creation failed for: {Title}. Error: {ErrorMessage}", 
                    request.Title, ex.Message);
                return OpenHRCoreServiceResponse<GetJobPositionResponse>.CreateFailure(
                    ex,
                    "An error occurred while creating the Job Position.");
            }
        }

        /// <summary>
        /// Updates an existing job position in the system.
        /// </summary>
        /// <param name="request">The job position update request containing modified information</param>
        /// <returns>Response containing the updated job position details if successful, or error information if failed</returns>
        public async Task<OpenHRCoreServiceResponse<GetJobPositionResponse>> UpdateJobPositionAsync(UpdateJobPositionRequest request)
        {
            try
            {
                _logger.LogLayerInfo("Beginning update process for job position ID: {JobPositionId}", request.Id);

                var existingJobPosition = await _jobPositionRepository.GetByIdAsync(request.Id);
                if (existingJobPosition == null)
                {
                    _logger.LogLayerWarning("Update failed - Unable to locate job position with ID: {JobPositionId}", request.Id);
                    return OpenHRCoreServiceResponse<GetJobPositionResponse>.CreateFailure("Job Position not found.");
                }

                _mapper.Map(request, existingJobPosition);
                existingJobPosition.UpdatedAt = DateTime.UtcNow;

                _jobPositionRepository.Update(existingJobPosition);
                await _unitOfWork.SaveChangesAsync();

                _logger.LogLayerInfo("Job position update completed successfully for ID: {JobPositionId}", request.Id);

                var response = await GetJobPositionResponseById(request.Id);

                return OpenHRCoreServiceResponse<GetJobPositionResponse>.CreateSuccess(
                    response,
                    "Job Position updated successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogLayerError(ex, "Job position update failed for ID: {JobPositionId}. Error: {ErrorMessage}", 
                    request.Id, ex.Message);
                return OpenHRCoreServiceResponse<GetJobPositionResponse>.CreateFailure(
                    ex,
                    "An error occurred while updating the Job Position.");
            }
        }

        /// <summary>
        /// Deletes a job position from the system.
        /// </summary>
        /// <param name="id">The unique identifier of the job position to delete</param>
        /// <returns>Response containing the deleted job position details if successful, or error information if failed</returns>
        public async Task<OpenHRCoreServiceResponse<GetJobPositionResponse>> DeleteJobPositionAsync(Guid id)
        {
            try
            {
                _logger.LogLayerInfo("Beginning deletion process for job position ID: {JobPositionId}", id);

                var jobPosition = await _jobPositionRepository.GetByIdAsync(id);
                if (jobPosition == null)
                {
                    _logger.LogLayerWarning("Deletion failed - Unable to locate job position with ID: {JobPositionId}", id);
                    return OpenHRCoreServiceResponse<GetJobPositionResponse>.CreateFailure("Job Position not found.");
                }

                _jobPositionRepository.Remove(jobPosition);
                await _unitOfWork.SaveChangesAsync();

                _logger.LogLayerInfo("Job position successfully deleted. ID: {JobPositionId}", id);

                var response = _mapper.Map<GetJobPositionResponse>(jobPosition);

                return OpenHRCoreServiceResponse<GetJobPositionResponse>.CreateSuccess(
                    response,
                    "Job Position deleted successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogLayerError(ex, "Job position deletion failed for ID: {JobPositionId}. Error: {ErrorMessage}", 
                    id, ex.Message);
                return OpenHRCoreServiceResponse<GetJobPositionResponse>.CreateFailure(
                    ex,
                    "An error occurred while deleting the Job Position.");
            }
        }

        /// <summary>
        /// Retrieves a specific job position by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the job position to retrieve</param>
        /// <returns>Response containing the requested job position details if found, or error information if not found</returns>
        public async Task<OpenHRCoreServiceResponse<GetJobPositionResponse>> GetJobPositionByIdAsync(Guid id)
        {
            try
            {
                _logger.LogLayerInfo("Initiating retrieval of job position with ID: {JobPositionId}", id);

                var jobPosition = await _jobPositionRepository.GetFirstOrDefaultAsync(x => x.Id == id,
                    x => x.JobLevel!,
                    x => x.JobGrade!,
                    x => x.OrganizationUnit!);

                if (jobPosition == null)
                {
                    _logger.LogLayerWarning("Retrieval failed - Unable to locate job position with ID: {JobPositionId}", id);
                    return OpenHRCoreServiceResponse<GetJobPositionResponse>.CreateFailure("Job Position not found.");
                }

                var response = _mapper.Map<GetJobPositionResponse>(jobPosition);

                _logger.LogLayerInfo("Successfully retrieved job position details for ID: {JobPositionId}", id);

                return OpenHRCoreServiceResponse<GetJobPositionResponse>.CreateSuccess(
                    response,
                    "Job Position retrieved successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogLayerError(ex, "Failed to retrieve job position with ID: {JobPositionId}. Error: {ErrorMessage}", 
                    id, ex.Message);
                return OpenHRCoreServiceResponse<GetJobPositionResponse>.CreateFailure(
                    ex,
                    "An error occurred while retrieving the Job Position.");
            }
        }

        /// <summary>
        /// Retrieves all job positions from the system.
        /// </summary>
        /// <returns>Response containing a collection of all job positions if any exist, or error information if none found</returns>
        public async Task<OpenHRCoreServiceResponse<IEnumerable<GetJobPositionResponse>>> GetAllJobPositionsAsync()
        {
            try
            {
                _logger.LogLayerInfo("Initiating retrieval of all job positions");

                var jobPositions = await _jobPositionRepository.GetAllAsync();

                var response = _mapper.Map<IEnumerable<GetJobPositionResponse>>(jobPositions);

                _logger.LogLayerInfo("Successfully retrieved complete list of job positions. Total count: {Count}", 
                    jobPositions.Count());

                return OpenHRCoreServiceResponse<IEnumerable<GetJobPositionResponse>>.CreateSuccess(
                    response,
                    "Job Positions retrieved successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogLayerError(ex, "Failed to retrieve job positions. Error: {ErrorMessage}", ex.Message);
                return OpenHRCoreServiceResponse<IEnumerable<GetJobPositionResponse>>.CreateFailure(
                    ex,
                    "An error occurred while retrieving the Job Positions.");
            }
        }

        private async Task<GetJobPositionResponse> GetJobPositionResponseById(Guid id)
        {
            var jobPosition = await _jobPositionRepository.GetFirstOrDefaultAsync(x => x.Id == id,
                x => x.JobLevel!,
                x => x.JobGrade!,
                x => x.OrganizationUnit!);

            return _mapper.Map<GetJobPositionResponse>(jobPosition);
        }
    }
}
