using AutoMapper;
using Microsoft.Extensions.Logging;
using OpenHRCore.Application.DTOs.JobGrade;
using OpenHRCore.Application.Interfaces;
using OpenHRCore.Application.UnitOfWork;
using OpenHRCore.SharedKernel.Utilities;

namespace OpenHRCore.Application.Services
{
    /// <summary>
    /// Service responsible for managing job positions and related operations.
    /// </summary>
    public class JobPositionService : IJobPositionService
    {
        private readonly IWorkForceUnitOfWork _unitOfWork;
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
            IWorkForceUnitOfWork unitOfWork,
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
            var response = new OpenHRCoreServiceResponse<CreateJobGradeResponse>();

            try
            {
                _logger.LogApplicationInfo("Starting job grade creation for {JobGradeName}", request.Name);

                var jobGrade = _mapper.Map<JobGrade>(request);
                jobGrade.SortOrder = await GetNextSortOrderAsync();

                await _jobGradeRepository.AddAsync(jobGrade);
                await _unitOfWork.SaveChangesAsync();

                response.Data = _mapper.Map<CreateJobGradeResponse>(jobGrade);
                response.UserMessage = "Job grade created successfully.";

                _logger.LogApplicationInfo("Job grade {JobGradeId} created successfully with sort order {SortOrder}", jobGrade.Id, jobGrade.SortOrder);
            }
            catch (Exception ex)
            {
                HandleException(response, ex, "An error occurred while creating the job grade.");
            }

            return response;
        }

        /// <summary>
        /// Deletes a job grade asynchronously.
        /// </summary>
        /// <param name="request">The request containing the ID of the job grade to delete.</param>
        /// <returns>A response containing the deleted job grade details.</returns>
        public async Task<OpenHRCoreServiceResponse<DeleteJobGradeResponse>> DeleteJobGradeAsync(DeleteJobGradeRequest request)
        {
            var response = new OpenHRCoreServiceResponse<DeleteJobGradeResponse>();

            try
            {
                _logger.LogApplicationInfo("Attempting to delete job grade with ID: {JobGradeId}", request.Id);

                var jobGradeToDelete = await _jobGradeRepository.GetByIdAsync(request.Id);

                if (jobGradeToDelete == null)
                {
                    _logger.LogApplicationWarning("Job grade with ID {JobGradeId} not found.", request.Id);
                    return new OpenHRCoreServiceResponse<DeleteJobGradeResponse>("Job grade not found.");
                }

                _jobGradeRepository.Remove(jobGradeToDelete);
                await _unitOfWork.SaveChangesAsync();

                response.Data = _mapper.Map<DeleteJobGradeResponse>(jobGradeToDelete);
                response.UserMessage = "Job grade deleted successfully.";

                _logger.LogApplicationInfo("Job grade {JobGradeId} deleted successfully.", request.Id);
            }
            catch (Exception ex)
            {
                HandleException(response, ex, "An error occurred while deleting the job grade.");
            }

            return response;
        }

        /// <summary>
        /// Retrieves all job grades asynchronously.
        /// </summary>
        /// <returns>A response containing a collection of all job grades.</returns>
        public async Task<OpenHRCoreServiceResponse<IEnumerable<GetAllJobGradesResponse>>> GetAllJobGradesAsync()
        {
            var response = new OpenHRCoreServiceResponse<IEnumerable<GetAllJobGradesResponse>>();

            try
            {
                _logger.LogApplicationInfo("Retrieving all job grades.");

                var jobGrades = await _jobGradeRepository.GetAllAsync();
                var jobGradeResponses = _mapper.Map<IEnumerable<GetAllJobGradesResponse>>(jobGrades);

                response.Data = jobGradeResponses;
                response.UserMessage = "Retrieved all job grades successfully.";

                _logger.LogApplicationInfo("Retrieved {Count} job grades.", jobGrades.Count());
            }
            catch (Exception ex)
            {
                HandleException(response, ex, "An error occurred while retrieving job grades.");
            }

            return response;
        }

        /// <summary>
        /// Retrieves a job grade by its ID asynchronously.
        /// </summary>
        /// <param name="id">The ID of the job grade to retrieve.</param>
        /// <returns>A response containing the retrieved job grade details.</returns>
        public async Task<OpenHRCoreServiceResponse<GetJobGradeByIdResponse>> GetJobGradeByIdAsync(Guid id)
        {
            var response = new OpenHRCoreServiceResponse<GetJobGradeByIdResponse>();

            try
            {
                _logger.LogApplicationInfo("Retrieving job grade with ID: {JobGradeId}", id);

                var jobGrade = await _jobGradeRepository.GetByIdAsync(id);

                if (jobGrade == null)
                {
                    _logger.LogApplicationWarning("Job grade with ID {JobGradeId} not found.", id);
                    return new OpenHRCoreServiceResponse<GetJobGradeByIdResponse>("Job grade not found.");
                }

                var jobGradeResponse = _mapper.Map<GetJobGradeByIdResponse>(jobGrade);
                response.Data = jobGradeResponse;
                response.UserMessage = "Retrieved job grade successfully.";

                _logger.LogApplicationInfo("Successfully retrieved job grade with ID: {JobGradeId}", id);
            }
            catch (Exception ex)
            {
                HandleException(response, ex, "An error occurred while retrieving the job grade.");
            }

            return response;
        }

        /// <summary>
        /// Updates a job grade asynchronously.
        /// </summary>
        /// <param name="request">The request containing updated job grade details.</param>
        /// <returns>A response containing the updated job grade details.</returns>
        public async Task<OpenHRCoreServiceResponse<UpdateJobGradeResponse>> UpdateJobGradeAsync(UpdateJobGradeRequest request)
        {
            var response = new OpenHRCoreServiceResponse<UpdateJobGradeResponse>();

            try
            {
                _logger.LogApplicationInfo("Attempting to update job grade with ID: {JobGradeId}", request.Id);

                var existingJobGrade = await _jobGradeRepository.GetByIdAsync(request.Id);

                if (existingJobGrade == null)
                {
                    _logger.LogApplicationWarning("Job grade with ID {JobGradeId} not found.", request.Id);
                    return new OpenHRCoreServiceResponse<UpdateJobGradeResponse>("Job grade not found.");
                }

                _mapper.Map(request, existingJobGrade);

                _jobGradeRepository.Update(existingJobGrade);
                await _unitOfWork.SaveChangesAsync();

                response.Data = _mapper.Map<UpdateJobGradeResponse>(existingJobGrade);
                response.UserMessage = "Job grade updated successfully.";

                _logger.LogApplicationInfo("Job grade {JobGradeId} updated successfully.", request.Id);
            }
            catch (Exception ex)
            {
                HandleException(response, ex, "An error occurred while updating the job grade.");
            }

            return response;
        }

        /// <summary>
        /// Calculates the next sort order for job grades.
        /// </summary>
        /// <returns>The next available sort order.</returns>
        private async Task<int> GetNextSortOrderAsync()
        {
            _logger.LogApplicationInfo("Calculating next sort order for job grades.");

            var maxSortOrder = await _jobGradeRepository.MaxAsync(jg => jg.SortOrder);
            _logger.LogApplicationInfo("Max sort order retrieved: {MaxSortOrder}", maxSortOrder);

            return maxSortOrder + 1;
        }

        /// <summary>
        /// Handles exceptions by logging the error and setting appropriate response properties.
        /// </summary>
        /// <typeparam name="T">The type of the response data.</typeparam>
        /// <param name="response">The service response to update.</param>
        /// <param name="ex">The exception that occurred.</param>
        /// <param name="errorMessage">The error message to set in the response.</param>
        private void HandleException<T>(OpenHRCoreServiceResponse<T> response, Exception ex, string errorMessage) where T : class
        {
            response.ErrorMessage = errorMessage;
            response.TechnicalMessage = ex.Message;
            _logger.LogApplicationError(ex, "{ErrorMessage}: {ExceptionMessage}", errorMessage, ex.Message);
        }
    }
}
