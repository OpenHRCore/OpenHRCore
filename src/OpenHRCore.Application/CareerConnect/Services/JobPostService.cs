using AutoMapper;
using Microsoft.Extensions.Logging;
using OpenHRCore.Application.CareerConnect.DTOs.JobPostDtos;
using OpenHRCore.Application.CareerConnect.Interfaces;
using OpenHRCore.Application.UnitOfWork;
using OpenHRCore.Domain.CareerConnect.Interfaces;

namespace OpenHRCore.Application.CareerConnect.Services
{
    public class JobPostService : IJobPostService
    {
        private readonly IOpenHRCoreUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<JobPostService> _logger;
        private readonly IJobPostRepository _jobPostRepository;

        public JobPostService(IOpenHRCoreUnitOfWork unitOfWork, 
            IMapper mapper, 
            ILogger<JobPostService> logger, 
            IJobPostRepository jobPostRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
            _jobPostRepository = jobPostRepository;
        }

        public Task<OpenHRCoreServiceResponse<GetJobPostResponse>> CreateJobPostAsync(CreateJobPostRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<OpenHRCoreServiceResponse<IEnumerable<GetJobPostResponse>>> GetAllJobPostsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<OpenHRCoreServiceResponse<GetJobPostResponse>> GetJobPostByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
