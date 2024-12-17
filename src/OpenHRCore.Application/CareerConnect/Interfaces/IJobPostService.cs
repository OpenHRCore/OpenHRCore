using OpenHRCore.Application.CareerConnect.DTOs.JobPostDtos;

namespace OpenHRCore.Application.CareerConnect.Interfaces
{
    public interface IJobPostService
    {
        Task<OpenHRCoreServiceResponse<GetJobPostResponse>> CreateJobPostAsync(CreateJobPostRequest request);

        Task<OpenHRCoreServiceResponse<GetJobPostResponse>> GetJobPostByIdAsync(Guid id);

        Task<OpenHRCoreServiceResponse<IEnumerable<GetJobPostResponse>>> GetAllJobPostsAsync();
    }
}
