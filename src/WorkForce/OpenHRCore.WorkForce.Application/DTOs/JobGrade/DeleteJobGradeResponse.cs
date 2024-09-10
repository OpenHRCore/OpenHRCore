namespace OpenHRCore.WorkForce.Application.DTOs.JobGrade
{
    public class DeleteJobGradeResponse
    {
        public required string Id { get; set; }
        public required string Code { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
        public int SortOrder { get; set; }
    }
}
