namespace OpenHRCore.Domain.Workforce.Entities
{
    public class JobGrade : OpenHRCoreBaseEntity
    {
        public required string Code { get; set; }
        public required string GradeName { get; set; }
        public string? Description { get; set; }
        public decimal? MinSalary { get; set; }
        public decimal? MaxSalary { get; set; }
        public required int SortOrder { get; set; }
    }
}
