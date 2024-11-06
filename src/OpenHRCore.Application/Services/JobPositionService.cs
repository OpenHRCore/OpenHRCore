using AutoMapper;
using Microsoft.Extensions.Logging;
using OpenHRCore.Application.DTOs.JobGrade;
using OpenHRCore.Application.Interfaces;
using OpenHRCore.Application.UnitOfWork;
using OpenHRCore.Domain.EmployeeModule.Entities;
using OpenHRCore.Domain.EmployeeModule.Interfaces;

namespace OpenHRCore.Application.Services
{
    /// <summary>
    /// Service responsible for managing job positions and related operations.
    /// </summary>
    public class JobPositionService : IJobPositionService
    {
        private readonly IOpenHRCoreUnitOfWork _unitOfWork;
        private readonly IJobGradeRepository _jobGradeRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<JobPositionService> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="JobPositionService"/> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work for managing database transactions.</param>
        /// <param name="jobGradeRepository">The repository for job grade operations.</param>
        /// <param name="mapper">The AutoMapper instance for object mapping.</param>
        /// <param name="logger">The logger for logging service operations.</param>
        /// <exception cref="ArgumentNullException">Thrown if any of the parameters are null.</exception>
        public JobPositionService(
            IOpenHRCoreUnitOfWork unitOfWork,
            IJobGradeRepository jobGradeRepository,
            IMapper mapper,
            ILogger<JobPositionService> logger)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _jobGradeRepository = jobGradeRepository ?? throw new ArgumentNullException(nameof(jobGradeRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Creates a new job grade asynchronously.
        /// </summary>
        /// <param name="request">The request containing job grade details.</param>
        /// <returns>A response containing the created job grade details.</returns>
        public async Task<OpenHRCoreServiceResponse<CreateJobGradeResponse>> CreateJobGradeAsync(CreateJobGradeRequest request)
        {
            try
            {
                _logger.LogLayerInfo("Starting job grade creation for {JobGradeName}", request.Name);

                var jobGrade = _mapper.Map<JobGrade>(request);
                jobGrade.SortOrder = await GetNextSortOrderAsync();

                await _jobGradeRepository.AddAsync(jobGrade);
                await _unitOfWork.SaveChangesAsync();

                _logger.LogLayerInfo("Job grade {JobGradeId} created successfully with sort order {SortOrder}", jobGrade.Id, jobGrade.SortOrder);

                var response = _mapper.Map<CreateJobGradeResponse>(jobGrade);
                return OpenHRCoreServiceResponse<CreateJobGradeResponse>.CreateSuccess(response, "Job grade created successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogLayerError(ex, "An error occurred while creating the job grade.");
                return OpenHRCoreServiceResponse<CreateJobGradeResponse>.CreateFailure(ex, "An error occurred while creating the job grade.");
            }
        }

        /// <summary>
        /// Deletes a job grade asynchronously.
        /// </summary>
        /// <param name="request">The request containing the ID of the job grade to delete.</param>
        /// <returns>A response containing the deleted job grade details.</returns>
        public async Task<OpenHRCoreServiceResponse<DeleteJobGradeResponse>> DeleteJobGradeAsync(DeleteJobGradeRequest request)
        {
            try
            {
                _logger.LogLayerInfo("Attempting to delete job grade with ID: {JobGradeId}", request.Id);

                var jobGradeToDelete = await _jobGradeRepository.GetByIdAsync(Guid.Parse(request.Id));

                if (jobGradeToDelete == null)
                {
                    _logger.LogLayerWarning("Job grade with ID {JobGradeId} not found.", request.Id);
                    return OpenHRCoreServiceResponse<DeleteJobGradeResponse>.CreateFailure("Job grade not found.");
                }

                _jobGradeRepository.Remove(jobGradeToDelete);
                await _unitOfWork.SaveChangesAsync();

                _logger.LogLayerInfo("Job grade {JobGradeId} deleted successfully.", request.Id);
                var response = _mapper.Map<DeleteJobGradeResponse>(jobGradeToDelete);
                return OpenHRCoreServiceResponse<DeleteJobGradeResponse>.CreateSuccess(response, "Job grade deleted successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogLayerError(ex, "An error occurred while deleting the job grade.");
                return OpenHRCoreServiceResponse<DeleteJobGradeResponse>.CreateFailure(ex, "An error occurred while deleting the job grade.");
            }
        }

        /// <summary>
        /// Retrieves all job grades asynchronously.
        /// </summary>
        /// <returns>A response containing a collection of all job grades.</returns>
        public async Task<OpenHRCoreServiceResponse<IEnumerable<GetAllJobGradesResponse>>> GetAllJobGradesAsync()
        {
            try
            {
                _logger.LogLayerInfo("Retrieving all job grades.");

                var jobGrades = await _jobGradeRepository.GetAllAsync();
                var jobGradeResponses = _mapper.Map<IEnumerable<GetAllJobGradesResponse>>(jobGrades);

                _logger.LogLayerInfo("Retrieved {Count} job grades.", jobGrades.Count());

                return OpenHRCoreServiceResponse<IEnumerable<GetAllJobGradesResponse>>.CreateSuccess(jobGradeResponses, "Retrieved all job grades successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogLayerError(ex, "An error occurred while retrieving job grades.");
                return OpenHRCoreServiceResponse<IEnumerable<GetAllJobGradesResponse>>.CreateFailure(ex, "An error occurred while retrieving job grades.");
            }
        }

        /// <summary>
        /// Retrieves a job grade by its ID asynchronously.
        /// </summary>
        /// <param name="id">The ID of the job grade to retrieve.</param>
        /// <returns>A response containing the retrieved job grade details.</returns>
        public async Task<OpenHRCoreServiceResponse<GetJobGradeByIdResponse>> GetJobGradeByIdAsync(Guid id)
        {
            try
            {
                _logger.LogLayerInfo("Retrieving job grade with ID: {JobGradeId}", id);

                var jobGrade = await _jobGradeRepository.GetByIdAsync(id);

                if (jobGrade == null)
                {
                    _logger.LogLayerWarning("Job grade with ID {JobGradeId} not found.", id);
                    return OpenHRCoreServiceResponse<GetJobGradeByIdResponse>.CreateFailure("Job grade not found.");
                }

                var jobGradeResponse = _mapper.Map<GetJobGradeByIdResponse>(jobGrade);

                _logger.LogLayerInfo("Successfully retrieved job grade with ID: {JobGradeId}", id);

                return OpenHRCoreServiceResponse<GetJobGradeByIdResponse>.CreateSuccess(jobGradeResponse, "Retrieved job grade successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogLayerError(ex, "An error occurred while retrieving the job grade.");
                return OpenHRCoreServiceResponse<GetJobGradeByIdResponse>.CreateFailure(ex, "An error occurred while retrieving the job grade.");
            }
        }

        /// <summary>
        /// Updates a job grade asynchronously.
        /// </summary>
        /// <param name="request">The request containing updated job grade details.</param>
        /// <returns>A response containing the updated job grade details.</returns>
        public async Task<OpenHRCoreServiceResponse<UpdateJobGradeResponse>> UpdateJobGradeAsync(UpdateJobGradeRequest request)
        {
            try
            {
                _logger.LogLayerInfo("Attempting to update job grade with ID: {JobGradeId}", request.Id);

                var existingJobGrade = await _jobGradeRepository.GetByIdAsync(request.Id);

                if (existingJobGrade == null)
                {
                    _logger.LogLayerWarning("Job grade with ID {JobGradeId} not found.", request.Id);
                    return OpenHRCoreServiceResponse<UpdateJobGradeResponse>.CreateFailure("Job grade not found.");
                }

                _mapper.Map(request, existingJobGrade);

                _jobGradeRepository.Update(existingJobGrade);
                await _unitOfWork.SaveChangesAsync();

                _logger.LogLayerInfo("Job grade {JobGradeId} updated successfully.", request.Id);
                var response = _mapper.Map<UpdateJobGradeResponse>(existingJobGrade);
                return OpenHRCoreServiceResponse<UpdateJobGradeResponse>.CreateSuccess(response, "Job grade updated successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogLayerError(ex, "An error occurred while updating the job grade.");
                return OpenHRCoreServiceResponse<UpdateJobGradeResponse>.CreateFailure(ex, "An error occurred while updating the job grade.");
            }
        }

        /// <summary>
        /// Calculates the next sort order for job grades.
        /// </summary>
        /// <returns>The next available sort order.</returns>
        private async Task<int> GetNextSortOrderAsync()
        {
            _logger.LogLayerInfo("Calculating next sort order for job grades.");

            var maxSortOrder = await _jobGradeRepository.MaxAsync(jg => jg.SortOrder);
            _logger.LogLayerInfo("Max sort order retrieved: {MaxSortOrder}", maxSortOrder);

            return maxSortOrder + 1;
        }
    }
}
