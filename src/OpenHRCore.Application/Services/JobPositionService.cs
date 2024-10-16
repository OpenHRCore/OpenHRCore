using AutoMapper;
using OpenHRCore.Application.DTOs.JobGrade;
using OpenHRCore.Application.Interfaces;
using OpenHRCore.Application.UnitOfWork;

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

        /// <summary>
        /// Initializes a new instance of the <see cref="JobPositionService"/> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work for managing database transactions.</param>
        /// <param name="jobGradeRepository">The repository for job grade operations.</param>
        /// <param name="mapper">The AutoMapper instance for object mapping.</param>
        public JobPositionService(
            IWorkForceUnitOfWork unitOfWork,
            IJobGradeRepository jobGradeRepository,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _jobGradeRepository = jobGradeRepository ?? throw new ArgumentNullException(nameof(jobGradeRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        /// <summary>
        /// Creates a new job grade asynchronously.
        /// </summary>
        /// <param name="request">The request containing job grade creation details.</param>
        /// <returns>A task representing the asynchronous operation, containing the service response with the created job grade.</returns>
        public async Task<OpenHRCoreServiceResponse<CreateJobGradeResponse>> CreateJobGradeAsync(CreateJobGradeRequest request)
        {
            var response = new OpenHRCoreServiceResponse<CreateJobGradeResponse>();

            try
            {
                var jobGrade = _mapper.Map<JobGrade>(request);
                jobGrade.SortOrder = await GetNextSortOrderAsync();

                await _jobGradeRepository.AddAsync(jobGrade);
                await _unitOfWork.SaveChangesAsync();

                response.Data = _mapper.Map<CreateJobGradeResponse>(jobGrade);
                response.UserMessage = "Job grade created successfully.";
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "An error occurred while creating the job grade.";
                response.TechnicalMessage = ex.Message;
                // Consider logging the exception here
            }

            return response;
        }

        public async Task<OpenHRCoreServiceResponse<DeleteJobGradeResponse>> DeleteJobGradeAsync(DeleteJobGradeRequest request)
        {
            var response = new OpenHRCoreServiceResponse<DeleteJobGradeResponse>();

            try
            {
                var deleteJobGrade = await _jobGradeRepository.GetByIdAsync(request.Id);

                if (deleteJobGrade == null)
                    return new OpenHRCoreServiceResponse<DeleteJobGradeResponse>("Job grade not found.");

                _jobGradeRepository.Remove(deleteJobGrade);
                await _unitOfWork.SaveChangesAsync();
                response.Data = _mapper.Map<DeleteJobGradeResponse>(deleteJobGrade);
                response.UserMessage = "Job grade deleted successfully.";
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "An error occurred while creating the job grade.";
                response.TechnicalMessage = ex.Message;
            }
            return response;
        }

        public async Task<OpenHRCoreServiceResponse<IEnumerable<GetAllJobGradesResponse>>> GetAllJobGradesAsync()
        {
            var response = new OpenHRCoreServiceResponse<IEnumerable<GetAllJobGradesResponse>>();

            try
            {
                var jobGrades = await _jobGradeRepository.GetAllAsync();
                var jobGradeResponses = _mapper.Map<IEnumerable<GetAllJobGradesResponse>>(jobGrades);

                response.Data = jobGradeResponses;
                response.UserMessage = "Retrieved all job grades successfully.";
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "An error occurred while retrieving job grades.";
                response.TechnicalMessage = ex.Message;
            }

            return response;
        }

        public async Task<OpenHRCoreServiceResponse<GetJobGradeByIdResponse>> GetJobGradeByIdAsync(Guid id)
        {
            var response = new OpenHRCoreServiceResponse<GetJobGradeByIdResponse>();

            try
            {
                var jobGrade = await _jobGradeRepository.GetByIdAsync(id);

                if (jobGrade == null)
                    return new OpenHRCoreServiceResponse<GetJobGradeByIdResponse>("Job grade not found.");

                var jobGradeResponse = _mapper.Map<GetJobGradeByIdResponse>(jobGrade);

                response.Data = jobGradeResponse;
                response.UserMessage = "Retrieved job grade successfully.";
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "An error occurred while retrieving the job grade.";
                response.TechnicalMessage = ex.Message;
            }

            return response;
        }

        public async Task<OpenHRCoreServiceResponse<UpdateJobGradeResponse>> UpdateJobGradeAsync(UpdateJobGradeRequest request)
        {
            var response = new OpenHRCoreServiceResponse<UpdateJobGradeResponse>();

            try
            {
                var existingJobGrade = await _jobGradeRepository.GetByIdAsync(request.Id);

                if (existingJobGrade == null)
                    return new OpenHRCoreServiceResponse<UpdateJobGradeResponse>("Job grade not found.");

                existingJobGrade.Code = request.Code;
                existingJobGrade.Name = request.Name;
                existingJobGrade.Description = request.Description;

                _jobGradeRepository.Update(existingJobGrade);
                await _unitOfWork.SaveChangesAsync();

                response.Data = _mapper.Map<UpdateJobGradeResponse>(existingJobGrade);
                response.UserMessage = "Job grade updated successfully.";
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "An error occurred while updating the job grade.";
                response.TechnicalMessage = ex.Message;
            }

            return response;
        }

        /// <summary>
        /// Gets the next available sort order for a new job grade.
        /// </summary>
        /// <returns>The next available sort order.</returns>
        private async Task<int> GetNextSortOrderAsync()
        {
            var maxSortOrder = await _jobGradeRepository.MaxAsync(jg => jg.SortOrder);
            return maxSortOrder + 1;
        }
    }
}
