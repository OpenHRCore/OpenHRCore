namespace OpenHRCore.Application.CareerConnect.DTOs.JobPostDtos
{
    public class GetJobPostResponse
    {
        public required Guid Id { get; set; }
        public required string Code { get; set; }
        public required string Title { get; set; }
        public required string Description { get; set; }
    }
}
