namespace OpenHRCore.Application.Workforce.DTOs.JobLevelDtos
{
    public class GetJobLevelResponse
    {
        public required Guid Id { get; set; }
        public required string Code { get; set; }
        public required string LevelName { get; set; }
        public string? Description { get; set; }
        public required int SortOrder { get; set; }
    }
}
