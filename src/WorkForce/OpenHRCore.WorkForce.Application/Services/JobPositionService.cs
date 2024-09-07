using AutoMapper;
using OpenHRCore.WorkForce.Application.DTOs.JobGrade;
using OpenHRCore.WorkForce.Application.Interfaces;
using OpenHRCore.WorkForce.Application.UnitOfWork;

namespace OpenHRCore.WorkForce.Application.Services
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
