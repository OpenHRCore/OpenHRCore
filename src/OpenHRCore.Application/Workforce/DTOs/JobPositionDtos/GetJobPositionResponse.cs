namespace OpenHRCore.Application.Workforce.DTOs.JobPositionDtos
{
    public class GetJobPositionResponse
    {
        public required Guid Id { get; set; }
        public required string Code { get; set; }
        public required string Title { get; set; }
        public string? Description { get; set; }
        public required Guid JobLevelId { get; set; }
        public required string JobLevel {  get; set; }
        public required Guid JobGradeId { get; set; }
        public required string JobGrade { get; set; }
        public required Guid OrganizationUnitId { get; set; }
        public required string OrganizationUnit { get; set; }
    }
}
