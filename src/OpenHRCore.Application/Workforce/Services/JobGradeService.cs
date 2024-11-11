using AutoMapper;
using Microsoft.Extensions.Logging;
using OpenHRCore.Application.UnitOfWork;
using OpenHRCore.Application.Workforce.DTOs.JobGradeDtos;
using OpenHRCore.Application.Workforce.Interfaces;
using OpenHRCore.Domain.Workforce.Entities;
using OpenHRCore.Domain.Workforce.Interfaces;
using OpenHRCore.SharedKernel.Utilities;

namespace OpenHRCore.Application.Workforce.Services
{
    /// <summary>
    /// Service responsible for managing job grades, including CRUD operations.
    /// Implements the IJobGradeService interface to provide functionality for creating, reading, updating and deleting job grades.
    /// </summary>
    public class JobGradeService : IJobGradeService
    {
        private readonly IJobGradeRepository _jobGradeRepository;
        private readonly IOpenHRCoreUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<JobGradeService> _logger;

        /// <summary>
        /// Initializes a new instance of the JobGradeService class.
        /// </summary>
        /// <param name="jobGradeRepository">Repository for job grade data access</param>
        /// <param name="unitOfWork">Unit of work for transaction management</param>
        /// <param name="mapper">AutoMapper instance for object mapping</param>
        /// <param name="logger">Logger for service operations</param>
        /// <exception cref="ArgumentNullException">Thrown when any required dependency is null</exception>
        public JobGradeService(
            IJobGradeRepository jobGradeRepository,
            IOpenHRCoreUnitOfWork unitOfWork,
            IMapper mapper,
            ILogger<JobGradeService> logger)
        {
            _jobGradeRepository = jobGradeRepository ?? throw new ArgumentNullException(nameof(jobGradeRepository));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Creates a new job grade in the system.
        /// </summary>
        /// <param name="request">The job grade creation request details</param>
        /// <returns>Response containing the created job grade information</returns>
        public async Task<OpenHRCoreServiceResponse<GetJobGradeResponse>> CreateJobGradeAsync(CreateJobGradeRequest request)
        {
            _logger.LogLayerInfo("Beginning job grade creation process for: {JobGradeName}", request.GradeName);

            try
            {
                var jobGrade = _mapper.Map<JobGrade>(request);
                jobGrade.CreatedAt = DateTime.UtcNow;
                jobGrade.SortOrder = await GetNextSortOrderAsync();

                await _jobGradeRepository.AddAsync(jobGrade);
                await _unitOfWork.SaveChangesAsync();

                _logger.LogLayerInfo("Job grade creation completed successfully. ID: {JobGradeId}", jobGrade.Id);

                var response = _mapper.Map<GetJobGradeResponse>(jobGrade);

                return OpenHRCoreServiceResponse<GetJobGradeResponse>.CreateSuccess(
                    response,
                    "Job Grade created successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogLayerError(ex, "Job grade creation failed for: {JobGradeName}. Error: {ErrorMessage}", 
                    request.GradeName, ex.Message);
                return OpenHRCoreServiceResponse<GetJobGradeResponse>.CreateFailure(
                    ex,
                    "An error occurred while creating the Job Grade.");
            }
        }

        /// <summary>
        /// Updates an existing job grade's information.
        /// </summary>
        /// <param name="request">The job grade update request details</param>
        /// <returns>Response containing the updated job grade information</returns>
        public async Task<OpenHRCoreServiceResponse<GetJobGradeResponse>> UpdateJobGradeAsync(UpdateJobGradeRequest request)
        {
            try
            {
                _logger.LogLayerInfo("Beginning update process for job grade ID: {JobGradeId}", request.Id);

                var existingJobGrade = await _jobGradeRepository.GetByIdAsync(request.Id);
                if (existingJobGrade == null)
                {
                    _logger.LogLayerWarning("Update failed - Unable to locate job grade with ID: {JobGradeId}", request.Id);
                    return OpenHRCoreServiceResponse<GetJobGradeResponse>.CreateFailure("Job Grade not found.");
                }

                _mapper.Map(request, existingJobGrade);
                existingJobGrade.UpdatedAt = DateTime.UtcNow;

                _jobGradeRepository.Update(existingJobGrade);
                await _unitOfWork.SaveChangesAsync();

                _logger.LogLayerInfo("Job grade update completed successfully for ID: {JobGradeId}", request.Id);

                var response = _mapper.Map<GetJobGradeResponse>(existingJobGrade);

                return OpenHRCoreServiceResponse<GetJobGradeResponse>.CreateSuccess(
                    response,
                    "Job Grade updated successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogLayerError(ex, "Job grade update failed for ID: {JobGradeId}. Error: {ErrorMessage}", 
                    request.Id, ex.Message);
                return OpenHRCoreServiceResponse<GetJobGradeResponse>.CreateFailure(
                    ex,
                    "An error occurred while updating the Job Grade.");
            }
        }

        /// <summary>
        /// Deletes a job grade from the system.
        /// </summary>
        /// <param name="id">The unique identifier of the job grade to delete</param>
        /// <returns>Response indicating the result of the deletion operation</returns>
        public async Task<OpenHRCoreServiceResponse<GetJobGradeResponse>> DeleteJobGradeAsync(Guid id)
        {
            try
            {
                _logger.LogLayerInfo("Beginning deletion process for job grade ID: {JobGradeId}", id);

                var jobGrade = await _jobGradeRepository.GetByIdAsync(id);
                if (jobGrade == null)
                {
                    _logger.LogLayerWarning("Deletion failed - Unable to locate job grade with ID: {JobGradeId}", id);
                    return OpenHRCoreServiceResponse<GetJobGradeResponse>.CreateFailure("Job Grade not found.");
                }

                _jobGradeRepository.Remove(jobGrade);
                await _unitOfWork.SaveChangesAsync();

                _logger.LogLayerInfo("Job grade successfully deleted. ID: {JobGradeId}", id);

                var response = _mapper.Map<GetJobGradeResponse>(jobGrade);

                return OpenHRCoreServiceResponse<GetJobGradeResponse>.CreateSuccess(
                    response,
                    "Job Grade deleted successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogLayerError(ex, "Job grade deletion failed for ID: {JobGradeId}. Error: {ErrorMessage}", 
                    id, ex.Message);
                return OpenHRCoreServiceResponse<GetJobGradeResponse>.CreateFailure(
                    ex,
                    "An error occurred while deleting the Job Grade.");
            }
        }

        /// <summary>
        /// Retrieves a specific job grade by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the job grade to retrieve</param>
        /// <returns>Response containing the requested job grade information</returns>
        public async Task<OpenHRCoreServiceResponse<GetJobGradeResponse>> GetJobGradeByIdAsync(Guid id)
        {
            try
            {
                _logger.LogLayerInfo("Initiating retrieval of job grade with ID: {JobGradeId}", id);

                var jobGrade = await _jobGradeRepository.GetByIdAsync(id);
                if (jobGrade == null)
                {
                    _logger.LogLayerWarning("Retrieval failed - Unable to locate job grade with ID: {JobGradeId}", id);
                    return OpenHRCoreServiceResponse<GetJobGradeResponse>.CreateFailure("Job Grade not found.");
                }

                var response = _mapper.Map<GetJobGradeResponse>(jobGrade);

                _logger.LogLayerInfo("Successfully retrieved job grade details for ID: {JobGradeId}", id);

                return OpenHRCoreServiceResponse<GetJobGradeResponse>.CreateSuccess(
                    response,
                    "Job Grade retrieved successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogLayerError(ex, "Failed to retrieve job grade with ID: {JobGradeId}. Error: {ErrorMessage}", 
                    id, ex.Message);
                return OpenHRCoreServiceResponse<GetJobGradeResponse>.CreateFailure(
                    ex,
                    "An error occurred while retrieving the Job Grade.");
            }
        }

        /// <summary>
        /// Retrieves all job grades from the system.
        /// </summary>
        /// <returns>Response containing a collection of all job grades</returns>
        public async Task<OpenHRCoreServiceResponse<IEnumerable<GetJobGradeResponse>>> GetAllJobGradesAsync()
        {
            try
            {
                _logger.LogLayerInfo("Initiating retrieval of all job grades");

                var jobGrades = await _jobGradeRepository.GetAllAsync();

                var response = _mapper.Map<IEnumerable<GetJobGradeResponse>>(jobGrades);

                _logger.LogLayerInfo("Successfully retrieved complete list of job grades. Total count: {Count}", 
                    jobGrades.Count());

                return OpenHRCoreServiceResponse<IEnumerable<GetJobGradeResponse>>.CreateSuccess(
                    response,
                    "Job Grades retrieved successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogLayerError(ex, "Failed to retrieve job grades. Error: {ErrorMessage}", ex.Message);
                return OpenHRCoreServiceResponse<IEnumerable<GetJobGradeResponse>>.CreateFailure(
                    ex,
                    "An error occurred while retrieving the Job Grades.");
            }
        }

        /// <summary>
        /// Calculates the next available sort order value for a new job grade.
        /// </summary>
        /// <returns>The next available sort order value, calculated by incrementing the maximum existing sort order by one</returns>
        private async Task<int> GetNextSortOrderAsync()
        {
            _logger.LogLayerInfo("Calculating next available sort order for new job grade");

            var maxSortOrder = await _jobGradeRepository.MaxAsync(ou => ou.SortOrder);
            _logger.LogLayerInfo("Current maximum sort order is: {MaxSortOrder}. Next available will be: {NextSortOrder}",
                maxSortOrder, maxSortOrder + 1);

            return maxSortOrder + 1;
        }
    }
}
